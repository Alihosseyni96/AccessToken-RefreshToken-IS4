using System;
using System.Collections.Generic;

namespace ProjectMVC.Data.Models.Models;

public partial class User
{
    public int Id { get; set; }

    public string NationalCode { get; set; } = null!;

    public virtual ICollection<Travel> Travels { get; set; } = new List<Travel>();
}
