using App.Models;
using Core.Services;
using Nancy;
using Nancy.ModelBinding;

namespace App.Modules
{
    public class UserModule : NancyModule
    {
         
        protected IUserService Service { get; set; }

        public UserModule(IUserService service) : base("/api/users")
        {
            Service = service;

            Post["/login"] = p =>
            {
                var model = this.Bind<NewUserModel>();

                var user = Service.CreateUser(model.Name);

                return Negotiate.WithModel(user.ViewModel);
            };
        }

        

    }
}