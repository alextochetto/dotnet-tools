using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CleanNugetPackage
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
            string[] x = directories.Where(_ => regex.IsMatch(_)).ToArray();

            foreach (var item in x)
            {
                Console.WriteLine(item);
            }

            return 0;
        }
    }
}
