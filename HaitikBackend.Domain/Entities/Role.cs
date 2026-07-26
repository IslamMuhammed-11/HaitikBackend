using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class Role : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    private Role()
    {
    }

    private Role(string name)
    {
        name = Name;
    }

    public static Result<Role> Create(string name)
    {
        return Result<Role>.Success(new Role(name));
    }

    public virtual ICollection<User> Users { get; private set; } = new List<User>();
}
