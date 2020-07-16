using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDL_to_CSharp
{
    internal class DDLtoCSharpConverter
    {
        private readonly string TablePattern = "^CREATE TABLE \\[(\\w+)\\][.]\\[(\\w+)\\]\\($";
        private readonly string FieldPattern = "^\\s+\\[(\\w+)\\] \\[(\\w+)\\](?:\\((\\d+|max)(?:, (\\d+))?\\))?(?:\\s?)(?:IDENTITY\\(1,1\\))?(?:\\s?)(NOT NULL|NULL)?,?$";
        public readonly IDictionary<Match, IEnumerable<Match>> Bag = new Dictionary<Match, IEnumerable<Match>>();
        private Match match;

        internal ProcessResultEnum process(string line)
        {
            Match match = Regex.Match(line, FieldPattern);
            if (match.Success)
            {
                ((List<Match>)this.Bag[this.match]).Add(match);
                return ProcessResultEnum.Read;
            }
            else
            {
                match = Regex.Match(line, TablePattern);
                if (match.Success)
                {
                    this.match = match;
                    this.Bag.Add(match, new List<Match>());
                    return ProcessResultEnum.NewEntity;
                }
                else
                    return ProcessResultEnum.Ignore;
            }
        }
    }
}