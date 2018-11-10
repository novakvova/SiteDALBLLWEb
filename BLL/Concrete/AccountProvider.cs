using BLL.Abstract;
using BLL.Services.Identity;
using BLL.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class AccountProvider : IAccountProvider
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authManager;

        public AccountProvider(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
        }

        private ApplicationSignInManager SignInManager
        {
            get { return _signInManager; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return _authManager; }
        }

        public SignInStatus Login(LoginViewModel model)
        {
            var result = SignInManager.PasswordSignIn(model.Email, 
                model.Password, model.RememberMe, shouldLockout: false);
            return result;
        }

        public async Task<SignInStatus> LoginAsync(LoginViewModel model)
        {
            return await Task.Run(() => this.Login(model));
        }

        public void LogOff()
        {
            AuthenticationManager
                .SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
