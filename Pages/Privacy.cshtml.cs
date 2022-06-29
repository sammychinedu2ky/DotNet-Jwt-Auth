using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt_Auth.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IConfiguration _config;

        public PrivacyModel(ILogger<PrivacyModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            IdentityModelEventSource.ShowPII = true;
        }

        public void OnGet()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "test"),
                new Claim(JwtRegisteredClaimNames.Iss, DateTime.Now.ToString()),
                new Claim(ClaimTypes.Name, "samson"),
              //  new Claim(JwtRegisteredClaimNames.Exp, TimeSpan.FromSeconds(10).ToString())
                
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["jwt:issuer"],
                audience: _config["jwt:audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(2),
                signingCredentials: creds
            );
            ViewData["token"] = new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public ActionResult OnGetSam() => Content("test");
    }
}