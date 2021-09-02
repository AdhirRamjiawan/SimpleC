using SimpleC.DataStructures;
using SimpleC.TokenProcessors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleC
{
    class Program
    {
        static readonly string version = "1.0.0";
        
        static ProgramState programState = new ProgramState();

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine($"Simple C interpreter {version}");
                Console.WriteLine("invalid usage");
                return;
            }

            if (args.Length == 2)
            {
                if (args[1] == "-d")
                {
                    Debug.DebugMode= true;
                }
            }

            string sourceCode = string.Empty;
            string sourceCodeFilePath = args[0];

            using (var reader = new StreamReader(File.OpenRead(sourceCodeFilePath)))
            {
                sourceCode = reader.ReadToEnd();
            }

            if (Debug.DebugMode)
            {
                Console.WriteLine($"Executing {sourceCodeFilePath}...");
            }
            
            int sourceCodeIndex = 0;
            string token = string.Empty;

            while (sourceCodeIndex < sourceCode.Length)
            {

                char currentChar = sourceCode[sourceCodeIndex];

                if (!programState.StringStarted && (currentChar == '\t' || currentChar == ' ' || currentChar == '\n' || currentChar == '\r'))
                {
                    sourceCodeIndex++;
                    continue;
                }

                ProcessCurrentCharacter(ref token, currentChar);

                sourceCodeIndex++;
            }

            ExecuteProgram();
        }

        static void ExecuteProgram()
        {
            foreach (var methodCallId in programState.ExecutionSequence)
            {
                Debug.Log($"executing method: {methodCallId}");

                var methodCall = programState.MethodCallsTable.Where(mc => mc.ID == methodCallId).FirstOrDefault();
                ExecuteMethod(methodCall);
            }
        }

        static void ExecuteMethod(MethodCallTableEntry methodCall)
        {
            Debug.Log($"Calling method: {methodCall.MethodName}");

            foreach (var param in methodCall.Parameters)
            {
                if (param.Type == ParameterType.String)
                {
                    var stringTableEntry = programState.StringTable.Where(sv => sv.ID == param.ParameterID).FirstOrDefault();

                    if (methodCall.MethodName == "output")
                    {
                        Console.Write(stringTableEntry.Value);
                    }
                    else if (methodCall.MethodName == "outputline")
                    {
                        Console.WriteLine(stringTableEntry.Value);
                    }
                }
            }
        }

        static void ProcessCurrentCharacter(ref string token, char curChar)
        {

            if      (curChar == Syntax.OpenCurlyBrace ) { OpenCurlyBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.CloseCurlyBrace) { CloseCurlyBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.EndStatement   ) { EndStatementProcessor.Process(programState, token); }
            else if (curChar == Syntax.OpenBrace      ) { OpenBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.CloseBrace     ) { CloseBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.DoubleQoute    ) { DoubleQuoteProcessor.Process(programState, token); }
            else
            {
                token += curChar;
                return;
            }

            programState.PreviousToken = token;
            token = string.Empty;
        }
    }
}
