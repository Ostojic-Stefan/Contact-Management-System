using Contacts.Data;
using Contacts.Models;
using Contacts.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers
{
    public record ContactDto(string FirstName, string LastName, string Email);

    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        public async Task<IActionResult> AddContact(ContactDto contact) => Created("", await _repo.AddContact(contact));
        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<Person>))]
        public async Task<IActionResult> GetAllContacts() => Ok(await _repo.GetAllContacts());

        [HttpGet]
        [Route("findByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Person))]
        public async Task<IActionResult> GetContactByName([FromQuery] string name)
        {
            try 
            {
                return Ok(await _repo.GetContactByName(name));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Person))]
        public async Task<IActionResult> DeleteContact(int personId)
        {
            try 
            {
                await _repo.DeleteContact(personId);
                return NoContent();
            } 
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}