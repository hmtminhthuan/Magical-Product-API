﻿using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request;

public class ProductRequest
{
	[Required(ErrorMessage = "Email is required")]
	[MaxLength(50, ErrorMessage = "Email's max length is 50 characters")]
	public string Email { get; set; }
	[Required(ErrorMessage = "Password is required")]
	[MaxLength(64, ErrorMessage = "Password's max length is 64 characters")]
	public string Password { get; set; }
}