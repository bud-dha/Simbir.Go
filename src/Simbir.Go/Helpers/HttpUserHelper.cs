using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Simbir.Go.Helpers
{
    public static class HttpUserHelper
    {
        public static string GetClaimsValueHttp(this HttpContext httpContext, string type)
        {
            var token = httpContext.Request.Headers["authorization"];
            var tokenParameters = AuthenticationHeaderValue.Parse(token).Parameter;

            var handler = new JwtSecurityTokenHandler();
            var tokenData = handler.ReadJwtToken(tokenParameters);
            var userClaims = tokenData.Claims;

            if(type == "Username")
                return userClaims.FirstOrDefault(x => x.Type == "Username").Value;

            return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        }
    }
}