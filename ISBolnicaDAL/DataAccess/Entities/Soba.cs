using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class Soba
{
    public int SobaId { get; set; }

    public string? OznakaSobe { get; set; }

    public int? KategorijaSobe { get; set; }

    public int? Kapacitet { get; set; }

    public int? OdjelId { get; set; }

    public virtual Odjel? Odjel { get; set; }

    public virtual ICollection<PovijestBolesti> PovijestBolestis { get; set; } = new List<PovijestBolesti>();

    public virtual ICollection<Uredaj> Uredajs { get; set; } = new List<Uredaj>();
}
