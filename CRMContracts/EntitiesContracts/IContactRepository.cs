using CRMEntities.Models;

namespace CRMContracts
{
    public interface IContactRepository
    {

        void CreateContact(Contact contact);
    }
}
