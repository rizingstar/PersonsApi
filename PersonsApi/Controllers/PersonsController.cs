using Microsoft.AspNetCore.Mvc;
using PersonsApi.Model;

namespace PersonsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        IPersonsRepository _personsRepository;
        private readonly ILogger _logger;
        public PersonsController(IPersonsRepository PersonsRepository, ILogger<PersonsController> logger)
        {
            _personsRepository = PersonsRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult List()
        {
            _logger.LogInformation(LogEvents.ListItems, "GET Persons Accessed");
            return Ok(_personsRepository.All);
        }

        // POST: api/Persons
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Person> PostPerson(Person person)
        {
            _logger.LogInformation(LogEvents.InsertItem, $"POST Persons Called with Person Name:{person.PersonName}");
            _personsRepository.Insert(person);

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> GetPerson(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, $"GET: Persons ID:{id}");
            var Person = _personsRepository.Find(id);

            if (Person == null)
            {
                _logger.LogDebug(LogEvents.GetItemNotFound, $"GET: Persons not found, ID:{id}");
                return NotFound();
            }

            return Ok(Person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _logger.LogInformation(LogEvents.DeleteItem, $"DELETE: Person with Id: {id} called");
                var Person = _personsRepository.Find(id);
                if (Person == null)
                {
                    _logger.LogError(LogEvents.GetItemNotFound, $"Person with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _personsRepository.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Person action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}