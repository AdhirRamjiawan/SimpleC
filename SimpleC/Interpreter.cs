using SimpleC.DataStructures;
using SimpleC.TokenProcessors;

namespace SimpleC
{
    internal class Interpreter
    {
        internal static void Process(ProgramState programState, string sourceCode)
        {
            int sourceCodeIndex = 0;
            string token = string.Empty;

            while (sourceCodeIndex < sourceCode.Length)
            {
                char currentChar = sourceCode[sourceCodeIndex];

                ProcessCurrentCharacter(programState, sourceCode, sourceCodeIndex, ref token, currentChar);

                sourceCodeIndex++;
            }
        }

        private static void ProcessCurrentCharacter(ProgramState programState, string sourceCode, int sourceCodeIndex, ref string token, char curChar)
        {

            if      (curChar == Syntax.OpenCurlyBrace)  { OpenCurlyBraceProcessor.Process(programState, token);  }
            else if (curChar == Syntax.CloseCurlyBrace) { CloseCurlyBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.EndStatement)    { EndStatementProcessor.Process(programState, token);    }
            else if (curChar == Syntax.OpenBrace)       { OpenBraceProcessor.Process(programState, token);       }
            else if (curChar == Syntax.CloseBrace)      { CloseBraceProcessor.Process(programState, token);      }
            else if (curChar == Syntax.DoubleQoute)     { DoubleQuoteProcessor.Process(programState, token);     }
            else if (curChar == Syntax.Assignment)      { AssignmentProcessor.Process(programState, token);      }
            else
            {

                if (!Syntax.IsWhitespace(curChar) || programState.WhitespaceImportant)
                {

                    token += curChar;
                        
                    if (Syntax.IntLiteralForm.IsMatch(token))
                    {
                        IntLiteralProcessor.Process(programState, ref token);
                    }
                    else if (Syntax.VariableForm.IsMatch(token)
                        && programState.PreviousToken != "")
                    {
                        VariableReferenceProcessor.Process(programState, token);
                    }

                    return;
                }
                else
                {
                    // Might be a type, identifier or method name?

                    if (token == Types.Int)
                    {
                        IntTypeProcessor.Process(programState, token);
                    }
                    else
                    {
                        if (Syntax.VariableForm.IsMatch(token) && !programState.StringStarted)
                        {
                           // VariableProcessor.Process(programState, token);
                        }
                        
                    }

                }

            }

            programState.PreviousToken = token;
            token = string.Empty;

        }
    }
}
