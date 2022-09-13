using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    public class LogicArray : IEnumerable<bool>
    {
        private readonly bool[] _values;
        public int Count { get; private set; }

        public LogicArray()
        {
            _values = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">Размер</param>
        /// <param name="n">Степень двойки</param>
        public LogicArray(int size, int n)
        {
            Count = size;
            _values = new bool[size];

            var isTurnZeros = true;
            var equalElements = 0;
            for (var i = 0; i < Count; i++)
            {
                if (equalElements >= n)
                {
                    isTurnZeros = !isTurnZeros;
                    equalElements = 0;
                }

                _values[i] = !isTurnZeros;

                equalElements++;
            }
        }

        public LogicArray(in LogicArray a)
        {
            Count = a.Count;
            _values = new bool[Count];

            for (var i = 0; i < Count; i++)
                _values[i] = a[i];
        }

        public LogicArray(in ICollection<bool> collection)
        {
            Count = collection.Count;
            _values = new bool[Count];

            var i = 0;
            foreach (var element in collection)
                _values[i++] = element;
        }


        public bool this[int i]
        {
            get => _values[i];
            private set => _values[i] = value;
        }

        public static LogicArray operator +(in LogicArray a, in LogicArray b)
        {
            var result = new LogicArray(a);
            for (var i = 0; i < a.Count; i++)
                result[i] = a[i] || b[i];

            return result;
        }

        public static LogicArray operator *(in LogicArray a, in LogicArray b)
        {
            var result = new LogicArray(a);
            for (var i = 0; i < a.Count; i++)
                result[i] = a[i] && b[i];

            return result;
        }

        public static LogicArray operator !(in LogicArray a)
        {
            var result = new LogicArray(a);
            for (var i = 0; i < a.Count; i++)
                result[i] = !a[i];

            return result;
        }

        public static LogicArray Perform(ref Stack<LogicArray> stack, char sign)
        {
            LogicArray result = null;

            LogicArray a, b;
            switch (sign)
            {
                case '!':
                    a = stack.Peek();
                    stack.Pop();

                    result = !a;
                    break;
                case '*':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    result = b * a;
                    break;
                case '+':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    result = b + a;
                    break;
            }

            return result;
        }

        public bool AllAreTrue() => _values.All(element => element);

        public bool AllAreFalse() => _values.All(element => !element);

        public IEnumerator<bool> GetEnumerator() => ((IEnumerable<bool>)_values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}