namespace Kidemy.Web.Attributes
{
    public class IgnoreSanitizeAttribute : Attribute
    {
        public string[] IgnoredFieldsName { get; set; }

        public IgnoreSanitizeAttribute(params string[] ignoredFieldsName)
        {
            IgnoredFieldsName = ignoredFieldsName;
        }
    }
}
