﻿using SimpleC.DataStructures;
using SimpleC.TokenProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if (!programState.StringStarted && (currentChar == '\t' || currentChar == ' ' || currentChar == '\n' || currentChar == '\r'))
                {
                    sourceCodeIndex++;
                    continue;
                }

                ProcessCurrentCharacter(programState, ref token, currentChar);

                sourceCodeIndex++;
            }
        }

        private static void ProcessCurrentCharacter(ProgramState programState, ref string token, char curChar)
        {

            if (curChar == Syntax.OpenCurlyBrace) { OpenCurlyBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.CloseCurlyBrace) { CloseCurlyBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.EndStatement) { EndStatementProcessor.Process(programState, token); }
            else if (curChar == Syntax.OpenBrace) { OpenBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.CloseBrace) { CloseBraceProcessor.Process(programState, token); }
            else if (curChar == Syntax.DoubleQoute) { DoubleQuoteProcessor.Process(programState, token); }
            else
            {
                token += curChar;
                return;
            }

            programState.PreviousToken = token;
            token = string.Empty;
        }
    }
}