using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Simbir.Go.Helpers
{
    public static class HttpUserHelper
    {
        public static string GetLoginHttpUser(this HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["authorization"];
            var tokenParameters = AuthenticationHeaderValue.Parse(token).Parameter;

            var handler = new JwtSecurityTokenHandler();
            var tokenData = handler.ReadJwtToken(tokenParameters);
            var userClaims = tokenData.Claims;

            return userClaims.FirstOrDefault(x => x.Type == "Username").Value;
        }
    }
}