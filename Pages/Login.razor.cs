using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using VapeInventoryManagement.DTO;
using VapeInventoryManagement.Models;

namespace VapeInventoryManagement.Pages
{
    public partial class Login : ComponentBase
    {
        private readonly LoginDto loginModel = new();
        private bool rememberMe = false;

        [Inject] private SignInManager<User> SignInManager { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private async Task HandleLogin()
        {
            var result = await SignInManager.PasswordSignInAsync(
                loginModel.Email,
                loginModel.Password,
                isPersistent: rememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                NavigationManager.NavigateTo("/home");
            }
            else
            {
                // TODO: show error message
            }
        }
    }

}
