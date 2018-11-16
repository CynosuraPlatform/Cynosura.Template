# Overview
dotnet Cynosura template

## Project structure

    ┐ 
    ├── src        - template source content
    └──┐ template  - template automation files
       ├── base    - NuGet package base files
       ├── content - Nuget package file, used by automation scripts and Azure DevOps pipeline

# Development

## Automation

> $ cd template

> $ npm install

> $ ./pack-development

If you have probllems with `gyp`

on windows:

> $ npm install --global --production windows-build-tools

> $ npm install -g node-gyp

on mac:

> $ xcode-select --install


# Usage

> dotnet new -i Cynosura.Template