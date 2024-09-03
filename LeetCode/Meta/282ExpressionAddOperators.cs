using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _282ExpressionAddOperators
    {
        public IList<string> AddOperators(string num, int target)
        {
            var res = new List<string>();
            var firstOperand = new LinkedList<long>();
            firstOperand.AddLast(num[0] - '0');
            var stack = new Stack<(int, LinkedList<char>, LinkedList<long>)>();
            stack.Push((0, new LinkedList<char>(), firstOperand));
            while (stack.Count != 0)
            {
                for (int c = stack.Count; c > 0; --c)
                {
                    var node = stack.Pop();
                    int index = node.Item1;
                    var operators = node.Item2;
                    var operands = node.Item3;
                    if (index == num.Length - 1)
                    {
                        if (Evaluate(operators, operands, out string expression) == target)
                        {
                            res.Add(expression);
                        }
                    }
                    else
                    {
                        if (operands.Count != 0 && operands.Last() != 0)
                        {
                            var p = new LinkedList<long>(operands);
                            p.Last.Value = p.Last.Value * 10 + (num[index + 1] - '0');
                            stack.Push((index + 1, operators, p));
                        }

                        var add = new LinkedList<char>(operators);
                        add.AddLast('+');
                        var parameters = new LinkedList<long>(operands);
                        parameters.AddLast(num[index + 1] - '0');
                        stack.Push((index + 1, add, parameters));

                        var sub = new LinkedList<char>(operators);
                        sub.AddLast('-');
                        stack.Push((index + 1, sub, parameters));

                        var mul = new LinkedList<char>(operators);
                        mul.AddLast('*');
                        stack.Push((index + 1, mul, parameters));
                    }
                }
            }

            return res;
        }

        public IList<string> AddOperatorsv1(string num, int target)
        {
            var res = new List<string>();
            var operands = new LinkedList<long>();
            operands.AddLast(num[0] - '0');
            Append(num, 0, new LinkedList<char>(), operands, res, target);
            return res;
        }

        private void Append(string num, int index, LinkedList<char> operators, LinkedList<long> operands, IList<string> res, int target)
        {
            if (index == num.Length - 1)
            {
                if (Evaluate(operators, operands, out string expression) == target)
                {
                    res.Add(expression);
                }
            }
            else
            {
                if (operands.Count != 0 && operands.Last() != 0)
                {
                    var p = new LinkedList<long>(operands);
                    p.Last.Value = p.Last.Value * 10 + (num[index + 1] - '0');
                    Append(num, index + 1, operators, p, res, target);
                }

                var add = new LinkedList<char>(operators);
                add.AddLast('+');
                var parameters = new LinkedList<long>(operands);
                parameters.AddLast(num[index + 1] - '0');
                Append(num, index + 1, add, parameters, res, target);

                var sub = new LinkedList<char>(operators);
                sub.AddLast('-');
                Append(num, index + 1, sub, parameters, res, target);

                var mul = new LinkedList<char>(operators);
                mul.AddLast('*');
                Append(num, index + 1, mul, parameters, res, target);
            }
        }

        private long Evaluate(LinkedList<char> operators, LinkedList<long> operands, out string expression)
        {
            StringBuilder sb = new StringBuilder();
            var operand = operands.First;
            sb.Append(operand.Value);
            long prevValue = operand.Value;
            long? curValue = null;
            var _operator = operators.First;
            operand = operand.Next;

            while (operand != null)
            {
                sb.Append(_operator.Value);
                sb.Append(operand.Value);

                if (_operator.Value == '*')
                {
                    if (curValue != null)
                    {
                        curValue *= operand.Value;
                    }
                    else
                        prevValue *= operand.Value;
                }
                else
                {
                    if (curValue != null)
                        prevValue += curValue.Value;

                    curValue = operand.Value * (_operator.Value == '+' ? 1 : -1);
                }

                _operator = _operator.Next;
                operand = operand.Next;
            }

            expression = sb.ToString();
            return prevValue + (curValue.HasValue ? curValue.Value : 0);
        }

        private long EvaluateV1(LinkedList<char> operators, LinkedList<long> operands, out string expression)
        {
            StringBuilder sb = new StringBuilder();
            Stack<long> stack = new Stack<long>(operands.Count);
            var operand = operands.First;
            stack.Push(operand.Value);
            sb.Append(operand.Value);
            var _operator = operators.First;
            operand = operand.Next;

            while (operand != null)
            {
                if (_operator != null)
                {
                    sb.Append(_operator.Value);
                    sb.Append(operand.Value);

                    if (_operator.Value == '*')
                        stack.Push(stack.Pop() * operand.Value);
                    else
                        stack.Push(operand.Value * (_operator.Value == '+' ? 1 : -1));

                    _operator = _operator.Next;
                }

                operand = operand.Next;
            }

            long result = 0;
            while (stack.Count != 0)
            {
                result += stack.Pop();
            }

            expression = sb.ToString();
            return result;
        }
    }
}
