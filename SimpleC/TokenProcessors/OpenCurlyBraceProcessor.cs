using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class OpenCurlyBraceProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            Debug.Log("begin code block");
            programState.CurrentTokenStack.Push(TokenType.Symbol);
        }
    }
}
