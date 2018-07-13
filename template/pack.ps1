$source = Resolve-Path "..\src\"
$dest = ".\content"
$exclude = "node_modules"
$dest_exclude = "\.template\.config"

# delete files

Get-ChildItem $dest | Where-Object {$_.FullName -notmatch $dest_exclude} | Remove-Item -Recurse

# Copy files

Get-ChildItem $source -Recurse  | Where-Object {$_.FullName -notmatch $exclude} |
    Copy-Item -Destination {Join-Path $dest $_.FullName.Substring($source.path.length)}
nuget pack