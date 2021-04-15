[![tool hopperclean](https://github.com/alextochetto/dotnet-tools/actions/workflows/dotnet.yml/badge.svg)](https://github.com/alextochetto/dotnet-tools/actions/workflows/dotnet.yml)

<p align="center">
  <img src="https://github.com/alextochetto/dotnet-tools/blob/main/src/Hopper.CleanNugetPackage/Images/icon.png?raw=true" width="150px" alt="SharpSenses" />
</p>
<p>

# dotnet tools

## Hopper.CleanNugetPackage

This tool helps you to remove old .nuget packages and reduce the disk space usage.

### Install Hopper.CleanNugetPackage

> dotnet tool install --global Hopper.CleanNugetPackage

### Uninstall Hopper.CleanNugetPackage

> dotnet tool uninstall -g Hopper.CleanNugetPackage

### How it works

Below there are some steps to understand how it works:

> hopperclean microsoft.applicationinsights

1. You can specify a part of directory name like above, and the tool will run to all paths that starts with the name of package you've specified.

> hopperclean microsoft.entityframeworkcore.abstractions

2. You can specify the full name, not just the initial part libe above.