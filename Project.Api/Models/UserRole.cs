﻿using System;
using System.Collections.Generic;

namespace Project.Api.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public long UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
