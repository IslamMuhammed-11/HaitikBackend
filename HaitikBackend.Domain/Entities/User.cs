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

    public Driver AssignAsDriver(short? maximumOrdersPerDay, enDriverStatus status = enDriverStatus.Offline)
    {
        //Role = Driver

        return Driver.Create(Id, maximumOrdersPerDay, status);
    }

    public void AssignAsDeliveryAdmin()
    {
        // Role = DeliveryAdmin;
    }


    public void CreateRefreshToken(string tokenHash, DateTime expiry)
    {
        var refreshtoken = RefreshToken.Create(Id, null, tokenHash, expiry);

        RefreshTokens.Add(refreshtoken);

    }

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

    public virtual Driver? Driver { get; private set; }

    public virtual ICollection<Return> Returns { get; private set; } = new List<Return>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    public virtual Role Role { get; private set; } = null!;
}
