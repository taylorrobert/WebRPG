using System;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RPG.Controllers.Designer
{
    [Route("api/[controller]")]
    public class DevController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }
}
