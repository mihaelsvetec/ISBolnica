using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class Pacijent
{
    public int PacijentId { get; set; }

    public string? Oib { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Spol { get; set; }

    public string? KrvnaGrupa { get; set; }

    public string? Alergije { get; set; }

    public DateTime? DatumRodenja { get; set; }

    public virtual ICollection<PovijestBolesti> PovijestBolestis { get; set; } = new List<PovijestBolesti>();
}
