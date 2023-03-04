using System.Data.SqlClient;
using CrudApiWithDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiWithDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //--- to get the ConnectionString for SQL Server ---------------------------------------------------------------
        
        private readonly IConfiguration _config;
        
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        //--- get all the users in database ----------------------------------------------------------------------------
        
        [HttpGet]
        public async Task<ActionResult<List<userModel>>> GetAllUsers()
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var users = await cnx.QueryAsync<userModel>("SELECT * FROM UserTable");
            if (users != null) {
                return Ok(users);
            }else {
                return NotFound("Data not found !");
            }
        }
        
        //--- get the user with the id given in parameter --------------------------------------------------------------
        
        [HttpGet("{userId}")]
        public async Task<ActionResult<userModel>> GetSingleUser(int userId)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var singleUser = await cnx.QueryFirstAsync<userModel>("SELECT * FROM UserTable WHERE id = @id;", new {id = userId});
            if (singleUser != null) {
                return Ok(singleUser);
            }else {
                return NotFound("Data not found !");
            }
        }
        
        //create new user with the params (username, gender, birthDate, birthPlace, email, crewId), id is set by default
        
        [HttpPost]
        public async Task<ActionResult<userModel>> InsertUser(userModel user)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "INSERT INTO UserTable(username, gender, birthDate, birthPlace, email, crewId) VALUES (@nusername, @ngender, @nbirthDate, @nbirthPlace, @nemail, @ncrewId);", 
                new {nusername = user.username, ngender = user.gender, nbirthDate = user.birthDate, nbirthPlace = user.birthPlace, nemail = user.email, ncrewId = user.crewId}
            );
            return Ok();
        }
        
        //--- update the user ------------------------------------------------------------------------------------------
        
        [HttpPut]
        public async Task<ActionResult<userModel>> UpdateUser(userModel user)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "UPDATE UserTable SET username = @nusername, gender = @ngender, birthDate = @nbirthDate, birthPlace = @nbirthPlace, email = @nemail, crewId = @ncrewId WHERE id = @nid;", 
                new {nid = user.id, nusername = user.username, ngender = user.gender, nbirthDate = user.birthDate, nbirthPlace = user.birthPlace, nemail = user.email, ncrewId = user.crewId}
            );
            return Ok();
        }
        
        //--- delete the user with the given id in parameter -----------------------------------------------------------
        
        [HttpDelete("{userId}")]
        public async Task<ActionResult<userModel>> DeleteUser(int userId)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "DELETE FROM UserTable WHERE id = @nid;", 
                new {nid = userId}
            );
            return Ok();
        }
        
    }
}

