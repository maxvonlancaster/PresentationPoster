using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramAccess.Constants;

namespace PresentationPoster.Services
{
    public class CustomValidator : IUserValidator<IdentityUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            var errors = new List<IdentityError>();

            
            if (user.UserName != Const.UserName)
                errors.Add(new IdentityError() { Code = "Invalid account" });

            Task<IdentityResult> task = Task<IdentityResult>.Run(() => 
            {
                return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
            });

            return await task;
        }
    }
}
