using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC
{
    internal class SourceCodeLoader
    {
        internal static string Load(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine($"Simple C interpreter {Program.SimpleCVersion}");
                Console.WriteLine("invalid usage");
                System.Environment.Exit(1);
            }

            if (args.Length == 2)
            {
                if (args[1] == "-d")
                {
                    Debug.DebugMode = true;
                }
            }

            string sourceCode = string.Empty;
            string sourceCodeFilePath = args[0];

            using (var reader = new StreamReader(File.OpenRead(sourceCodeFilePath)))
            {
                sourceCode = reader.ReadToEnd();
            }

            return sourceCode;
        }
    }
}
