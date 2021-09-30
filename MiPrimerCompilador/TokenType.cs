using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerCompilador
{
    public enum TokenType
    {
        Add = '+',
        Minus = '-',
        Mul = '*',
        Div = '/',
        RParen = ')',
        LParen = '(',
        EOF = (char)0,
        Number = (char)1
    }
}
