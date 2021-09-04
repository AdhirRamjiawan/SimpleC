using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.TokenProcessors
{
    internal class IntLiteralProcessor
    {
        internal static void Process(ProgramState programState, ref string token)
        {
            Debug.Log($"int literal {token}");
            programState.CurrentTokenStack.Push(TokenType.IntLiteral);

            // if RHS of assignment is an expression then this needs to move nearer the EndStatementProcessor
            if (programState.CurrentTokenStack.Contains(TokenType.Assignment))
            {
                var variable = programState.VariablesTable.Where(v=> v.ID == programState.CurrentIndentiferId).FirstOrDefault();
                variable.Value = Convert.ToInt32(token);
            }

            programState.PreviousToken = token;
            token = string.Empty;
        }
    }
}