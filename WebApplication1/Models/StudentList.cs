using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public partial class StudentList
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    [NotMapped]
    public string EncrptedId { get; set; } = null;
    public string Email { get; set; } = null!;

    public string StudentPassword { get; set; } = null!;

    public string Faculty { get; set; } = null!;
}
