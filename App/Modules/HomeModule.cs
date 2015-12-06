using Nancy;

namespace App.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => "Hello World!";
        }
    }
}