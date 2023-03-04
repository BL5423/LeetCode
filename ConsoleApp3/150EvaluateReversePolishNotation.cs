using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _150EvaluateReversePolishNotation
    {
        public int EvalRPN(string[] tokens)
        {
            Stack<int> operands = new Stack<int>();
            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        int operand1 = operands.Pop();
                        int operand2 = operands.Pop();
                        if (token == "+")
                            operands.Push(operand2 + operand1);
                        if (token == "-")
                            operands.Push(operand2 - operand1);
                        if (token == "*")
                            operands.Push(operand2 * operand1);
                        if (token == "/")
                            operands.Push(operand2 / operand1);
                        break;

                    default:
                        operands.Push(int.Parse(token));
                        break;
                }
            }

            return operands.Peek();
        }
    }
}
