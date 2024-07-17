using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.UI.Models;

namespace VisitorRegistrationSystem.UI.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;

        }
        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            return View(new UserViewModel()
            {
                User = user

            });

        }
    }
}
