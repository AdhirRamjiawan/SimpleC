using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC
{
    internal class Executor
    {
        internal static void Process(ProgramState programState)
        {
            foreach (var methodCallId in programState.ExecutionSequence)
            {
                Debug.Log($"executing method: {methodCallId}");

                var methodCall = programState.MethodCallsTable.Where(mc => mc.ID == methodCallId).FirstOrDefault();
                ExecuteMethod(programState, methodCall);
            }
        }

        private static void ExecuteMethod(ProgramState programState, MethodCallTableEntry methodCall)
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

    }
}
