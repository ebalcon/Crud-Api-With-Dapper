using System.Data.SqlClient;
using CrudApiWithDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiWithDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrewController : ControllerBase
    {
        //--- to get the ConnectionString for SQL Server ---------------------------------------------------------------
        
        private readonly IConfiguration _config;
        
        public CrewController(IConfiguration config)
        {
            _config = config;
        }
        
        //--- get all the crew in database -----------------------------------------------------------------------------
        
        [HttpGet]
        public async Task<ActionResult<List<crewModel>>> GetAllCrews()
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var crews = await cnx.QueryAsync<crewModel>("SELECT * FROM CrewTable");
            if (crews != null) {
                return Ok(crews);
            }else {
                return NotFound("Data not found !");
            }
        }
        
        //--- get the crew with de id given has parameter --------------------------------------------------------------
        
        [HttpGet("{crewId}")]
        public async Task<ActionResult<crewModel>> GetSingleCrew(int crewId)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var singleCrew = await cnx.QueryFirstAsync<crewModel>("SELECT * FROM CrewTable WHERE id = @id;", new {id = crewId});
            if (singleCrew != null) {
                return Ok(singleCrew);
            }else {
                return NotFound("Data not found !");
            }
        }
        
        //--- create a new crew with the params (name, description), the other are set by default ----------------------
        
        [HttpPost]
        public async Task<ActionResult<crewModel>> InsertCrew(crewModel crew)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "INSERT INTO CrewTable(name, description) VALUES (@nname, @ndescription);", 
                new {nname = crew.name, ndescription = crew.description}
            );
            return Ok();
        }
        
        //--- update the crew ------------------------------------------------------------------------------------------

        [HttpPut]
        public async Task<ActionResult<crewModel>> UpdateCrew(crewModel crew)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "UPDATE CrewTable SET name = @nname, description = @ndescription, isStillActif = @nisstillactif WHERE id = @nid;", 
                new {nid = crew.id, nname = crew.name, ndescription = crew.description , nisstillactif = crew.isStillActif}
            );
            return Ok();
        }
        
        //--- delete the crew with the given id in parameter -----------------------------------------------------------
        
        [HttpDelete("{crewId}")]
        public async Task<ActionResult<crewModel>> DeleteCrew(int crewId)
        {
            using var cnx = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await cnx.ExecuteAsync(
                "DELETE FROM CrewTable WHERE id = @nid;", 
                new {nid = crewId}
            );
            return Ok();
        }
    }
}
