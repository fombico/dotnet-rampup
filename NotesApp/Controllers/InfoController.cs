using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    public class InfoController : Controller
    {
        public InfoController(
            IOptions<CloudFoundryApplicationOptions> appOptions,
            IOptions<CloudFoundryServicesOptions> serviceOptions)
        {
            AppOptions = appOptions.Value;
            ServiceOptions = serviceOptions.Value;
        }

        private CloudFoundryServicesOptions ServiceOptions { get; }
        private CloudFoundryApplicationOptions AppOptions { get; }

        public IActionResult Index()
        {
            return Json(new {
                applicationName = AppOptions.ApplicationName,
                hostname = ServiceOptions.Services
                    .SingleOrDefault(s => "notes-db".Equals(s.Name))?
                    .Credentials["hostname"]
                    .Value
            });
        }
    }
}