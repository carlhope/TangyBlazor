using Microsoft.AspNetCore.Components;
using System;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO SignUpRequest = new SignUpRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _authService.RegisterUser(SignUpRequest);
            if (result.IsRegistrationSuccessful)
            {
                //registration successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}
