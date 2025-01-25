using System.Reflection;
using System.Runtime.Serialization;
using BackEnd.AppService.Enums;

namespace BackEnd.AppService.Extensions;

public static class EnumExtensions
{
    public static string GetEnumMemberValue(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var memberInfo = enumType.GetMember(enumValue.ToString());
        var enumMemberAttribute = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();

        return enumMemberAttribute?.Value ?? enumValue.ToString();
    }

    public static PositionEnum? GetPositionFromValue(Guid value)
    {
        foreach (var field in typeof(PositionEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attribute != null && attribute.Value == value.ToString())
            {
                return (PositionEnum)field.GetValue(null);
            }
        }
        return null;
    }
}