using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hopper.CleanNugetPackage
{
    [Command(Description = "CleanNugetPackage")]
    public class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "The parameter that must be specified.\nPackage fullname or start name with.")]
        [Required]
        public string PackageName { get; } = null;

        private int OnExecute()
        {
            Console.WriteLine();
            Console.WriteLine($"Clean Nuget Package @alextochetto");
            Console.WriteLine();

            string regexPattern = $"{PackageName}*";
            var regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            string userNugetPackagesPath = $@"{userProfilePath}\.nuget\packages";
            IEnumerable<string> directories = Directory.EnumerateDirectories(userNugetPackagesPath);
            string[] packagesPathName = directories.Where(_ => regex.IsMatch(_)).ToArray();

            foreach (string packageNamePath in packagesPathName)
            {
                Console.WriteLine(packageNamePath);

                List<Version> versions = new List<Version>();
                IEnumerable<string> packagesVersionPath = Directory.EnumerateDirectories(packageNamePath);
                foreach (string packageVersionPath in packagesVersionPath)
                {
                    var directoryVersionName = new DirectoryInfo(packageVersionPath).Name;
                    versions.Add(new Version(directoryVersionName));
                }

                IEnumerable<IGrouping<(int Major, int Minor), Version>> groupVersionsMajorMinor = versions.GroupBy(_ => (_.Major, _.Minor));
                foreach (IGrouping<(int Major, int Minor), Version> groupVersionMajorMinor in groupVersionsMajorMinor)
                {
                    IEnumerable<Version> versionsMajorMinor = versions.Where(_ => _.Major == groupVersionMajorMinor.Key.Major && _.Minor == groupVersionMajorMinor.Key.Minor);
                    Version version = versionsMajorMinor.OrderByDescending(_ => _.Build).ThenByDescending(_ => _.MinorRevision).FirstOrDefault();
                    versions.Remove(version);

                    Console.WriteLine($"Current path of major, minor, build and minor revision: {packageNamePath}\\{version.ToString()}");
                }

                foreach (Version versionToDelete in versions)
                {
                    string pathToDelete = $"{packageNamePath}\\{versionToDelete.Major}.{versionToDelete.Minor}.{versionToDelete.Build}.{versionToDelete.MinorRevision.ToString().PadLeft(4, '0')}";
                    if (Directory.Exists(pathToDelete))
                    {
                        Directory.Delete(pathToDelete, recursive: true);

                        Console.WriteLine($"Removed: {pathToDelete}");
                    }
                }
            }

            return 0;
        }
    }
}
