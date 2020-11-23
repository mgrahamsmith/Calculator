using System;

namespace BankAccountNS
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
            OVERFLOW
        }

        private int m_leftVal;
        private int m_rightVal;
        private string m_operator;
        private int m_result;
        private Status m_status = Status.GOOD;

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
                result = "An error occured: " + Status.DIVIDE_BY_ZERO.ToString();
            }
            else if (m_status == Status.OVERFLOW)
            {
                result = "An error occured: " + Status.OVERFLOW.ToString();
            }
            else if (m_status == Status.INVALID_INPUT)
            {
                result = "An error occured: " + Status.INVALID_INPUT.ToString();
            }
            else
            {
                result = "An unkown error occured.";
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
            return false;
        }

        private int strToInt(string inpStr)
        {
            int result = -1;
            try
            {
                result = Int32.Parse(inpStr);
            }
            catch (FormatException)
            {
                Console.WriteLine($"  ERROR: Unable to parse '{inpStr}' to int.  Only integers are supported.");
                m_status = Status.INVALID_INPUT;
            }
            
            return result;
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
            string result;
            string expected;

            Calculator calc = new Calculator("10", "11", "+");
            
            calc.setLeft("9");
            calc.setRight("1");
            calc.setOperator("+");
            calc.calculate();
            expected = "10";
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            } else
            {
                Console.Write(" -- FAIL.. Expected: " + expected + ", calculated: " + result + "\n");
            }

            calc.setLeft("4");
            calc.setRight("-7");
            calc.setOperator("-");
            calc.calculate();
            expected = "11";
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: " + expected + ", calculated: " + result + "\n");
            }

            calc.setLeft("8");
            calc.setRight("8");
            calc.setOperator("*");
            calc.calculate();
            expected = "64";
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: " + expected + ", calculated: " + result + "\n");
            }

            calc.setLeft("12");
            calc.setRight("3");
            calc.setOperator("/");
            calc.calculate();
            expected = "4";
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: " + expected + ", calculated: " + result + "\n");
            }

            calc.setLeft("256");
            calc.setRight("0");
            calc.setOperator("/");
            calc.calculate();
            expected = null;
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: ERROR, calculated: " + result + "\n");
            }

            calc.setLeft("");
            calc.setRight("423");
            calc.setOperator("+");
            calc.calculate();
            expected = null;
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: ERROR, calculated: " + result + "\n");
            }

            calc.setLeft("foo");
            calc.setRight("14");
            calc.setOperator("+");
            calc.calculate();
            expected = null;
            result = calc.getResult();
            if (String.Equals(expected, result))
            {
                Console.Write(" -- PASS\n");
            }
            else
            {
                Console.Write(" -- FAIL.. Expected: ERROR, calculated: " + result + "\n");
            }

            //calc.setLeft("8270394857234085720349857203498572304985273049852730495827340598237450298347520983475234");
            //calc.setRight("8270394857234085720342708457230495874502983349852730495827340598237450298347520986756777");
            //calc.setOperator("/");
            //calc.calculate();
            //Unhandled exception. System.OverflowException: Value was either too large or too small for an Int32.
            //   at System.Number.ThrowOverflowOrFormatException(Parsingm_status m_status, TypeCode type)
            //   at System.Number.ParseInt32(ReadOnlySpan`1 value, NumberStyles styles, NumberFormatInfo info)
            //   at System.Int32.Parse(String s)
            //   at BankAccountNS.Calculator.strToInt(String inpStr) in D:\repos\Calculator\Calculator\Program.cs:line 84
            //   at BankAccountNS.Calculator.setLeft(String numStr) in D:\repos\Calculator\Calculator\Program.cs:line 24
            //   at BankAccountNS.Calculator.Main() in D:\repos\Calculator\Calculator\Program.cs:line 131

        }
    }
}