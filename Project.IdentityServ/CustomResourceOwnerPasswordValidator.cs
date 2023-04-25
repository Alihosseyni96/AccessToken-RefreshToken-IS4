using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ.Models;

namespace Project.IdentityServ
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IdentityServerContext _db;
        public CustomResourceOwnerPasswordValidator(IdentityServerContext db)
        {
            _db = db;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == context.UserName && x.Password == context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(subject: user.Id.ToString(), authenticationMethod: "password");

            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user is not exist");
            }
        }
    }
}
