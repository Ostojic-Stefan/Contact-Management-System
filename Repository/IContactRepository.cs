using Contacts.Controllers;
using Contacts.Models;

namespace Contacts.Repository
{
    public interface IContactRepository
    {
        Task<Person> AddContact(ContactDto contact);
        Task<IEnumerable<Person>> GetAllContacts();
        Task<Person> GetContactByName(string name);
        Task<Person> GetContactById(int id);
        Task DeleteContact(int id);
    }
}