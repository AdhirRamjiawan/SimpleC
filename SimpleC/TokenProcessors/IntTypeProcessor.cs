using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class IntTypeProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            Debug.Log($"type {token}");
            programState.CurrentTokenStack.Push(TokenType.IntType);

            int variableId = programState.VariablesTable.Count;
            programState.CurrentIndentiferId = variableId;
            programState.CurrentVariableDefault = 0;
        }
    }
}
