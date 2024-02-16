using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AngetPet.Shared.Helpers
{
    public class ConstantHelper
    {
        public class Role
        {
            public const string ADMIN = "ADMIM";
            public const string CUSTOMER = "CUSTOMER";
        }

        public class ValueId
        {
            public int Id { get; set; }
            public string? Value { get; set; }
        }

        public class ClaimType
        {
            public const string UserId = "userId";
            public const string Name = "name";
            public const string Role = "role";
        }

        public class TokenInfo
        {
            public static string GetClaimValue(ClaimsPrincipal claims, string value)
            {
                return claims.Claims.FirstOrDefault(c => c.Type == value)?.Value;
            }
        }
    }
}
