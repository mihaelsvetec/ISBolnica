using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess;

public partial class Uredaj
{
    public int UredajId { get; set; }

    public string? Naziv { get; set; }

    public string? SerijskiBroj { get; set; }

    public string? Upotreba { get; set; }

    public int? OdjelId { get; set; }

    public int? SobaId { get; set; }

    public virtual Odjel? Odjel { get; set; }

    public virtual Soba? Soba { get; set; }
}
