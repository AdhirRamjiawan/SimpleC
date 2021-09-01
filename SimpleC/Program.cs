using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleC
{

    public enum ParameterType
    {
        String = 0
    }

    public enum TokenType
    {
        String = 0,
        Method,
        Symbol
    }

    class StringTableEntry
    {
        public int ID;
        public string Value;
    }
    
    
    class MethodParameter
    {
        public int ParameterID;
        public ParameterType Type;
    }

    class MethodCallTableEntry
    {
        public int ID;
        public string MethodName;
        public List<MethodParameter> Parameters;
    }

    class Program
    {
        static bool stringStarted = false;
        static bool debugMode = false;
        static string previousToken = string.Empty;
        static int currentMethodId = -1;
        static Stack<TokenType> currentTokenStack = new Stack<TokenType>();

        static List<StringTableEntry> stringTable = new List<StringTableEntry>();
        static List<MethodCallTableEntry> methodCallsTable = new List<MethodCallTableEntry>();
        static List<int> executionSequence = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Simple C interpreter");

            if (args.Length < 1)
            {
                Console.WriteLine("invalid usage");
                return;
            }

            if (args.Length == 2)
            {
                if (args[1] == "-d")
                {
                    debugMode = true;
                }
            }

            string sourceCode = string.Empty;
            string sourceCodeFilePath = args[0];

            using (var reader = new StreamReader(File.OpenRead(sourceCodeFilePath)))
            {
                sourceCode = reader.ReadToEnd();
            }

            if (debugMode)
            {
                Console.WriteLine($"Executing {sourceCodeFilePath}...");
            }
            
            int sourceCodeIndex = 0;
            string token = string.Empty;

            while (sourceCodeIndex < sourceCode.Length)
            {

                char currentChar = sourceCode[sourceCodeIndex];

                if (!stringStarted && (currentChar == ' ' || currentChar == '\n' || currentChar == '\r'))
                {
                    sourceCodeIndex++;
                    continue;
                }

                ProcessCurrentCharacter(ref token, currentChar, ref sourceCodeIndex);


                sourceCodeIndex++;
            }

            ExecuteProgram();
        }

        static void ExecuteProgram()
        {
            foreach (var methodCallId in executionSequence)
            {
                DebugLog($"executing method: {methodCallId}");

                var methodCall = methodCallsTable.Where(mc => mc.ID == methodCallId).FirstOrDefault();
                ExecuteMethod(methodCall);
            }
        }

        static void DebugLog(string message)
        {
            if (debugMode)
            {
                Console.WriteLine(message);
            }
        }

        static void ExecuteMethod(MethodCallTableEntry methodCall)
        {
            DebugLog($"Calling method: {methodCall.MethodName}");

            foreach (var param in methodCall.Parameters)
            {
                if (param.Type == ParameterType.String)
                {
                    var stringTableEntry = stringTable.Where(sv => sv.ID == param.ParameterID).FirstOrDefault();

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

        static void ProcessCurrentCharacter(ref string token, char currentChar, ref int sourceCodeIndex)
        {
            if (currentChar == '{')
            {
                DebugLog("begin code block");
                currentTokenStack.Push(TokenType.Symbol);
            }
            else if (currentChar == '}')
            {
                DebugLog("end code block");
                currentTokenStack.Push(TokenType.Symbol);
            }
            else if (currentChar == ';')
            {
                DebugLog("end statement");
                currentTokenStack.Clear();

                currentMethodId = -1;
            }
            else if (currentChar == '(')
            {
                if (token != string.Empty)
                {
                    DebugLog($"method found: {token}");

                    currentMethodId = methodCallsTable.Count;
                    currentTokenStack.Push(TokenType.Method);
                    methodCallsTable.Add(new MethodCallTableEntry() { ID = currentMethodId, MethodName = token });
                }

                DebugLog("begin brace");
                currentTokenStack.Push(TokenType.Symbol);
            }
            else if (currentChar == ')')
            {
                DebugLog("end brace");
                currentTokenStack.Clear();
                currentMethodId = -1;
                //currentTokenStack.Push(TokenType.Symbol);
            }
            else if (currentChar == '"')
            {
                if (stringStarted)
                {
                    DebugLog($"string found: \"{token}\"");
                    DebugLog("ending string");

                    var stringID = stringTable.Count;
                    stringTable.Add(new StringTableEntry() { ID = stringID, Value = token });

                    var stackContainsMethod = currentTokenStack.Contains(TokenType.Method);
                    if (stackContainsMethod && currentMethodId > -1)
                    {
                        var methodCall = methodCallsTable.Where(mc => mc.ID == currentMethodId).FirstOrDefault();

                        methodCall.Parameters = new List<MethodParameter>() 
                        {
                            new MethodParameter() { ParameterID = stringID, Type = ParameterType.String }
                        };

                        executionSequence.Add(methodCall.ID);
                    }

                    currentTokenStack.Push(TokenType.Symbol);
                }
                else
                {
                    DebugLog("starting string");
                }

                stringStarted = !stringStarted;
            }
            else
            {
                token += currentChar;
                return;
            }

            previousToken = token;
            token = string.Empty;
        }
    }
}
