using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    public static class InputManager
    {
        public static Dictionary<char, LogicArray> _variables = new Dictionary<char, LogicArray>();

        public static void ReadExpression(string expression)
        {
            var variables = new List<char>();
            foreach (var element in expression)
            {
                if (VariableStorage.IsContain(element))
                    if (variables.Count == 0 || !variables.Contains(element))
                        variables.Add(element);
            }

            var size = (int)Math.Pow(2, variables.Count);

            var equalElements = size >> 1;
            foreach (var symbol in expression)
            {
                if (VariableStorage.IsContain(symbol))
                {
                    if (_variables.Count == 0 || !_variables.ContainsKey(symbol))
                    {
                        _variables.Add(symbol, new LogicArray(size, equalElements));
                        equalElements >>= 1;
                    }
                }
            }
        }

        public static LogicArray GetVariable(char c) =>
            _variables.ContainsKey(c) ? _variables[c] : throw new Exception(" ");
    }
}