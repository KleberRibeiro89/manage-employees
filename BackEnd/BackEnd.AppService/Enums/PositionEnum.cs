using System.Runtime.Serialization;

namespace BackEnd.AppService.Enums;

public enum PositionEnum
{
    [EnumMember(Value = "c3d4c221-c289-4100-8f8b-23e3ca578328")]
    Director,

    [EnumMember(Value = "8aa13f02-fedc-4bdc-8a61-6bd583086332")]
    Leader,

    [EnumMember(Value = "1f563b65-4ee8-4595-aef8-9f00a50a2899")]
    Employee
}
