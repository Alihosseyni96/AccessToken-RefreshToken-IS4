using System;
using System.Collections.Generic;

namespace Project.IdentityServ.Models;

public partial class Person
{
    public int Id { get; set; }

    public string NationalCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;
}
