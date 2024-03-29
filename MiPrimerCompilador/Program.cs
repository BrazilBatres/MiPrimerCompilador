﻿using System;

namespace MiPrimerCompilador
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            bool esc = false;
            Console.WriteLine("Ingrese una expresión aritmética: ");
            while (!esc)
            {
                string regexp = Console.ReadLine();
                if (regexp != "E")
                {
                    try
                    {
                        double result = parser.Parse(regexp);
                        Console.WriteLine();
                        Console.WriteLine("Expresión OK");
                        Console.WriteLine("Resultado = " + result);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("Ingrese otra expresión (si desea terminar, presione E en su teclado): ");
                }
                else
                {
                    esc = true;
                }
            }
            
        }
    }
}
