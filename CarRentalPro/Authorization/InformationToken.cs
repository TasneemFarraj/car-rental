using System.IdentityModel.Tokens.Jwt;

namespace CarRentalPro.Authorization
{
    public class InformationToken
    {
        public static Response.JwtAuthResponse GetInfoUsers(string strToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(strToken.Replace("Bearer ", ""));
            var InfoDecrypt = SecuredHelper.GetInfoFromToken(JwtToken.ToString());
            return InfoDecrypt;
        }
    }
}
