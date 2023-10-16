using Microsoft.AspNetCore.Mvc;
using MongoDB.Net6.Core;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDBController : ControllerBase
    {
        private readonly ICrudMongoDB<Student> _crudMongoDB;

        public MongoDBController(ICrudMongoDB<Student> crudMongoDB)
        {
            _crudMongoDB = crudMongoDB;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _crudMongoDB.ListAsync();

            if (response.Count() == 0)
            {
                return StatusCode(404, "Sin resultados");
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Student mongoDocument)
        {
            var response = await _crudMongoDB.InsertAsync(mongoDocument);

            if (response)
            {
                return StatusCode(200, "Inserción realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Student student)
        {
            var response = await _crudMongoDB.UpdateAsync(student);

            if (response)
            {
                return StatusCode(200, "Actualización realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }

        [HttpDelete(Name = "{id:string}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _crudMongoDB.DeleteAsync(id);

            if (response)
            {
                return StatusCode(200, "Eliminación realizada correctamente");
            }

            return StatusCode(500, "Error Interno");
        }
    }
}
