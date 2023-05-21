using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class PovijestBolesti
{
    public int PovijestBolestiId { get; set; }

    public DateTime? VrijemeOtvaranja { get; set; }

    public DateTime? VrijemeZatvaranja { get; set; }

    public string? Sazetak { get; set; }

    public string? Simptomi { get; set; }

    public string? Dijagnoza { get; set; }

    public int? PacijentId { get; set; }

    public int? DoktorId { get; set; }

    public int? SobaId { get; set; }

    public int? TerapijaId { get; set; }

    public virtual Zaposlenik? Doktor { get; set; }

    public virtual Pacijent? Pacijent { get; set; }

    public virtual Soba? Soba { get; set; }
}
