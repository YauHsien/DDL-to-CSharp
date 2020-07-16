using System.Collections.Generic;
using System.Text;

namespace DDL_to_CSharp
{
    internal class SqlTemplateClassGenerator
    {
        private IEnumerable<ClassConversion> bag;

        public SqlTemplateClassGenerator(IEnumerable<ClassConversion> bag)
        {
            this.bag = bag;
        }

        public IEnumerable<string> GetSqlTemplateClassDefinition()
        {
            List<string> result = new List<string>();

            result.Add("public class SqlTemplate");
            result.Add("{");
            result.Add("    public SqlTemplate()");
            result.Add("    {");
            result.Add("    }");

            foreach (var item in bag)
            {
                string className = item.TableName;
                result.Add("    public static explicit operator SqlTemplate(" + className + " obj)");
                result.Add("    {");


                result.Add(string.Format("string[] fields = new string[] {{0}};", item.GetColumnNames().Aggregate((acc, item) => "\"" + acc + "\", \"" + item + "\"")));
                result.Add("return new SqlBase(document.Schema, "PurchasingDocument", fields.Where((field) =>");
                result.Add("    (");
                result.Add("        (\"Id\".Equals(field) && document.Id != null)");
                result.Add("        || (\"PurchasingFromStatusId\".Equals(field) && document.PurchasingFromStatusId != null)");
                result.Add("        || (\"CreatedByEmployeeId\".Equals(field) && document.CreatedByEmployeeId != null)");
                result.Add("        || (\"CreateTime\".Equals(field) && document.CreateTime != null)");
                result.Add("         || (\"WroteByEmployeeId\".Equals(field) && document.WroteByEmployeeId != null)");
                result.Add("        || (\"WroteTime\".Equals(field) && document.WroteTime != null)");
                result.Add("    )).ToHashSet());");


                result.Add("        SqlTemplate sqlTemplate = new SqlTemplate();");
                result.Add("        return sqlTemplate;");
                result.Add("    }");
            }

            result.Add("}");


            return result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public static explicit operator SqlTemplate("Match fieldMatch)
            return base.ToString();
        }
    }
}