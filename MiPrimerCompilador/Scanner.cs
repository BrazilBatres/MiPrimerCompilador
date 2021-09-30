using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerCompilador
{
    class Scanner
    {
        private string _regexp = "";
        private int _index = 0;
        private int _state = 0;
        public Scanner(string regexp)
        {
            _regexp = regexp + (char)TokenType.EOF;
            _index = 0;
            _state = 0;
        }
        public Token GetToken()
        {
            Token result = new Token() { Value = ((char)0).ToString() };
            bool tokenFound = false;
            _state = 0;
            while (!tokenFound)
            {
                char peek = _regexp[_index];
                switch (_state)
                {
                    case 0:
                        //whitespace removal
                        while (char.IsWhiteSpace(peek))
                        {
                            peek = _regexp[_index];
                            _index++;
                        }
                        switch (peek)
                        {
                            case (char)TokenType.Add:
                            case (char)TokenType.Div:
                            case (char)TokenType.EOF:
                            case (char)TokenType.LParen:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Mul:
                            case (char)TokenType.RParen:
                                tokenFound = true;
                                result.Tag = (TokenType)peek;
                                break;
                            default:
                                if (char.IsDigit(peek))
                                {
                                    _state = 1;
                                    result.Tag = TokenType.Number;
                                    result.Value = peek.ToString();
                                }
                                else
                                {
                                    throw new Exception("Lex Error");
                                }
                                break;
                        }// SWITCH - peek

                        break;
                    case 1:
                        switch (peek)
                        {
                            case (char)TokenType.Add:
                            case (char)TokenType.Div:
                            case (char)TokenType.EOF:
                            case (char)TokenType.LParen:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Mul:
                            case (char)TokenType.RParen:
                                tokenFound = true;
                                _index--;
                                break;
                            default:
                                if (char.IsDigit(peek))
                                {
                                    result.Value += peek.ToString();
                                }
                                else
                                {
                                    throw new Exception("Lex Error");
                                }
                                break;
                        }
                        break;//CASE state1
                    default:
                        break;
                }// SWITCH state
                _index++;
            }// WHILE - tokenfound
            return result;
        }// GetToken
    }
}
