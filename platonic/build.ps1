﻿$dest = "C:\Source\mode-api\src";

cd C:\Source\mode-api\platonic;

New-Item -ItemType Directory -Force -Path $dest

Remove-Item –path "$($dest)\*" –Recurse -Force

Copy-Item ./* -Destination ../src -Exclude build.ps1,README.md -Recurse

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
$replacements."modeDetailPlatonic" = "contextDetail"; 
$replacements."ModeDetailPlatonic" = "ContextDetail"; 
$replacements."NamePlatonic" = "Name";
$replacements."namePlatonic" = "name";
$replacements."mode-detail-platonic" = "context-detail";
$replacements."mode_detail_platonic" = "context_detail";
$replacements."BattleLanguagePlatonic" = "BattleLanguage";
 

foreach ($file in Get-Files)
{
    foreach ($r in $replacements.GetEnumerator()) {
        (Get-Content $file.PSPath -Force) |
        Foreach-Object { $_ -creplace $r.Name, $r.Value } |
        Set-Content $file.PSPath -Force
    }
}

foreach ($r in $replacements.GetEnumerator()) {
    Get-Items | Where {$_.Name -clike "*$($r.Name)*"} | Rename-Item -NewName { $_.name -creplace $r.Name, $r.Value} -verbose
}
