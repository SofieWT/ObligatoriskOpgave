using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObligatoriskOpgave.Models;
using ObligatoriskOpgave.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace ObligatoriskOpgave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private RecordsRepositoryList _repo;
        public RecordsController(RecordsRepositoryList repo)
        {
            _repo = repo;
        }

        // GET: api/<RecordsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<Record>> Get(
            [FromQuery] string? title = null, 
            [FromQuery] string? artist = null, 
            [FromQuery] int? durationInSecs = null, 
            [FromQuery] string? publicationYear = null)
        {
            var records = _repo.GetAll(title, artist, durationInSecs, publicationYear);
            if (records.Any())
            {
                return Ok(records);
            }
            return NotFound();

        }

        // GET api/<RecordsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecordsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public ActionResult<Record> Post([FromBody] Record record)
        {
            var addedRecord = _repo.AddRecord(record);
            if(addedRecord == null)
            {
                return BadRequest("Could not add the record.");
            }
            return CreatedAtAction(nameof(Get), new { id = addedRecord.ID }, addedRecord);
        }

        // PUT api/<RecordsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecordsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public ActionResult<Record> Delete(int id)
        {
            var deletedRecord = _repo.DeleteRecord(id);
            if (deletedRecord == null)
            {
                return NotFound();
            }
            return Ok(deletedRecord);
        }
    }
}
