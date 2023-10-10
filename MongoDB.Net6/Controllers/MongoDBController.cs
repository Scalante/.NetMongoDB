using Microsoft.AspNetCore.Mvc;
using MongoDB.Net6.Core;
using MongoDB.Net6.Model.Dtos;

namespace MongoDB.Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDBController : ControllerBase
    {
        private readonly ICrudMongoDB _crudMongoDB;

        public MongoDBController(ICrudMongoDB crudMongoDB)
        {
            _crudMongoDB = crudMongoDB;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _crudMongoDB.List();

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PeopleDto peopleDto)
        {
            var response = await _crudMongoDB.Insert(peopleDto);

            if (response)
            {
                return StatusCode(200, "Inserción realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PeopleDto peopleDto)
        {
            var response = await _crudMongoDB.Update(peopleDto);

            if (response)
            {
                return StatusCode(200, "Actualización realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }

        [HttpDelete(Name = "{id:string}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _crudMongoDB.Delete(id);

            if (response)
            {
                return StatusCode(200, "Eliminación realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }
    }
}
