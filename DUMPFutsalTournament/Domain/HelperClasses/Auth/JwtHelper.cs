using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using DUMPFutsalTournament.Data.Entities;
using Jose;

namespace DUMPFutsalTournament.Domain.HelperClasses.Auth
{
    public static class JwtHelper
    {
        public static string GetJwtToken(User userToGenerateFor)
        {
            var secret = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["as:AudienceSecret"]);
            var issuer = ConfigurationManager.AppSettings["as:Issuer"];
            var audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            var currentSeconds = Math.Round(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            var payload = new Dictionary<string, string>
            {
                {"iss", issuer},
                {"aud", audienceId},
                {"exp", (currentSeconds + 20000).ToString(CultureInfo.InvariantCulture) },
                {"userid", userToGenerateFor.UserId.ToString()}
            };

            return JWT.Encode(payload, secret, JwsAlgorithm.HS256);
        }

    }
}
