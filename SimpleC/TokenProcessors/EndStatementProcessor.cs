using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class EndStatementProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            Debug.Log("end statement");
            programState.CurrentTokenStack.Clear();
            programState.CurrentMethodId = -1;
        }
    }
}
