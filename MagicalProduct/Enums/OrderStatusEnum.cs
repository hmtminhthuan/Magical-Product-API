using System.ComponentModel;

namespace MagicalProduct.API.Enums;

public enum OrderStatusEnum
{
    New = 0, // Create order
    Accepted = 1, // Accept order
    InProcess = 2, // Delivery
    Completed = 3, // Done
    Fail = 4 // Fail with problem
}
