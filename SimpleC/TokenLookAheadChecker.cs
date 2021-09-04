using SimpleC.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC
{
    internal class TokenLookAheadChecker
    {
        internal static bool CheckFor(ProgramState programState, string sourceCode, int sourceCodeIndex, char tokenToCheckFor)
        {
            int sourceCodeLength = sourceCode.Length;
            char nextCharToTest = '\0';

            while (sourceCodeIndex < sourceCodeLength && Syntax.IsWhitespace(sourceCode[sourceCodeIndex]))
            {
                sourceCodeIndex++;
                nextCharToTest = sourceCode[sourceCodeIndex];
            }

            if (nextCharToTest == tokenToCheckFor)
                return true;
            else
                return false;

        }
    }
}
