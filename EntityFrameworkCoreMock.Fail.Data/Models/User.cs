using System;
using System.Collections.Generic;

namespace EFCoreMock.Fail.Models;

public partial class User : IEntity
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = null!;
    public bool IsMaster { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
