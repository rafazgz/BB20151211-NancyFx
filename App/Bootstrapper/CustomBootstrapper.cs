using Common.Interfaces;
using Core.Services;
using Data.Context;
using Nancy;
using Nancy.TinyIoc;

namespace App.Bootstrapper
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(typeof(IRoomService), (c, o) => new RoomService());
        }
    }
}