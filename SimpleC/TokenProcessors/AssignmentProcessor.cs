using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class AssignmentProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            Debug.Log("assingment");
            programState.CurrentTokenStack.Push(TokenType.Assignment);
        }
    }
}
