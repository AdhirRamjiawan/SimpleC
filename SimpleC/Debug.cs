using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleC
{
    internal class Debug
    {
        internal static bool DebugMode = false;

        internal static void Log(string message)
        {
            if (DebugMode)
            {
                Console.WriteLine(message);
            }
        }
    }
}
