using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTask.Data;
using MyTask.Models;

namespace MyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public UserController(ApplicationDbContext db)
        {
            this.db = db;

        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(db.Users.ToList());

        }

        [HttpPost]

        public IActionResult CreateUser([FromBody] User addUser)
        {
            addUser.Id = new System.Guid();

            db.Users.Add(addUser);
            db.SaveChanges();
            return Ok(addUser);
        }



        [HttpPost("loginUser")]
        public IActionResult Login([FromBody] Login user)
        {

            var availableUser = db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (availableUser != null)
            {
                return Ok(availableUser);

            }
           return NotFound();
        }

        


    }
}
