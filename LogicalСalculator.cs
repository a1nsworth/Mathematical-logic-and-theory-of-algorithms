using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    public enum TypeOperation
    {
        Unary,
        Binary,
        OpenBracket
    }

    // public interface IOperationsBehavior
    // {
    //     T PerformValue<T>(ref Stack<T> stack, char sign);
    //     T ConvertChar<T>(char symbol);
    //     bool IsOperation(char c);
    // }
    //
    // public interface IOperationsPriority
    // {
    //     (TypeOperation, int) GetPriority(char c);
    // }
    //
    // public abstract class Operations
    // {
    //     private IOperationsBehavior _operationsBehavior;
    //     private IOperationsPriority _operationsPriority;
    //
    //     public void SetOperationBehavior(IOperationsBehavior operationsBehavior) =>
    //         _operationsBehavior = operationsBehavior;
    //
    //     public void SetOperationPriority(IOperationsBehavior operationsPriority) =>
    //         _operationsBehavior = operationsPriority;
    //
    //     protected T PerformOperationBehavior<T>(ref Stack<T> stack, char sign) =>
    //         _operationsBehavior.PerformValue(ref stack, sign);
    //
    //     protected T PerformConvertValue<T>(char symbol) =>
    //         _operationsBehavior.ConvertChar<T>(symbol);
    //
    //     protected (TypeOperation, int) PerformOperationPriority(char c) => _operationsPriority.GetPriority(c);
    //
    //     protected bool IsRequiredCharacter(char c) => _operationsBehavior.IsOperation(c);
    // }

    public class LogicalСalculator
    {
        public LogicArray ValueExpression { get; private set; }

        public LogicalСalculator(string expression)
        {
            InputManager.ReadExpression(expression);

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
                if (VariableStorage.IsContain(symbol))
                {
                    polishNotation += symbol;
                }

                else
                {
                    if (symbol == '(')
                        operations.Push(symbol);
                    else if (symbol == ')')
                    {
                        while (operations.Peek() != '(')
                            DeleteWithReplacement(ref polishNotation, ref operations);

                        operations.Pop();
                    }
                    else
                    {
                        while (operations.Count != 0 && LogicalOperations.GetPriority(operations.Peek()).Item2 >=
                               LogicalOperations.GetPriority(symbol).Item2)
                            DeleteWithReplacement(ref polishNotation, ref operations);

                        if (!(operations.Count != 0 && LogicalOperations.GetPriority(operations.Peek()).Item2 >=
                                LogicalOperations.GetPriority(symbol).Item2))
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

        private LogicArray CalculatingExpression(in string expression)
        {
            var resultStack = new Stack<LogicArray>();

            foreach (var symbol in expression)
            {
                resultStack.Push(LogicalOperations.IsOperation(symbol)
                    ? LogicArray.Perform(ref resultStack, symbol)
                    : InputManager.GetVariable(symbol));
            }

            return resultStack.Peek();
        }
    }

    // 3*(2+4*2)+((3-1)*2)
    // (2*2+3*2)
}