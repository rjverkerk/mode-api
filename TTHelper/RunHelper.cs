using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task = System.Threading.Tasks.Task;

namespace TTHelper
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class RunHelper
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("f7c3b250-7b0f-4627-bdb0-c10aa3dab5de");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunHelper"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private RunHelper(AsyncPackage package, OleMenuCommandService commandService) {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static RunHelper Instance {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider {
            get {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package) {
            // Switch to the main thread - the call to AddCommand in RunHelper's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new RunHelper(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e) {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            string title = "Generated Files";

            var updatedFiles = await GenerateTextTemplates();
            var solutionDirectory = await GetSolutionDirectory();

            string message = String.Join("\r\n", updatedFiles.Select(path => path.Replace(solutionDirectory, "")));

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private static string solutionName = "mode-api";

        private async Task<IEnumerable<string>> RunT4Templates(string app, dynamic config) {
            var t4 = await ServiceProvider.GetServiceAsync(typeof(STextTemplating)) as ITextTemplating;
            
            var t4Files = Directory.GetFiles(await GetSolutionDirectory(), "*.tt", SearchOption.AllDirectories)
                .Where(path => path.Contains("TT"));

            var output = new List<string>();
            foreach(var filePath in t4Files ) {
                string result = t4.ProcessTemplate(filePath, File.ReadAllText(filePath));
                var path = Path.ChangeExtension(filePath.Replace("TTApp", app).Replace("TT", config.Key), ".cs");
                File.WriteAllText(path, result);

                output.Add(path);
            }

            return output;
        }

        public async Task<IEnumerable<string>> GenerateTextTemplates() {
            var solutionDirectory = await GetSolutionDirectory();

            var files = Directory.GetFiles(solutionDirectory, "*.cs", SearchOption.AllDirectories)
                .Where(path =>
                    !path.Contains($"{solutionName}.tt") &&
                    !path.Contains($"TTModel.xml") &&
                    !path.Contains("TTHelper") &&
                    path.Contains("TT"));

            var t4Files = Directory.GetFiles(solutionDirectory, "*.tt", SearchOption.AllDirectories)
                .Where(path => path.Contains("TT"));

            dynamic model = GetModel($"{solutionDirectory}/{solutionName}.tt/TTModel.xml");

            var output = new List<string>();
            foreach ( var app in model.Apps ) {
                foreach ( var config in app.Value ) {
                    File.WriteAllText($"{solutionDirectory}/{solutionName}.tt/TTProcess.xml", ToXmlString(config, new List<string>(), app.Key));
                    output.AddRange(await RunT4Templates(app.Key, config));
                }
            }

            return output;
        }

        private string ToXmlString(dynamic model, List<string> xml, string root) {
            if ( root != null ) {
                xml.Add($"<{root}>");
            }
            xml.Add($"<{model.Key}>");
            foreach (var prop in model.Value ) {
                ToXmlString(prop, xml, null);
            }
            xml.Add($"</{model.Key}>");
            if ( root != null ) {
                xml.Add($"</{root}>");
            }

            return string.Join("", xml);
        }

        private static void CopyFile(string app, dynamic config, string sourceFile) {
            var targetPath = sourceFile.Replace("TTApp", app).Replace("TT", config.Key);
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            File.Copy(sourceFile, targetPath);
        }

        private static dynamic GetModel(string modelPath) {
            XDocument doc = XDocument.Load(modelPath);
            string jsonText = JsonConvert.SerializeXNode(doc);
            return JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
        }

        private async Task<string> GetSolutionDirectory() {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            // Get an instance of the currently running Visual Studio IDE
            var dte = (DTE)(await ServiceProvider.GetServiceAsync(typeof(DTE)));
            return System.IO.Path.GetDirectoryName(dte.Solution.FullName);
        }
    }
}
