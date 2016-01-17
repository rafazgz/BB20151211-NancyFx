using Microsoft.Owin.Extensions;
using Owin;

namespace App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Common.Setup.AutoMapper.Initialize();
            app.UseNancy();
            app.UseStageMarker(PipelineStage.MapHandler);
        }    
    }
}