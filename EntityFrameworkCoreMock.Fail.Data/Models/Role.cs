using System;
using System.Collections.Generic;

namespace EFCoreMock.Fail.Models;

public partial class Role : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
