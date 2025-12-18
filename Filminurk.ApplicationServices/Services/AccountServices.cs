using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Filminurk.ApplicationServices.Services
{
    public class AccountServices : IAccountsServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailServices emailServices;


        public AccountServices
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserEmailServices emailServices
            )
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            this.emailServices = emailServices;
        }
        public async Task<ApplicationUser> Register(ApplicationUserDTO userDTO)
        {
            var user = new ApplicationBase
            {
                UserName = userDTO.Email,
                EmailServices = userDTO.Email,
                ProfileType = userDTO.ProfileType,
                DisplayNameAttribute = userDTO.DisplayName,
            };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            return user;
        }
        public async Task<ApplicationUser> Login(LoginDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            return user; 
        }
    }
}
