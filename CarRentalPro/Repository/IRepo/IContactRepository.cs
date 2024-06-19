using CarRentalPro.Data.Models;

namespace CarRentalPro.Repository.IRepo
{
    public interface IContactRepository
    {
        Task<Contact> GetAsync(int Id);
        Task<List<Contact>> GetListAsync();
        Task AddContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int Id);
    }
}

