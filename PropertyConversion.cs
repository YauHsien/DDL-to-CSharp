using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL_to_CSharp
{
    internal class PropertyConversion
    {
        private string fieldName;
        private string typeName;
        private string typeParam;
        private dynamic typeParam2;
        private dynamic nullable;

        public string IndentString { get; set; }
        public string PropertyType
        {
            get
            {
                if ("int".Equals(typeName))
                    return "int";
                if ("smallint".Equals(typeName))
                    return "short";
                if ("bigint".Equals(typeName))
                    return "BigInteger";
                if ("tinyint".Equals(typeName))
                    return "byte";
                if ("bit".Equals(typeName))
                    return "bool";
                if ("tinyint".Equals(typeName))
                    return "byte";
                if ("numeric".Equals(typeName))
                    return "decimal";
                if ("decimal".Equals(typeName))
                    return "double";
                if ("money".Equals(typeName))
                    return "decimal";
                if ("smallmoney".Equals(typeName))
                    return "decimal";
                if ("datetime".Equals(typeName))
                    return "DateTime";
                if ("datetime2".Equals(typeName))
                    return "DateTime";
                if ("date".Equals(typeName))
                    return "DateTime";
                if ("datetimeoffset".Equals(typeName))
                    return "DateTimeOffset";
                if ("char".Equals(typeName))
                {
                    if (typeParam == "1")
                        return "char";
                    else
                        return "string";
                }
                if ("nchar".Equals(typeName))
                {
                    if (typeParam == "1")
                        return "char";
                    else
                        return "string";
                }
                if ("nvarchar".Equals(typeName))
                    return "string";
                if ("varchar".Equals(typeName))
                    return "string";
                if ("ntext".Equals(typeName))
                    return "string";
                if ("uniqueidentifier".Equals(typeName))
                    return "Guid";
                throw new NotImplementedException();
            }
        }
        
        public bool IsNullable
        {
            get => "NULL".Equals(nullable);
        }

        public PropertyConversion(string fieldName, string typeName, string typeParam, dynamic typeParam2, dynamic nullable)
        {
            this.fieldName = fieldName;
            this.typeName = typeName;
            this.typeParam = typeParam;
            this.typeParam2 = typeParam2;
            this.nullable = nullable;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(IndentString);
            builder.Append("public ");
            builder.Append(PropertyType);
            if (IsNullable)
                builder.Append("?");
            builder.Append(" ");
            builder.Append(this.fieldName);
            builder.AppendLine(" { get; set; }");
            return builder.ToString();
        }

        public static explicit operator PropertyConversion(Match fieldMatch)
        {
            var fieldName = fieldMatch.Groups[1].Value;
            var typeName = fieldMatch.Groups[2].Value;
            var typeParam = fieldMatch.Groups[3].Value;
            dynamic typeParam2 = null;
            dynamic nullable;
            if ("decimal".Equals(typeName))
            {
                typeParam2 = fieldMatch.Groups[4].Value;
                nullable = fieldMatch.Groups[5].Value;
            }
            else
            {
                nullable = fieldMatch.Groups[4].Value;
            }
            return new PropertyConversion(fieldName, typeName, typeParam, typeParam2, nullable);
        }
    }
}