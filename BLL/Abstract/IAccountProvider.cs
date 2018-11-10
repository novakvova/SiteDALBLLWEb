using BLL.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAccountProvider
    {
        Task<SignInStatus> LoginAsync(LoginViewModel model);
        SignInStatus Login(LoginViewModel model);
        void LogOff();
    }
}
