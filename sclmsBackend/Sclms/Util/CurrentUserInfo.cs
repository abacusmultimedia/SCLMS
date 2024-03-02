using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;

namespace Sclms.Util
{
    public class CurrentUserInfo
    {

        public string GetUser(IHeaderDictionary headers)
        {
            string UserId = "";
            var userToken = headers["Authorization"].ToString(); ;
            var jwtToken = userToken.Replace("Bearer ", string.Empty);

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            // Access claims to get userId
            UserId = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return UserId;

        }
        public string GetRole(IHeaderDictionary headers)
        {
            string userRole = "";
            var userToken = headers["Authorization"].ToString(); ;
            var jwtToken = userToken.Replace("Bearer ", string.Empty);

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            // Access claims to get userRole
            userRole = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
           
            return userRole;


        }
    }
}
