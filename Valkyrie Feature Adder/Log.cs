using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class Log
    {
        public const ConsoleColor ColorPrintInfo = ConsoleColor.Yellow;
        public const ConsoleColor ColorPrompt = ConsoleColor.Cyan;
        public const ConsoleColor ColorInput = ConsoleColor.White;
        public const ConsoleColor ColorMetaInfo = ConsoleColor.Gray;
        public const ConsoleColor ColorError = ConsoleColor.Red;
        public const ConsoleColor ColorErrorDetails = ConsoleColor.Yellow;

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

        /// <summary>
        /// Prompts the user to input the number corresponding to a value
        /// of a given enum type.
        /// </summary>
        /// <param name="enumType">The type of enum to prompt for.</param>
        /// <param name="maxExclusive">The exclusive maximum value (if any) to accept.</param>
        /// <returns>A number representing the enum value.</returns>
        public static int EnumPrompt(Type enumType, int? maxExclusive = null)
        {
            string prompt = $"Select {enumType.Name} value:";

            Log.WriteLine();
            Log.WriteLine(prompt, ColorPrompt);

            Array values = Enum.GetValues(enumType);

            int max = maxExclusive ?? int.MaxValue;
            max = Math.Min(max, values.Length);

            for(int i = 0; i < max; i++)
            {
                string enumName = values.GetValue(i).ToString();
                Log.WriteLine($"{i}. {enumName}", ColorPrintInfo);
            }
            Log.WriteLine();

            return ReadIntFromConsole(max);
        }

        /// <summary>
        /// Continues prompting the user to input an integer less than
        /// a given exclusive upper limit until a valid input is given.
        /// </summary>
        /// <param name="maxValueExclusive">The exclusive upper limit.</param>
        /// <returns>The number entered by the user.</returns>
        public static int ReadIntFromConsole(int maxValueExclusive)
        {
            string input;
            int ret;

            bool complete;
            do
            {
                input = Log.ReadLine(ColorInput);

                complete = int.TryParse(input, out ret);
                complete = complete && ret < maxValueExclusive;
            }
            while (!complete);

            return ret;
        }

        /// <summary>
        /// Continues prompting the user to input a line
        /// until a valid non-empty input is given.
        /// </summary>
        /// <param name="prompt">A prompt given to the user before their input.</param>
        /// <returns>The line input by the user entered by the user.</returns>
        public static string ReadStringFromConsole(string prompt)
        {
            Log.WriteLine();
            if (!String.IsNullOrEmpty(prompt))
                Log.WriteLine(prompt, ColorPrompt);

            return ReadStringFromConsole();
        }

        /// <summary>
        /// Continues prompting the user to input a line
        /// until a valid non-empty input is given.
        /// </summary>
        /// <returns>The line input by the user entered by the user.</returns>
        public static string ReadStringFromConsole()
        {
            string input;

            do
            {
                input = Log.ReadLine(ColorInput);
            }
            while (String.IsNullOrEmpty(input));

            return input;
        }
    }
}
