using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerCompilador
{
    public struct OpNode
    {
        public int leftOperand;
        public int Operate(int rightOperand, char opSign)
        {
            int returnValue = 0;
            switch (opSign)
            {
                case '+':
                    returnValue = leftOperand + rightOperand;
                    break;
                case '-':
                    returnValue = leftOperand - rightOperand;
                    break;
                case '*':
                    returnValue = leftOperand * rightOperand;
                    break;
                case '/':
                    returnValue = leftOperand / rightOperand;
                    break;
                default:
                    break;
            }
            return returnValue;
        }
    }
}
