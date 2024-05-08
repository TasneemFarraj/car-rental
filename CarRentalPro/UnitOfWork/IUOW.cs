using CarRentalPro.Repository.IRepo;

namespace CarRentalPro.UnitOfWork
{
    public interface IUOW 
    {
        public IUserRepository User { get; set; }

    }
}
