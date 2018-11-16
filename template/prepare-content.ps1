$source = Resolve-Path "..\src\"
$dest = ".\content"
$exclude = "node_modules"

# delete files
Get-ChildItem $dest | Remove-Item -Recurse

# create path
New-Item -ItemType Directory -Force -Path content\.template.config\

# copy template.json
cp template.json content\.template.config\template.json

# Copy files

Get-ChildItem $source -Recurse  | Where-Object {$_.FullName -notmatch $exclude} |
    Copy-Item -Destination {Join-Path $dest $_.FullName.Substring($source.path.length)}
