using System;
using System.Text.RegularExpressions;

namespace DDL_to_CSharp
{
    internal class DDLtoCSharpConverter
    {
        private readonly string TablePattern = "^CREATE TABLE \\[(\\w+)\\][.]\\[(\\w+)\\]\\($";
        private readonly string FieldPattern = "^\\s+\\[(\\w+)\\] \\[(\\w+)\\](?:\\((\\d+|max)(?:, (\\d+))?\\))?(?:\\s?)(?:IDENTITY\\(1,1\\))?(?:\\s?)(NOT NULL|NULL)?,?$";
        internal ProcessResultEnum process(string line)
        {
            Match match = Regex.Match(line, FieldPattern);
            if (match.Success)
            {
                return ProcessResultEnum.Read;
            }
            else
            {
                match = Regex.Match(line, TablePattern);
                if (match.Success)
                {
                    return ProcessResultEnum.NewEntity;
                }
                else
                    return ProcessResultEnum.Ignore;
            }
        }
    }
}