using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class MedicinskiProizvod
{
    public int MedicinskiProizvodId { get; set; }

    public string? Naziv { get; set; }

    public decimal? KolicinaPakiranja { get; set; }

    public string? MjernaJedinicaPakiranja { get; set; }

    public int? StanjeZaliha { get; set; }

    public int? KategorijaProizvoda { get; set; }
}
