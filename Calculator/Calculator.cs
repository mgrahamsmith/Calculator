using System;

namespace BankAccountNS
{
    /// <summary>
    /// Bank account demo class.
    /// </summary>
    public class Calculator
    {
        private int m_leftVal;
        private int m_rightVal;
        private string m_operator;
        private int m_result;

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

        public void calculate()
        {
            Console.Write("Calculating: " + m_leftVal + " " + m_operator + " " + m_rightVal + "...\n");
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

            Console.Write("    Result: " + m_result + "\n");
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

            Console.Write("Invalid operator: " + operator_str + ".  Expected: +,-,*,/ \n");
            return false;
        }

        private int strToInt(string inpStr)
        {
            int result = -1;
            try
            {
                result = Int32.Parse(inpStr);
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{inpStr}' to int.  Only integers are supported.");
                return -1;
            }
        }

        private int add(int val1, int val2)
        {
            return val1 + val2;
        }

        private int subtract(int val1, int val2)
        {
            return val1 - val2;
        }

        private int divide(int val1, int val2)
        {
            return val1 / val2;
        }

        private int multiply(int val1, int val2)
        {
            return val1 * val2;
        }

        public static void Main()
        {
            Calculator calc = new Calculator("10", "11", "+");
            calc.calculate();

            calc.setOperator("s");

            calc.setLeft("3");
            calc.setRight("4");
            calc.setOperator("*");
            calc.calculate();

            calc.setLeft("3.5");
            calc.setRight("4.3");
            calc.setOperator("+");
            calc.calculate();

            //calc.setLeft("8270394857234085720349857203498572304985273049852730495827340598237450298347520983475234");
            //calc.setRight("8270394857234085720342708457230495874502983349852730495827340598237450298347520986756777");
            //calc.setOperator("/");
            //calc.calculate();
            //Unhandled exception. System.OverflowException: Value was either too large or too small for an Int32.
            //   at System.Number.ThrowOverflowOrFormatException(ParsingStatus status, TypeCode type)
            //   at System.Number.ParseInt32(ReadOnlySpan`1 value, NumberStyles styles, NumberFormatInfo info)
            //   at System.Int32.Parse(String s)
            //   at BankAccountNS.Calculator.strToInt(String inpStr) in D:\repos\Calculator\Calculator\Program.cs:line 84
            //   at BankAccountNS.Calculator.setLeft(String numStr) in D:\repos\Calculator\Calculator\Program.cs:line 24
            //   at BankAccountNS.Calculator.Main() in D:\repos\Calculator\Calculator\Program.cs:line 131

        }
    }
}