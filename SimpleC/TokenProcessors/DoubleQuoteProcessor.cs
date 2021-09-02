using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class DoubleQuoteProcessor
    {
        internal static void Process(ProgramState programState, string token)
        {
            if (programState.StringStarted)
            {
                Debug.Log($"string found: \"{token}\"");
                Debug.Log("ending string");

                var stringID = programState.StringTable.Count;
                programState.StringTable.Add(new StringTableEntry() { ID = stringID, Value = token });

                var stackContainsMethod = programState.CurrentTokenStack.Contains(TokenType.Method);
                if (stackContainsMethod && programState.CurrentMethodId > -1)
                {
                    var methodCall = programState.MethodCallsTable.Where(mc => mc.ID == programState.CurrentMethodId).FirstOrDefault();

                    methodCall.Parameters = new List<MethodParameter>()
                        {
                            new MethodParameter() { ParameterID = stringID, Type = ParameterType.String }
                        };

                    programState.ExecutionSequence.Add(methodCall.ID);
                }

                programState.CurrentTokenStack.Push(TokenType.Symbol);
            }
            else
            {
                Debug.Log("starting string");
            }

            programState.StringStarted = !programState.StringStarted;
        }
    }
}
