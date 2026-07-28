using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Entities;

public partial class User : BaseEntity
{
    public int Id { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public int RoleId { get; private set; }

    private User()
    {
    }

    private User(string firstName, string lastName, string email, string phoneNumber, string passwordHash, int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }


    public static User Create(string firstName, string lastName, string email, string phoneNumber, string passwordHash, int roleId)
    {
        return new User(firstName, lastName, email, phoneNumber, passwordHash, roleId);


    }

    public string FullName() => FirstName + LastName;

    public Result<Driver> AssignAsDriver(int geoZoneId, enDriverStatus status = enDriverStatus.Offline)
    {
        return Driver.Create(Id, geoZoneId, status);
    }

    public Result<DeliveryAdmin> AssignAsDeliveryAdmin()
    {
        return DeliveryAdmin.Create(Id);
    }

    public GovernmentEmployee AssignAsGovernmentEmployee(int agencyId)
    {
        return GovernmentEmployee.Create(Id, agencyId);
    }

    public RefreshToken CreateRefreshToken(string tokenHash, DateTime expiry) => RefreshToken.Create(Id, tokenHash, expiry);

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void ChangeRole(int roleId)
    {
        RoleId = roleId;
    }
    public virtual ICollection<DeliveryAdmin> DeliveryAdmins { get; private set; } = new List<DeliveryAdmin>();

    public virtual ICollection<Driver> Drivers { get; private set; } = new List<Driver>();

    public virtual ICollection<GovernmentEmployee> GovernmentEmployees { get; private set; } = new List<GovernmentEmployee>();

    public virtual RefreshToken? RefreshToken { get; private set; }

    public virtual Role Role { get; private set; } = null!;
}
