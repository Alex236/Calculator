using System;
using System.Collections.Generic;

namespace Calculator
{
    class Calculate
    {
        private Queue<string> PostfixNotation = new Queue<string>();
        private List<string> IntfixNotation = new List<string>();
        private Stack<string> Digits = new Stack<string>();

        public string Evaluate(string str)
        {
            if (!CheckUserInputBySymbols(str)) return "Incorrect symbol";
            Spliter(str);
            if (!CheckDots()) return "Too many dots in one number";
            if (!CheckBreakets()) return "Incorrect breakets position";
            if (!CheckSymbolsPosition()) return "Incorrect symbol position";
            IntfixToPostfixNotation();
            EvaluationOfPostfixExpression();
            return Math.Round(Double.Parse(Digits.Pop()), 10).ToString();
        }

        private bool CheckDots()
        {
            foreach (var e in IntfixNotation)
            {
                if (e == "+" || e == "-" || e == "*" || e == "/" || e == "^" || e == "(" || e == ")") continue;
                else
                {
                    int counter = 0;
                    for (int i = 0; i < e.Length; i++)
                    {
                        if (e[i] == '.') counter++;
                    }
                    if (counter > 1) return false;
                }
            }
            return true;
        }

        private bool CheckSymbolsPosition()
        {
            bool lastWasDigit = false;
            foreach(var e in IntfixNotation)
            {
                if (e == "+" || e == "-" || e == "*" || e == "/" || e == "^")
                {
                    if(lastWasDigit) return false;
                    else lastWasDigit = true;
                }
                else
                {
                    if (e == "(" || e == ")") lastWasDigit = false;
                    else
                    {
                        if (Double.Parse(e) < 0 && lastWasDigit) return false;
                        else lastWasDigit = false;
                    }
                }
            }
            return true;
        }

        private bool CheckBreakets()
        {
            int counter = 0;
            foreach(var e in IntfixNotation)
            {
                if (e == "(") counter++;
                if (e == ")") counter--;
                if (counter < 0) return false;
            }
            return counter == 0;
        }

        private bool CheckUserInputBySymbols(string str)
        {
            str = str.Replace(" ", "");
            str = str.Replace(",", ".");
            char[] correctSymbols = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '+', '-', '*', '/', '^', '(', ')', '.'};
            for(int i = 0; i < str.Length; i++)
            {
                bool correctSymbol = false;
                for(int j = 0; j < correctSymbols.Length; j++)
                {
                    if (str[i] == correctSymbols[j]) correctSymbol = true;
                }
                if(!correctSymbol) return false;
            }
            return true;
        }

        private void EvaluationOfPostfixExpression()
        {
            foreach (var str in PostfixNotation)
            {
                if (Double.TryParse(str, out double temp))
                {
                    Digits.Push(str);
                }
                else
                {
                    double operand1 = double.Parse(Digits.Pop());
                    double operand2 = double.Parse(Digits.Pop());
                    switch (str)
                    {
                        case "+":
                            Digits.Push((operand2 + operand1).ToString());
                            break;
                        case "-":
                            Digits.Push((operand2 - operand1).ToString());
                            break;
                        case "*":
                            Digits.Push((operand2 * operand1).ToString());
                            break;
                        case "/":
                            Digits.Push((operand2 / operand1).ToString());
                            break;
                        case "^":
                            Digits.Push(Math.Pow(operand2, operand1).ToString());
                            break;
                    }
                }
            }
        }

        private void IntfixToPostfixNotation()
        {
            foreach (var str in IntfixNotation)
            {
                if (Double.TryParse(str, out double temp)) PostfixNotation.Enqueue(str);
                else
                {
                    if (str == "(")
                    {
                        if (Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "^")
                        {
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if (str == "+" || str == "-")
                    {
                        if (Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if (str == "*" || str == "/")
                    {
                        if (Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if (str == "^")
                    {
                        if (Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if (Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if (str == ")")
                    {
                        while (Digits.Peek() != "(")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                        }
                        Digits.Pop();
                    }
                }
            }
            while (Digits.Count != 0)
            {
                PostfixNotation.Enqueue(Digits.Pop());
            }
        }

        private void Spliter(string str)
        {
            str = str.Replace(" ", "");
            str = str.Replace(",", ".");
            string number = "";
            bool lastElementIsDigit = false;
            bool wasNumber = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '-' && !lastElementIsDigit)
                {
                    number += str[i].ToString();
                    continue;
                }
                if (Char.IsDigit(str[i]) || str[i] == '.')
                {
                    number += str[i].ToString();
                    lastElementIsDigit = true;
                    wasNumber = true;
                    if (i == str.Length - 1 && wasNumber) IntfixNotation.Add(number);
                    continue;
                }
                if (wasNumber)
                {
                    IntfixNotation.Add(number);
                    number = "";
                    wasNumber = false;
                    lastElementIsDigit = false;
                    IntfixNotation.Add(str[i].ToString());
                }
                else
                {
                    lastElementIsDigit = false;
                    IntfixNotation.Add(str[i].ToString());
                }
            }
        }
    }
}