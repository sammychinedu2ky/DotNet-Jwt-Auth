using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jwt_Auth.Pages
{
    [Authorize]
    public class CokeModel : PageModel
    {
        public void OnGet()
        {
            var u = User;
        }
    }
}
