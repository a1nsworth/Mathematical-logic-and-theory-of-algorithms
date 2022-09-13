using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    // 3*(2+4*2)+((3-1)*2)
    // (2*2+3*2)
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cal = new LogicalСalculator("X*Y*!Y+!Z*X*Z*!Y+X*!X");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("F:=>");
            foreach (var element in cal.ValueExpression)
            {
                Console.WriteLine($"| {(element ? 1 : 0)} |");
            }


            Console.Write('\n');

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("Формула противоречива ? " + (cal.ValueExpression.AllAreFalse() ? "Да" : "Нет"));
            Console.Write('\n');
            Console.ForegroundColor = ConsoleColor.Green;

            for (var i = 0; i < cal.ValueExpression.Count; i++)
            {
                if (cal.ValueExpression[i])
                {
                    foreach (var value in InputManager._variables)
                    {
                        Console.Write($"{value.Key} ");
                        Console.WriteLine($"{value.Value[i]}");
                    }

                    Console.WriteLine("---------");
                }
            }

            Console.ResetColor();
        }
    }
}