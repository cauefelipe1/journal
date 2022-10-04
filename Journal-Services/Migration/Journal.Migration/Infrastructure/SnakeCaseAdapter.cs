using System.Text;

namespace Journal.Migration.Infrastructure;

public static class SnakeCaseAdapter
{
    public static IDictionary<string, object?> ConvertToSnakeCase(object @object, params string[] columnsToSkip)
    {
        if (@object is null)
            throw new ArgumentNullException(nameof(@object));

        var result = new Dictionary<string, object?>();

        var props = @object.GetType().GetProperties();

        foreach (var p in props)
        {
            if (columnsToSkip is null || !columnsToSkip.Contains(p.Name))
                result.Add(InternalConvertPropertyNameToSnakeCase(p.Name), p.GetValue(@object));
        }

        return result;
    }

    private static string InternalConvertPropertyNameToSnakeCase(string propertyName)
    {
        var result = new StringBuilder(1024);
        foreach (char c in propertyName)
        {
            if (result.Length > 0 && char.IsUpper(c))
                result.Append('_');

            result.Append(char.ToLower(c));
        }

        return result.ToString();
    }
}