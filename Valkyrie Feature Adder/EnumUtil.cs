using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public enum Feature
    {
        Bullet,
        Powerup,
        Enemy
    }

    public enum Bullet
    {
        BulletWithFireStrategy,
        AdditionalBullet,
    }

    public enum Powerup
    {
        OnFire,
        OnGetHit,
        OnHit,
        OnKill,
        OnLevelUp,
        Passive,
    }

    public enum Enemy
    {
        LoopingVariantFireStrategyEnemy,
        CustomFireStrategyEnemy,
        NoFireStrategyEnemy
    }

    public static class EnumUtil
    {
        public const ConsoleColor ColorDefault = ConsoleColor.Cyan;
        public const ConsoleColor ColorSql = ConsoleColor.Cyan;
        public const ConsoleColor ColorConfirm = ConsoleColor.Gray;
        public const ConsoleColor ColorCancel = ConsoleColor.Red;
        public const ConsoleColor ColorPrintInfo = ConsoleColor.Yellow;
        public const ConsoleColor ColorLinesToAdd = ConsoleColor.White;
        public const ConsoleColor ColorBackup = ConsoleColor.DarkGray;
        public const ConsoleColor ColorPrompt = ConsoleColor.Cyan;
        public const ConsoleColor ColorInstruction = ConsoleColor.Cyan;
        public const ConsoleColor ColorNumber = ConsoleColor.Yellow;
        public const ConsoleColor ColorInput = ConsoleColor.White;
        public const ConsoleColor ColorBold = ConsoleColor.Magenta;

        public static int ReadNumberSelection(string prompt, Type enumType)
        {
            Log.WriteLine();
            Log.WriteLine(prompt, ColorPrompt);

            Array values = Enum.GetValues(enumType);

            foreach(var e in values)
            {
                int value = (int)e;
                string enumName = Enum.GetName(enumType, e);

                Log.WriteLine($"{value}. {enumName}", ColorNumber);
            }
            Log.WriteLine();

            return ReadIntFromConsole(values.Length);
        }

        public static int ReadIntFromConsole(int maxValueExclusive)
        {
            string input;
            int ret;

            bool complete;
            do
            {
                input = Log.ReadLine(ColorInput);;

                complete = int.TryParse(input, out ret);
                complete = complete && ret < maxValueExclusive;
            }
            while (!complete);

            return ret;
        }

        public static string ReadStringFromConsole(string prompt, string prefix = "")
        {
            Log.WriteLine();
            if (!String.IsNullOrEmpty(prompt))
                Log.WriteLine(prompt, ColorPrompt);

            string input;

            do
            {
                Log.Write(prefix, ColorInput);
                input = Log.ReadLine(ColorInput);
            }
            while (String.IsNullOrEmpty(input));

            string ret = $"{prefix}{input}";
            return input;
        }
    }
}
