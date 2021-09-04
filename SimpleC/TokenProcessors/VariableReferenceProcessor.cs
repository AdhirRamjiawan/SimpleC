using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class VariableReferenceProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            Debug.Log($"variable reference {token}");
            programState.CurrentTokenStack.Push(TokenType.Identifier);

            programState.VariablesTable.Add(new VariableTableEntry()
            {
                ID = programState.CurrentIndentiferId,
                VariableName = token,
                Value = programState.CurrentVariableDefault
            });
        }
    }
}
