using Kidemy.Application.Security;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;

namespace Kidemy.Web.Filters
{
    public class SanitizeFilter : IActionFilter
    {
        private string[]? ignoredFieldNames;

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Path.ToString().ToLower().StartsWith("/admin")) return;

            var expectedAttribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType().IsAssignableTo(typeof(IgnoreSanitizeAttribute)));
            if (expectedAttribute is not null && expectedAttribute is IgnoreSanitizeAttribute ignoreAttribute)
            {
                if (ignoreAttribute.IgnoredFieldsName?.Length == 0) return;

                ignoredFieldNames = ignoreAttribute.IgnoredFieldsName;
            }

            foreach (var item in context.ActionArguments)
            {
                if (ignoredFieldNames?.Contains(item.Key) ?? false) continue;

                if (item.Value is string str)
                {
                    try
                    {
                        context.ActionArguments[item.Key] = str.SanitizeText();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    SanitizeObject(item.Value);
                }
            }
        }

        private void SanitizeObject(object? obj)
        {
            if (obj is null) return;

            if (obj is List<string> stringList)
            {
                foreach (var item in stringList)
                {
                    item.SanitizeText();
                }
                return;
            }

            if (obj is IEnumerable objects)
            {
                foreach (var item in objects)
                {
                    SanitizeObject(item);
                }
                return;
            }

            if (obj.GetType().IsClass)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    if (ignoredFieldNames?.Contains(property.Name) ?? false) continue;

                    if (property.PropertyType == typeof(string))
                    {
                        try
                        {
                            property.SetValue(obj, property.GetValue(obj)?.ToString()?.SanitizeText());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        SanitizeObject(property.GetValue(obj));
                    }
                }

                return;
            }
        }
    }
}
