using System;

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
            UNKOWN
        }

        private int m_leftVal;
        private int m_rightVal;
        private string m_operator;
        private int m_result;
        private Status m_status = Status.GOOD;

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
            if (validOperator(operator_str))
            {
                m_operator = operator_str;
            }
        }
        public string parseInput(string inputStr)
        {
            return inputStr;
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

        private bool validOperator(string operator_str)
        {
            string[] expectedOps = { "+", "-", "*", "/" };

            for (int i = 0; i < expectedOps.Length; i += 1)
            {
                if (String.Equals(operator_str, expectedOps[i]))
                {
                    return true;
                }
            }

            Console.Write("Invalid operator: " + operator_str + ".  Expected: +,-,*,/\n");
            m_status = Status.INVALID_INPUT;
            return false;
        }

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

        private int subtract(int val1, int val2)
        {
            return val1 - val2;
        }

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

        private int multiply(int val1, int val2)
        {
            return val1 * val2;
        }

        public static void Main()
        {
            Calculator calc = new Calculator();
        }
    }
}