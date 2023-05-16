using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess;

public partial class Odjel
{
    public int OdjelId { get; set; }

    public string? Ime { get; set; }

    public string? Opis { get; set; }

    public string? Lokacija { get; set; }

    public virtual ICollection<Soba> Sobas { get; set; } = new List<Soba>();

    public virtual ICollection<Uredaj> Uredajs { get; set; } = new List<Uredaj>();

    public virtual ICollection<Zaposlenik> Zaposleniks { get; set; } = new List<Zaposlenik>();
}
