using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VkAPI.Models;

namespace VkAPI.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private ApplicationContext db; 
    
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ApplicationContext _db): base (options,logger,encoder,clock)
    {
        db = _db;
    }
    
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
         if(!(Request.Headers.ContainsKey("login") && (Request.Headers.ContainsKey("password"))))
             return AuthenticateResult.Fail("Headers was not found");

         string? login = Request.Headers["login"];
         string? password = Request.Headers["password"];

         User? authUser = await db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login) && u.Password.Equals(password));
             
         
         if(authUser is null)
             return AuthenticateResult.Fail("Wrong headers");

         var claims = new List<Claim>()
         {
             new ("login", login),
             new ("password", password),
         };

         var identity = new ClaimsIdentity(claims,Scheme.Name);
         var principal = new ClaimsPrincipal(identity);
         var ticket = new AuthenticationTicket(principal,Scheme.Name);

         return AuthenticateResult.Success(ticket);
    }
}