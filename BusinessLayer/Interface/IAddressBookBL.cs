using ModelLayer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAddressBookBL
    {
        Task<IEnumerable<EntryModel>> GetAllContactsAsync();
        Task<EntryModel> GetContactByIdAsync(int id);
        Task<EntryModel> AddContactAsync(RequestModel request);
        Task<EntryModel> UpdateContactAsync(int id, RequestModel request);
        Task<bool> DeleteContactAsync(int id);
    }
}