using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerCompilador
{
    class Parser
    {
        Scanner _scanner;
        Token _token;
        public double Parse(string arithmeticExp)
        {
            double returnValue = -1;
            _scanner = new Scanner(arithmeticExp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Number:
                    returnValue = E();
                    break;
                default:
                    break;
            }
            Match(TokenType.EOF);
            return returnValue;
        }
        public double E()
        {
            double returnValue = -1;
            double Tvalue;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Number:
                    Tvalue = left_T();
                    returnValue = Ep(Tvalue);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double Ep(double leftOperand)
        {
            double returnValue = leftOperand;
            double Tvalue;
            double result;
            switch (_token.Tag)
            {
                case TokenType.Add:
                    Match(TokenType.Add);
                    Tvalue = T();
                    //Operación con número izquierdo (calculado previamente)
                    result = leftOperand + Tvalue;
                    //Envío del valor calculado como parámetro
                    returnValue = Ep(result);
                    break;
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    Tvalue = T();
                    //Operación con número izquierdo (calculado previamente)
                    result = leftOperand - Tvalue;
                    //Envío del valor calculado como parámetro
                    returnValue = Ep(result);
                    break;
                case TokenType.RParen:
                case TokenType.EOF:
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double left_T()
        {
            double returnValue = -1;
            double Svalue;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Number:
                    Svalue = left_S();
                    returnValue = left_Tp(Svalue);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double left_Tp(double leftOperand)
        {
            double returnValue = leftOperand;
            double Svalue;
            double result=0;
            switch (_token.Tag)
            {
                case TokenType.Mul:
                    Match(TokenType.Mul);
                    Svalue = S();
                    //Operación con número izquierdo (calculado previamente)
                    result = leftOperand * Svalue;
                    //Envío del valor calculado como parámetro
                    returnValue = left_Tp(result);
                    break;
                case TokenType.Div:
                    Match(TokenType.Div);
                    Svalue = S();
                    //Operación con número izquierdo (calculado previamente)
                    if (Svalue != 0) result = leftOperand / Svalue;
                    else
                    {
                        throw new Exception("No se puede dividir entre cero, resultado indefinido");
                    }
                    //Envío del valor calculado como parámetro
                    returnValue = left_Tp(result);
                    break;
                case TokenType.RParen:
                case TokenType.Add:
                case TokenType.Minus:
                case TokenType.EOF:
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double T()
        {
            double returnValue = -1;
            double Svalue;
            switch (_token.Tag)
            {
                case TokenType.LParen:
                case TokenType.Number:
                    Svalue = S();
                    returnValue = Tp(Svalue);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double Tp(double leftOperand)
        {
            double returnValue = leftOperand;
            double Svalue;
            double Result;
            switch (_token.Tag)
            {
                case TokenType.Mul:
                    Match(TokenType.Mul);
                    Svalue = S();
                    //Operación con número izquierdo (calculado previamente)
                    Result = leftOperand * Svalue;
                    //Envío del valor calculado como parámetro
                    returnValue = Tp(Result);
                    break;
                case TokenType.Div:
                    Match(TokenType.Div);
                    Svalue = S();
                    //Operación con número izquierdo (calculado previamente)
                    Result = leftOperand / Svalue;
                    //Envío del valor calculado como parámetro
                    returnValue = Tp(Result);
                    break;
                case TokenType.RParen:
                case TokenType.Add:
                case TokenType.Minus:
                case TokenType.EOF:
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double left_S()
        {
            double returnValue = -1;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    returnValue = S() * -1;
                    break;
                case TokenType.LParen:
                case TokenType.Number:
                    returnValue = S();
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public double S()
        {
            double returnValue = -1;
            switch (_token.Tag)
            {
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    returnValue = E();
                    Match(TokenType.RParen);
                    break;
                case TokenType.Number:
                    returnValue = double.Parse(_token.Value);
                    Match(TokenType.Number);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public void Match(TokenType tag)
        {
            if (_token.Tag == tag)
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception("Syntax Error");
            }
        }

    }
}
