using System;
using System.Collections.Generic;

namespace Laba1
{
    public class Сalculator
    {
        public float Value { get; private set; }

        public Сalculator(string expression)
        {
            CalculatingExpression(GetOutputReversePolishNotation(expression));
        }

        private int GetPriority(char c)
        {
            switch (c)
            {
                case '(':
                case ')':
                    return 0;
                case '*':
                case '/':
                    return 1;
                case '+':
                case '-':
                    return 2;
                default:
                    return 3;
            }
        }

        private float Perform(ref Stack<float> stack, char sign)
        {
            var value = 0f;

            float a, b = 0;
            switch (sign)
            {
                case '*':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    value = b * a;

                    break;
                case '/':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    value = b / a;

                    break;
                case '+':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    value = b + a;

                    break;
                case '-':
                    a = stack.Peek();
                    stack.Pop();
                    b = stack.Peek();
                    stack.Pop();

                    value = b - a;

                    break;
            }

            return value;
        }

        private List<char> GetOutputReversePolishNotation(string expression)
        {
            void OrderSigns(ref Stack<char> s)
            {
                var top = '\0';
                var bottom = '\0';
                if (s.Count >= 2)
                {
                    top = s.Peek();
                    s.Pop();
                    bottom = s.Peek();
                    s.Push(top);

                    if (GetPriority(bottom) == 1)
                    {
                        s.Pop();
                        s.Pop();

                        s.Push(top);
                        s.Push(bottom);
                    }
                }
            }

            var output = new List<char>();
            var stack = new Stack<char>();

            for (var i = 0; i < expression.Length; i++)
            {
                var symbol = expression[i];

                if (char.IsDigit(symbol))
                {
                    output.Add(symbol);

                    if (stack.Count != 0 && GetPriority(stack.Peek()) == 1)
                    {
                        output.Add(stack.Peek());
                        stack.Pop();
                    }
                }
                else
                {
                    char next;

                    switch (symbol)
                    {
                        case '(':
                            stack.Push(symbol);
                            break;
                        case ')':
                        {
                            while (stack.Peek() != '(')
                            {
                                output.Add(stack.Peek());
                                stack.Pop();
                            }

                            stack.Pop();

                            if (stack.Count == 1 && GetPriority(stack.Peek()) == 1)
                            {
                                output.Add(stack.Peek());
                                stack.Pop();
                            }

                            break;
                        }
                        case '*':
                        case '/':
                        {
                            next = expression[i + 1];
                            if (next == '(' || char.IsDigit(next))
                                stack.Push(symbol);
                            else
                                output.Add(symbol);

                            break;
                        }
                        case '+':
                        case '-':
                        {
                            next = expression[i + 1];
                            if (next == '(' || char.IsDigit(next))
                            {
                                if (stack.Count != 0)
                                {
                                    stack.Push(symbol);
                                    OrderSigns(ref stack);
                                }
                                else
                                    stack.Push(symbol);
                            }
                            else
                                output.Add(symbol);

                            break;
                        }
                    }
                }
            }

            while (stack.Count != 0)
            {
                output.Add(stack.Peek());
                stack.Pop();
            }

            return output;
        }

        private void CalculatingExpression(in List<char> expression)
        {
            int ConvertCharToDigit(char c) => c - '0';

            var resultStack = new Stack<int>();

            foreach (var symbol in expression)
            {
                if (char.IsDigit(symbol))
                    resultStack.Push(ConvertCharToDigit(symbol));
                else
                {
                    resultStack.Push(Perform(ref resultStack, symbol));
                }
            }

            Value = resultStack.Peek();
        }
    }

    // 3*(2+4*2)+((3-1)*2)
    // (2*2+3*2)
    internal class Program
    {
        public static void Main(string[] args)
        {
            var a = new Сalculator(Console.ReadLine());
            Console.WriteLine(a.Value);
        }
    }
}