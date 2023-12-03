using System.Reflection;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Helpers
{
    public static class ObjectComparer
    {
        public static List<string> GetChangedProperties<T>(T original, T modified)
        {
            List<string> changedProperties = new List<string>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object originalValue = property.GetValue(original);
                object modifiedValue = property.GetValue(modified);

                // Compare values
                if (!object.Equals(originalValue, modifiedValue))
                {
                    changedProperties.Add(property.Name);
                }
            }

            return changedProperties;
        }
    }
}
