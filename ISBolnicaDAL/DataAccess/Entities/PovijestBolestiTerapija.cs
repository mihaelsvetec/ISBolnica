using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class PovijestBolestiTerapija
{
    public int PovijestBolestiId { get; set; }

    public int TerapijaId { get; set; }

    public virtual PovijestBolesti PovijestBolesti { get; set; } = null!;

    public virtual Terapija Terapija { get; set; } = null!;
}
