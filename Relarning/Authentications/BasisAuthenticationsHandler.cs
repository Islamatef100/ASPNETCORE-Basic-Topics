using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Relarning.Authentications
{
    public class BasicAuthenticationsHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationsHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

            var authHeader = Request.Headers["Authorization"].ToString();

            if(!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            
            if (authHeader.Length <= 6)
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

            var token = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Substring("Basic ".Length).Trim()));

            var username = token.Split(':')[0];
            var password = token.Split(':')[1];
            if(username == "admin" && password == "admin")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

        }
    }
}
