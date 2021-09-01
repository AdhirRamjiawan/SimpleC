using System;
using System.IO;

namespace SimpleC
{
    class Program
    {
        static bool stringStarted = false;
        static bool identifierStarted = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Simple C interpreter");

            if (args.Length < 1)
            {
                Console.WriteLine("invalid usage");
                return;
            }

            string sourceCode = string.Empty;
            string sourceCodeFilePath = args[0];

            using (var reader = new StreamReader(File.OpenRead(sourceCodeFilePath)))
            {
                sourceCode = reader.ReadToEnd();
            }

            Console.WriteLine($"Executing {sourceCodeFilePath}...");

            int sourceCodeIndex = 0;
            string token = string.Empty;

            while (sourceCodeIndex < sourceCode.Length)
            {

                char currentChar = sourceCode[sourceCodeIndex];

                if (currentChar == ' ' || currentChar == '\n' || currentChar == '\r')
                {
                    sourceCodeIndex++;
                    continue;
                }

                ProcessCurrentCharacter(ref token, currentChar, ref sourceCodeIndex);


                sourceCodeIndex++;
            }
        }

        static void ProcessToken(string token)
        {

        }

        static void ProcessCurrentCharacter(ref string token, char currentChar, ref int sourceCodeIndex)
        {
            if (currentChar == '{')
            {
                Console.WriteLine("begin code block");
            }
            else if (currentChar == '}')
            {
                Console.WriteLine("end code block");
            }
            else if (currentChar == ';')
            {
                Console.WriteLine("end statement");
            }
            else if (currentChar == '(')
            {
                if (token != string.Empty)
                {
                    Console.WriteLine($"identifier found: {token}");
                }

                Console.WriteLine("begin brace");
            }
            else if (currentChar == ')')
            {
                Console.WriteLine("end brace");
            }
            else if (currentChar == '"')
            {
                if (stringStarted)
                {
                    Console.WriteLine($"string found: {token}");
                    Console.WriteLine("ending string");
                }
                else
                {
                    Console.WriteLine("starting string");
                }

                stringStarted = !stringStarted;
            }
            else
            {
                token += currentChar;
                return;
            }
            
            token = string.Empty;
        }
    }
}
