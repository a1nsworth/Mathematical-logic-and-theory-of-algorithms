using System;
using System.Linq;

namespace Laba1
{
    public class VariableStorage
    {
        public static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static bool IsContain(char c) => Array.IndexOf(alphabet, c) != -1;
    }
}