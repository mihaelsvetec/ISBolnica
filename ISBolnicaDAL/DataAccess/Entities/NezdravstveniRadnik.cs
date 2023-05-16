using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess.Entities;

public partial class NezdravstveniRadnik
{
    public int? ZaposlenikId { get; set; }

    public int? RazinaOvlasti { get; set; }

    public string? OpisRadnogMjesta { get; set; }

    public virtual Zaposlenik? Zaposlenik { get; set; }
}
