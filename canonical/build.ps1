$dest = "C:\Source\mode-api\src";

cd C:\Source\mode-api\canonical;

Write-Host "Create folder:" -ForegroundColor Green;

New-Item -ItemType Directory -Force -Path $dest

Remove-Item –path "$($dest)\*" –Recurse -Force

Write-Host "Removed existing files." -ForegroundColor Green;

Copy-Item ./* -Destination ../src -Exclude build.ps1,README.md -Recurse

Write-Host "Copied files." -ForegroundColor Green;

Get-ChildItem ../src/* -Recurse |
Where-Object{$_.FullName -like "*\obj\*" -or $_.FullName -like "*\bin\*"} |
Remove-Item -Recurse

Write-Host "Removed build directories." -ForegroundColor Green;

function Get-Files {
    return Get-ChildItem $dest -Recurse -File |
    Where {$_.FullName -notlike "*\obj\*"} |
    Where {$_.FullName -notlike "*\bin\*"}
}

function Get-Items {
    return Get-ChildItem $dest -Recurse |
    Where {$_.FullName -notlike "*\obj\*"} |
    Where {$_.FullName -notlike "*\bin\*"}
}

[hashtable]$replacements = New-Object system.collections.hashtable
$replacements."modeDetailCanonical" = "modeDetail"; 
$replacements."ModeDetailCanonical" = "ModeDetail"; 
$replacements."NameCanonical" = "Name";
$replacements."nameCanonical" = "name";
$replacements."name_canonical" = "name";
$replacements."mode-detail-canonical" = "mode-detail";
$replacements."mode_detail_canonical" = "mode_detail";
$replacements."BattleLanguageCanonical" = "BattleLanguage";
$replacements."mode-canonical-api" = "mode-api";
$replacements."mode_canonical" = "mode";
 

foreach ($file in Get-Files)
{
    foreach ($r in $replacements.GetEnumerator()) {
        (Get-Content $file.PSPath -Force) |
        Foreach-Object { $_ -creplace $r.Name, $r.Value } |
        Set-Content $file.PSPath -Force
    }
}

foreach ($r in $replacements.GetEnumerator()) {
    Get-Items | Where {$_.Name -clike "*$($r.Name)*"} | Rename-Item -NewName { $_.name -creplace $r.Name, $r.Value}
}

Write-Host "Performed string replacements." -ForegroundColor Green;
