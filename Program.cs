using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL_to_CSharp
{
    class Program
    {
        private readonly char REPLACEMENT = '\ufffd';


        static void Main(string[] args)
        {
            DDLtoCSharpConverter converter = new DDLtoCSharpConverter();
            bool willGenerateFile = (args.Length > 1 && args[1] == "-g");

            var stdin = Console.OpenStandardInput();
            string line;
            int count = 4282;
            while (stdin.CanRead && count > 0)
            {
                count--;

                line = Console.ReadLine();

                ProcessResultEnum result = converter.process(line);

                if (!willGenerateFile)
                    Console.Write("// ");
                Console.Write((ProcessResult)result);
                Console.Write(": ");
                Console.Write(line);
                Console.WriteLine();

            }

            string NameSpace = null;
            if (args.Length > 0)
                NameSpace = args[0].Replace('-', '_');
            var bag = converter.Bag.Select((kv) => { var cc = (ClassConversion)kv; cc.NameSpace = NameSpace; return cc; });

            if (willGenerateFile)
            {
                string directory = Directory.GetCurrentDirectory();
                foreach (var item in bag)
                    File.WriteAllText(directory + Path.DirectorySeparatorChar + item.TableName + ".cs", item.ToString());

                File.WriteAllText(directory + Path.DirectorySeparatorChar + "SqlTemplate.cs", new SqlTemplateClassGenerator(bag));
            }
            else
            {
                foreach (var item in bag)
                    Console.WriteLine(item.ToString());

                Console.WriteLine(new SqlTemplateClassGenerator(bag));
            }
        }

        public static void PrintCharacterInfo(char character)
        {
            Console.WriteLine($"{character}\t\t|{((int)character)}\t\t|\\u{((int)character).ToString("X4")}");
        }
    }
}
