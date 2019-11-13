using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly MyContext myContext;

        public BlogController(ILogger<BlogController> logger, MyContext myContext)
        {
            _logger = logger;
            this.myContext = myContext;
        }

        [HttpGet]
        public bool Get()
        {
            _logger.LogInformation("test");
            // Creating a new item and saving it to the database
            myContext.Blogs.Add(new Blog { BlogId = 1, Url = "https://www.google.nl" });
            myContext.SaveChanges();

            return true;
        }

        [HttpGet("migrate")]
        public bool Migrate()
        {
            _logger.LogInformation("migrate");
            myContext.Database.Migrate();
            return true;
        }
    }
}
