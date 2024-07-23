﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MagicalProduct.API.Constants;
using MagicalProduct.API.Models;

namespace MagicalProduct.API.Utils;

public class JwtUtil
{
    public static string GenerateJwtToken(User account)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables(prefix: JwtConstant.JwtEnvironment)
        .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey secrectKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConstant:" + JwtConstant.SecretKey]));
        var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
        string issuer = configuration["JwtConstant:" + JwtConstant.Issuer];
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, account.Email),
            new Claim(ClaimTypes.Role, account.Role.Name),
        };
        var expires = DateTime.Now.AddDays(30);
        var token = new JwtSecurityToken(issuer, null, claims, notBefore: DateTime.Now, expires, credentials);
        return jwtHandler.WriteToken(token);
    }
}