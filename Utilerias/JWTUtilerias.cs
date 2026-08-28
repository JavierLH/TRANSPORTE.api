using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIV2.Utilerias
{
    public static class JWTUtilerias
    {
        public static List<Claim> ObtenerClaimsToken(string tokenString)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(tokenString);
            return jwtToken.Claims.ToList();
        }

    }
}
