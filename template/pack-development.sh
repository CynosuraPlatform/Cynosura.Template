#!/bin/bash

npm run pack

# Uninstall template
dotnet new -u Cynosura.Template

# Install new template

dotnet new -i ./artifacts/Cynosura.Template.nupkg