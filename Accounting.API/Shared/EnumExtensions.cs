namespace Accounting.API.Shared;

static class EnumExtensions
{
    public static TEnum ParseEnum<TEnum>(string? val) where TEnum : struct, IConvertible
    {
        foreach (TEnum @enum in Enum.GetValues(typeof(TEnum)))
        {
            if (string.Equals(@enum.ToString(), val, StringComparison.InvariantCultureIgnoreCase))
            {
                return @enum;
            }
        }
        return default;
    }

    public static bool TryParseEnum<TEnum>(string? val, out TEnum output) where TEnum : struct, IConvertible
    {
        foreach (TEnum @enum in Enum.GetValues(typeof(TEnum)))
        {
            if (string.Equals(@enum.ToString(), val, StringComparison.InvariantCultureIgnoreCase))
            {
                output = @enum;
                return true;
            }
        }
        output = default;
        return false;
    }
}