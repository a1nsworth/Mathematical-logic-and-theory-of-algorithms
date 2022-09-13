using System;
using System.Collections.Generic;

namespace Laba1
{
    public static class LogicalOperations
    {
        
        private static readonly Dictionary<char, (TypeOperation, int)> _operations =
            new Dictionary<char, (TypeOperation, int)>
            {
                { '!', (TypeOperation.Unary, 3) },
                { '*', (TypeOperation.Binary, 2) },
                { '+', (TypeOperation.Binary, 1) },
                { '(', (TypeOperation.Binary, 0) }
            };

        public static (TypeOperation, int) GetPriority(char c) =>
            _operations.ContainsKey(c) ? _operations[c] : throw new Exception("bad");

        public static bool IsOperation(char c) => _operations.ContainsKey(c);
    }
}