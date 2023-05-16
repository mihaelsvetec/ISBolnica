using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class Zaposlenik
{
    public int ZaposlenikId { get; set; }

    public string? Oib { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Email { get; set; }

    public bool? ZdravstevniRadnik { get; set; }

    public int? OdjelId { get; set; }

    public virtual Odjel? Odjel { get; set; }

    public virtual ICollection<PovijestBolesti> PovijestBolestis { get; set; } = new List<PovijestBolesti>();
}
