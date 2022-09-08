using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    public interface IOperationsBehavior
    {
        T PerformValue<T>(ref Stack<T> stack, char sign);
        T ConvertChar<T>(char symbol);
    }

    public interface IOperationPriority
    {
        (char, int) GetPriority(char c);
    }

    public abstract class Operations
    {
        private IOperationsBehavior _operationsBehavior;
        private IOperationPriority _operationsPriority;

        public void SetOperationBehavior(IOperationsBehavior operationsBehavior) =>
            _operationsBehavior = operationsBehavior;

        public void SetOperationPriority(IOperationsBehavior operationPriority) =>
            _operationsBehavior = operationPriority;

        public T PerformOperationBehavior<T>(ref Stack<T> stack, char sign) =>
            _operationsBehavior.PerformValue(ref stack, sign);
        public T PerformConvertValue<T>(char symbol) =>
            _operationsBehavior.ConvertChar<T>(symbol);

        protected (char, int) PerformOperationPriority(char c) => _operationsPriority.GetPriority(c);
    }

    public class Сalculator<T> : Operations
    {
        public T ValueExpression { get; private set; }

        public Сalculator(string expression)
        {
            ValueExpression = CalculatingExpression(GetOutputReversePolishNotation(expression));
        }

        private string GetOutputReversePolishNotation(string expression)
        {
            void DeleteWithReplacement(ref string s, ref Stack<char> stack)
            {
                s += stack.Peek();
                stack.Pop();
            }

            var polishNotation = "";
            var operations = new Stack<char>();

            foreach (var symbol in expression)
            {
                if (char.IsDigit(symbol))
                {
                    polishNotation += symbol;
                }

                else
                {
                    if (symbol == '(')
                    {
                        operations.Push(symbol);
                    }
                    else if (symbol == ')')
                    {
                        while (operations.Peek() != '(')
                            DeleteWithReplacement(ref polishNotation, ref operations);

                        operations.Pop();
                    }
                    else
                    {
                        while (operations.Count != 0 && PerformOperationPriority(operations.Peek()).Item2 >=
                               PerformOperationPriority(symbol).Item2)
                            DeleteWithReplacement(ref polishNotation, ref operations);

                        if (!(operations.Count != 0 && PerformOperationPriority(operations.Peek()).Item2 >=
                                PerformOperationPriority(symbol).Item2))
                            operations.Push(symbol);
                    }
                }
            }

            while (operations.Count != 0)
            {
                polishNotation += operations.Peek();
                operations.Pop();
            }

            return polishNotation;
        }

        private T CalculatingExpression(in string expression)
        {
            var resultStack = new Stack<T>();

            foreach (var symbol in expression)
            {
                if (char.IsDigit(symbol))
                    resultStack.Push(PerformConvertValue<T>(symbol));
                else
                {
                    resultStack.Push(PerformOperationBehavior(ref resultStack, symbol));
                }
            }

            return resultStack.Peek();
        }
    }

    // 3*(2+4*2)+((3-1)*2)
    // (2*2+3*2)
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var a = new Сalculator<T>("2*(1+5)+3*(6-4)*6+3*2+7-5*(2-(5+1*6))*3+2");
            //Console.WriteLine(a.ValueExpression);
        }
    }
}