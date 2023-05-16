using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class Terapija
{
    public int TerapijaId { get; set; }

    public string? OpisTerapije { get; set; }
}
