using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class OpenBraceProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            if (token != string.Empty)
            {
                Debug.Log($"method found: {token}");

                programState.CurrentMethodId = programState.MethodCallsTable.Count;
                programState.CurrentTokenStack.Push(TokenType.Method);
                programState.MethodCallsTable.Add(new MethodCallTableEntry() { ID = programState.CurrentMethodId, MethodName = token });
            }

            Debug.Log("begin brace");
            programState.CurrentTokenStack.Push(TokenType.Symbol);
        }
    }
}
