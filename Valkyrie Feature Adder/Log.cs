using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class Log
    {
        public static string ReadLine(ConsoleColor inputColor)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = inputColor;
            string ret = Console.ReadLine();
            Console.ForegroundColor = current;

            return ret;
        }

        #region Write

        public static void Write(object message)
        {
            Write(message.ToString());
        }
        public static void Write(string message)
        {
            Console.Write(message);
        }

        public static void Write(object message, ConsoleColor color)
        {
            Write(message.ToString(), color);
        }
        public static void Write(string message, ConsoleColor color)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = current;
        }

        #endregion Write

        #region WriteLine

        public static void WriteLine() => Console.WriteLine();
        public static void WriteLine(object message)
        {
            WriteLine(message.ToString());
        }
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void WriteLine(object message, ConsoleColor color)
        {
            WriteLine(message.ToString(), color);
        }
        public static void WriteLine(string message, ConsoleColor color)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = current;
        }

        #endregion WriteLine
    }
}
