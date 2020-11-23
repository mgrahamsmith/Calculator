using System;
using System.Collections.Generic;

namespace CalculatorNS
{
    /// <summary>
    /// Bank account demo class.
    /// </summary>
    public class Calculator
    {
        private enum Status
        {
            GOOD,
            DIVIDE_BY_ZERO,
            INVALID_INPUT,
            OVERFLOW,
            PARSE_ERROR,
            UNKOWN
        }

        private int m_leftVal;
        private int m_rightVal;
        private string m_operator;
        private int m_result;
        private Status m_status = Status.GOOD;

        // Class and Methods for Calculator Project Part 1 /////////////////////
        public Calculator()
        {
            setLeft("0");
            setRight("1");
            setOperator("+");
        }

        public Calculator(string leftVal, string rightVal, string optr)
        {
            setLeft(leftVal);
            setRight(rightVal);
            setOperator(optr);
        }

        public void setLeft(string numStr)
        {
            m_leftVal = strToInt(numStr);
        }

        public void setRight(string numStr)
        {
            m_rightVal = strToInt(numStr);
        }

        public void setOperator(string operator_str)
        {
            if (isOperator(operator_str))
            {
                m_operator = operator_str;
            }
            else
            {
                Console.Write("Invalid operator: " + operator_str + ".  Expected: +,-,*,/\n");
                m_status = Status.INVALID_INPUT;
            }

        }

        public int calculate()
        {
            Console.Write("Calculating: " + m_leftVal + " " + m_operator + " " + m_rightVal + "...\n");
            if (m_status != Status.GOOD)
            {
                return -1;
            }

            if (String.Equals(m_operator, "+"))
            {
                m_result = add(m_leftVal, m_rightVal);
            }
            else if (String.Equals(m_operator, "-"))
            {
                m_result = subtract(m_leftVal, m_rightVal);
            }
            else if (String.Equals(m_operator, "*"))
            {
                m_result = multiply(m_leftVal, m_rightVal);
            }
            else if (String.Equals(m_operator, "/"))
            {
                m_result = divide(m_leftVal, m_rightVal);
            }

            if (m_status != Status.GOOD)
            {
                return -1;
            }

            return m_result;
        }

        public string getResult()
        {
            string result = "";
            if (m_status == Status.GOOD)
            {
                result = m_result.ToString();
            }
            else if (m_status == Status.DIVIDE_BY_ZERO)
            {
                result = "ERROR: " + Status.DIVIDE_BY_ZERO.ToString();
            }
            else if (m_status == Status.OVERFLOW)
            {
                result = "ERROR: " + Status.OVERFLOW.ToString();
            }
            else if (m_status == Status.INVALID_INPUT)
            {
                result = "ERROR: " + Status.INVALID_INPUT.ToString();
            }
            else
            {
                result = "ERROR: " + Status.UNKOWN.ToString();
            }

            Console.Write("  Result: " + result + "\n");
            return result;
        }

        public string getPreviousResult(int index)
        {
            int min = 0;
            int max = 10;
            if (index < min || index > max)
            {
                string errMsg = $"ERROR: Invalid index.  Must be a value in the range {min}-{max}";
                Console.Write(errMsg);
                throw new System.IndexOutOfRangeException(errMsg);
            }

            throw new System.NotImplementedException("Implementation not completed.");
        }


        // Methods for Calculator Project Part 2 ///////////////////////////////
        public string parseInput(string inputStr)
        { 
            string parsedResult = "";

            char[] parsedChars = inputStr.ToCharArray();
            Queue<char> operatorChars = new Queue<char>();  // Queue for operators
            Queue<int> numbers = new Queue<int>();          // Queue for Int32 operands

            for (int i=0; i<parsedChars.Length; i++)
            {
                string numStr = "";
                char tmpChar = parsedChars[i];

                int opPrec = getPrecedence(tmpChar);

                if (opPrec != -1)
                {
                    // If we reached an operator, check numStr to see if we
                    // need to enqueu on the number Queue.
                    if (!String.Equals(numStr, ""))
                    {
                        numbers.Enqueue(strToInt(numStr));
                        // Reset numStr
                        numStr = "";
                    }

                    if (opPrec > getPrecedence(operatorChars.Peek()))
                    {

                    }
                    operatorChars.Enqueue(tmpChar);
                }
                else if (System.Char.IsDigit(tmpChar))
                {
                    numStr += tmpChar;
                }
                else
                {
                    string errMsg = $"ERROR: Invalid input string.  Expected expression with no spaces" +
                        $", including only 0-9,+,-,*, or /.  Ex: '10-2*6/4'";
                    Console.Write(errMsg);
                    m_status = Status.PARSE_ERROR;
                }
                
            }

            return parsedResult;
        }

        // Helper methods //////////////////////////////////////////////////////

        // Return true if operator is valid ("+", "-", "*", "/")
        private bool isOperator(string operator_str)
        {
            string[] expectedOps = { "+", "-", "*", "/" };
            for (int i = 0; i < expectedOps.Length; i += 1)
            {
                if (String.Equals(operator_str, expectedOps[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // Return true if operator is valid ('+', '-', '*', '/')
        // Returns -1 if the operator is not valid
        private int getPrecedence(char operator_str)
        {
            int precedence = -1;
            if (String.Equals(operator_str, '+'))
            {
                precedence = 0;
            }
            else if (String.Equals(operator_str, '-'))
            {
                precedence = 0;
            }
            else if (String.Equals(operator_str, '*'))
            {
                precedence = 1;
            }
            else if (String.Equals(operator_str, '/'))
            {
                precedence = 1;
            }

            return precedence;
        }

        // Converts string input to Int32
        private int strToInt(string inpStr)
        {
            int result = -1;
            try
            {
                result = Int32.Parse(inpStr);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"  ERROR: Unable to parse '{inpStr}' to int.  Only integers are supported.  '{e}'");
                m_status = Status.INVALID_INPUT;
            }
            catch (System.OverflowException e)
            {

                Console.WriteLine($"  ERROR: Unable to parse '{inpStr}' to int.  '{e}'");
                m_status = Status.OVERFLOW;
            }

            return result;
        }

        // Add two Int32 values
        private int add(int val1, int val2)
        {
            int result = -1;

            try
            {
                result = val1 + val2;
            }
            catch (System.OverflowException e)
            {

                Console.WriteLine($"  ERROR: Unable to add values.  '{e}'");
                m_status = Status.INVALID_INPUT;
            }

            return result;
        }

        // Subtract two Int32 values
        private int subtract(int val1, int val2)
        {
            return val1 - val2;
        }

        // Divide two Int32 values
        private int divide(int val1, int val2)
        {
            if (val2 == 0)
            {
                Console.Write("  Error: cannot not divide by zero.\n");
                m_status = Status.DIVIDE_BY_ZERO;
                return 0;
            }
            return val1 / val2;
        }

        // Multipy two Int32 values
        private int multiply(int val1, int val2)
        {
            return val1 * val2;
        }

        // 

        // Main
        public static void Main()
        {
            Console.Write("Time to calculate.");
            // Arrange
            Calculator calc = new Calculator();

            // Action
            string inputStr = "3+1+4/6*6*8/3-4*4+7";
            string actual = calc.parseInput(inputStr);
        }
    }
}