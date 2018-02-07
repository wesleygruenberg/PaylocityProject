using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaylocityDeductionCalculator.Startup))]
namespace PaylocityDeductionCalculator
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
