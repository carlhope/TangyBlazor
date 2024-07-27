using Microsoft.AspNetCore.Components;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Login
    {
        private SignInRequestDTO SignInRequest = new SignInRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowSignInErrors { get; set; }
        public string Errors { get; set; }
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public string ReturnUrl { get; set; }

        private async Task LoginUser()
        {
            ShowSignInErrors = false;
            IsProcessing = true;
            var result = await _authService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //Sign in successful
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = System.Web.HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    _navigationManager.NavigateTo("/" +ReturnUrl);
                }
                else
                {
                    _navigationManager.NavigateTo("/");
                }
            
            }
            else
            {
                //failure
                Errors = result.ErrorMessage;
                ShowSignInErrors = true;
            }
            IsProcessing = false;
        }
    }
}
