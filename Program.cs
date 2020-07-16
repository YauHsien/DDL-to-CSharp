using System;
using System.Text;

namespace DDL_to_CSharp
{
    class Program
    {
        private readonly char REPLACEMENT = '\ufffd';


        static void Main(string[] args)
        {
            DDLtoCSharpConverter converter = new DDLtoCSharpConverter();

            var stdin = Console.OpenStandardInput();
            string line;
            int count = 4282;
            while (stdin.CanRead && count > 0)
            {
                count--;

                line = Console.ReadLine();

                ProcessResultEnum result = converter.process(line);

                Console.Write((ProcessResult)result);
                Console.Write(": ");
                Console.Write(line);
                Console.WriteLine();

            }
        }

        public static void PrintCharacterInfo(char character)
        {
            Console.WriteLine($"{character}\t\t|{((int)character)}\t\t|\\u{((int)character).ToString("X4")}");
        }
    }
}
