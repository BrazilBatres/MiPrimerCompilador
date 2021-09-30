using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerCompilador
{
    class Parser
    {
        Scanner _scanner;
        Token _token;
        public int Parse(string arithmeticExp)
        {
            int returnValue = -1;
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
        public int E()
        {
            int returnValue = -1;
            int Tvalue;
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
                    break;
            }
            return returnValue;
        }
        public int Ep(int leftOperand)
        {
            int returnValue = leftOperand;
            int Tvalue;
            int result;
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
        public int left_T()
        {
            int returnValue = -1;
            int Svalue;
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
                    break;
            }
            return returnValue;
        }
        public int left_Tp(int leftOperand)
        {
            int returnValue = leftOperand;
            int Svalue;
            int result=int.MaxValue;
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
                        Console.WriteLine("You can't divide by zero!");
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
        public int T()
        {
            int returnValue = -1;
            int Svalue;
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
        public int Tp(int leftOperand)
        {
            int returnValue = leftOperand;
            int Svalue;
            int Result;
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
        public int left_S()
        {
            int returnValue = -1;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    returnValue = F() * -1;
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
        public int S()
        {
            int returnValue = -1;
            switch (_token.Tag)
            {
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    returnValue = Sp();
                    break;
                case TokenType.Number:
                    returnValue = int.Parse(_token.Value);
                    Match(TokenType.Number);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;
        }
        public int Sp()
        {
            int returnValue = -1;
            int Fvalue;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    Fvalue = F();
                    Match(TokenType.RParen);
                    returnValue = Fvalue * -1;
                    break;
                case TokenType.LParen:
                case TokenType.Number:
                    returnValue = E();
                    Match(TokenType.RParen);
                    break;
                default:
                    throw new Exception("Syntax Error");
            }
            return returnValue;

        }
        public int F()
        {
            int returnValue = -1;
            switch (_token.Tag)
            {
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    returnValue = E();
                    Match(TokenType.RParen);
                    break;
                case TokenType.Number:
                    returnValue = int.Parse(_token.Value);
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
                throw new Exception("Error de sintaxis");
            }
        }

    }
}
