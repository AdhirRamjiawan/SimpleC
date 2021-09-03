using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleC
{
    internal class Syntax
    {
        internal const char OpenBrace = '(';
        internal const char CloseBrace = ')';
        internal const char OpenCurlyBrace = '{';
        internal const char CloseCurlyBrace = '}';
        internal const char EndStatement = ';';
        internal const char DoubleQoute = '"';
        internal const char Assignment = '=';
        internal const char Space = ' ';
        internal const char Tab = '\t';
        internal const char CarriageReturn = '\r';
        internal const char Newline = '\n';

        internal static readonly Regex VariableForm = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
        internal static readonly Regex IntLiteralForm = new Regex("^[0-9]+$");

        public static bool IsWhitespace(char currentChar)
        {
            return currentChar == Tab
                || currentChar == Space
                || currentChar == Newline
                || currentChar == CarriageReturn;
        }
    }
}