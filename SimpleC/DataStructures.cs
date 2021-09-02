using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC.DataStructures
{
    internal enum ParameterType
    {
        String = 0
    }

    internal enum TokenType
    {
        String = 0,
        Method,
        Symbol
    }

    internal class StringTableEntry
    {
        public int ID;
        public string Value;
    }

    internal class MethodParameter
    {
        public int ParameterID;
        public ParameterType Type;
    }

    internal class MethodCallTableEntry
    {
        public int ID;
        public string MethodName;
        public List<MethodParameter> Parameters;
    }

    internal class ProgramState
    {
        public bool StringStarted = false;
        public string PreviousToken = string.Empty;
        public int CurrentMethodId = -1;
        public Stack<TokenType> CurrentTokenStack = new Stack<TokenType>();

        public List<StringTableEntry> StringTable = new List<StringTableEntry>();
        public List<MethodCallTableEntry> MethodCallsTable = new List<MethodCallTableEntry>();
        public List<int> ExecutionSequence = new List<int>();
    }
}
