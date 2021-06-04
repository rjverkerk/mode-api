using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Xunit;

namespace mode_api.tt
{
    public class MoveFiles
    {
        private static string solutionName = "mode-api";

        [Fact]
        public void MoveFilesMethod()
        {
            var solutionDirectory = GetSolutionDirectory();

            var files = Directory.GetFiles(solutionDirectory, "*.cs", SearchOption.AllDirectories)
                .Where(path =>
                    !path.Contains($"{solutionName}.tt") &&
                    !path.Contains($"TTModel.xml") &&
                    path.Contains("TT"));

            dynamic model = GetModel($"{solutionDirectory}/{solutionName}/TTModel.xml");


            foreach (var app in model.Apps)
            {
                foreach(var config in app.Value)
                {
                    foreach(var file in files)
                    {
                        CopyFile(app.Key, config, file);
                    }
                }
            }
        }

        private static void CopyFile(string app, dynamic config, string sourceFile)
        {
            var targetPath = sourceFile.Replace("TTApp", app).Replace("TT", config.Key);
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            File.Copy(sourceFile, targetPath);
        }

        private static dynamic GetModel(string modelPath)
        {
            XDocument doc = XDocument.Load(modelPath);
            string jsonText = JsonConvert.SerializeXNode(doc);
            return JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
        }

        private static string GetSolutionDirectory()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        }
    }
}
