using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VapeInventoryManagement.DTO;

namespace VapeInventoryManagement.Pages
{
    public partial class CreateAccount : ComponentBase
    {
        private RegisterDto registerModel = new();
        private string errorMessage;

        [Inject] private UserManager<IdentityUser> UserManager { get; set; }
        [Inject] private SignInManager<IdentityUser> SignInManager { get; set; }
        [Inject] private RoleManager<IdentityRole> RoleManager { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private async Task HandleRegister()
        {
            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                errorMessage = "Passwords do not match.";
                return;
            }

            var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
            var result = await UserManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                // Ensure ADMIN role exists
                if (!await RoleManager.RoleExistsAsync("Admin"))
                {
                    await RoleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Assign ADMIN role to the new user
                await UserManager.AddToRoleAsync(user, "Admin");

                // Sign in the user
                await SignInManager.SignInAsync(user, isPersistent: false);

                // Redirect after successful registration
                NavigationManager.NavigateTo("/home");
            }

            else
            {
                errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
            }
        }
    }
}