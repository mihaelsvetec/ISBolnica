using System;
using System.Collections.Generic;

namespace ISBolnicaDAL.DataAccess;

public partial class TerminUredaj
{
    public int? UredajId { get; set; }

    public int? DoktorId { get; set; }

    public int? PacijentId { get; set; }

    public DateTime? DatumVrijeme { get; set; }

    public virtual Zaposlenik? Doktor { get; set; }

    public virtual Pacijent? Pacijent { get; set; }

    public virtual Uredaj? Uredaj { get; set; }
}
