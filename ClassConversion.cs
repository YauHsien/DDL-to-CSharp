using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL_to_CSharp
{
    internal class ClassConversion
    {
        private string schema;
        private string tableName;
        private IEnumerable<PropertyConversion> properties;

        public string TableName => tableName;
        public string NameSpace { get; set; }
        public string IndentString { get; set; }

        public ClassConversion(string schema, string tableName, IEnumerable<PropertyConversion> properties)
        {
            this.schema = schema;
            this.tableName = tableName;
            this.properties = properties;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            IndentString = "";
            builder.AppendLine("using System;");
            builder.AppendLine("using System.Numerics;");
            builder.AppendLine();
            if (NameSpace != null)
            {
                builder.Append("namespace ");
                builder.AppendLine(NameSpace);
                builder.AppendLine("{");
                IndentString = "    ";
            }

            builder.Append(IndentString);
            builder.Append("public class ");
            builder.Append(this.tableName);
            builder.Append(" : IDatabaseTable");

            builder.Append(IndentString);
            builder.AppendLine("{");

            builder.AppendLine();

            builder.Append(IndentString);
            builder.Append(IndentString);
            builder.Append("string IDatabaseTable.Schema => \"");
            builder.Append(this.schema);
            builder.AppendLine("\";");

            builder.Append(IndentString);
            builder.Append(IndentString);
            builder.Append("string IDatabaseTable.TableName => \"");
            builder.Append(this.tableName);
            builder.AppendLine("\";");

            builder.AppendLine();

            foreach (var property in properties)
            {
                property.IndentString = this.IndentString + this.IndentString;
                builder.AppendLine(property.ToString());
            }

            builder.AppendLine();

            builder.Append(IndentString);
            builder.AppendLine("}");

            if (NameSpace != null)
            {
                builder.Append("}");
            }

            return builder.ToString();
        }

        public static explicit operator ClassConversion(KeyValuePair<Match, IEnumerable<Match>> kv)
        {
            var tableMatch = kv.Key;
            var fieldMatches = kv.Value;

            var cls = new ClassConversion(
                tableMatch.Groups[1].Value,
                tableMatch.Groups[2].Value,
                fieldMatches.Select((match) => (PropertyConversion)match));

            return cls;
        }
    }
}