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
        internal static readonly string SimpleCVersion = "1.0.0";
        
        static ProgramState programState = new ProgramState();

        static void Main(string[] args)
        {
            var sourceCode = SourceCodeLoader.Load(args);
            Interpreter.Process(programState, sourceCode);
            Executor.Process(programState);
        }
    }
}
