using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ.Models;

namespace Project.IdentityServ
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ProjectApiContext _db;
        public CustomResourceOwnerPasswordValidator(ProjectApiContext db)
        {
            _db = db;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _db.People.SingleOrDefaultAsync(x=> x.NationalCode == context.UserName.ToString());
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
