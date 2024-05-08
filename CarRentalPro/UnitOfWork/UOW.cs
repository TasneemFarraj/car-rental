using CarRentalPro.Repository.IRepo;

namespace CarRentalPro.UnitOfWork;
    public class UOW : IUOW

    {
    public UOW(IUserRepository user)
    {
        User = user;

    }

    public IUserRepository User { get; set; }
    }

