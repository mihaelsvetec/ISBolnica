using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess;

public partial class ZdravstveniRadnik
{
    public int? ZaposlenikId { get; set; }

    public int? RadnoMjesto { get; set; }

    public string? OpisRadnogMjesta { get; set; }

    public virtual Zaposlenik? Zaposlenik { get; set; }
}
