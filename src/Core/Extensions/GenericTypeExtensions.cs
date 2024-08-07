﻿namespace AttachManagement.Core.Extensions;

public static class GenericTypeExtensions
{
    public static string GetGenericTypeName(this Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }

        var genericTypes = string.Join(',', type.GetGenericArguments().Select(t => t.Name).ToArray());
        return $"{type.Name.Remove(type.Name.IndexOf('`', StringComparison.Ordinal))}<{genericTypes}>";
    }

    public static string GetGenericTypeName(this object @object) =>
        @object.GetType().GetGenericTypeName();
}
