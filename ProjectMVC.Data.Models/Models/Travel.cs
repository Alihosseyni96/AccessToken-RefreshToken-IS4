using System;
using System.Collections.Generic;

namespace ProjectMVC.Data.Models.Models;

public partial class Travel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Origin { get; set; } = null!;

    public DateTime DepartedDate { get; set; }

    public string Destenition { get; set; } = null!;

    public DateTime ReturnDate { get; set; }

    public virtual User User { get; set; } = null!;
}
