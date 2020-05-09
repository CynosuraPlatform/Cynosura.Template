$source = Resolve-Path "..\src\"
$dest = ".\content"
$exclude = "node_modules"

# create destination directory
New-Item -ItemType Directory -Force -Path $dest

# delete files
Get-ChildItem $dest | Remove-Item -Recurse

# create .template.config
New-Item -ItemType Directory -Force -Path $dest\.template.config

# copy template.json
Copy-Item template.json $dest\.template.config\template.json

# copy files
Get-ChildItem $source -Recurse -Force  | Where-Object {$_.FullName -notmatch $exclude} |
    Copy-Item -Destination {Join-Path $dest $_.FullName.Substring($source.path.length)}

# copy license file
Copy-Item ..\LICENSE.md .\LICENSE.md
