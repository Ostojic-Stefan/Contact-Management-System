using Contacts.Controllers;
using Contacts.Data;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _ctx;

        public ContactRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Person> AddContact(ContactDto contact)
        {
            var newPerson = new Person()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
            };

            await _ctx.People.AddAsync(newPerson);
            await _ctx.SaveChangesAsync();

            return newPerson;
        }

        public async Task DeleteContact(int id)
        {
            Person found = await GetContactById(id);
            _ctx.Remove(found);

            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllContacts()
        {
            return await _ctx.People.AsNoTracking().ToArrayAsync();
        }

        public async Task<Person> GetContactById(int id)
        {
            Person? found = await _ctx.People.FirstOrDefaultAsync(p => p.Id == id);
            if (found == null)
                throw new Exception($"Person with id \"{id}\" doesen't exist");
            return found;
        }

        public async Task<Person> GetContactByName(string name)
        {
            Person? found = await _ctx.People.FirstOrDefaultAsync(p => p.FirstName == name);
            if (found == null)
                throw new Exception($"Person with name \"{name}\" doesen't exist");
            return found;
        }
    }
}