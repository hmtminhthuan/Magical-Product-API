using System.ComponentModel;

namespace MagicalProduct.API.Enums;

public enum RoleEnum
{
    [Description("Admin")]
    Admin,
    [Description("Staff")]
    Staff,
    [Description("Customer")]
    Customer
}