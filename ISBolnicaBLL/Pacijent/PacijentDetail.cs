using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.Pacijent
{
    public class PacijentDetail
    {
        public int PacijentId { get; set; }
        public string Oib { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string KrvnaGrupa { get; set; }
        public string Alergije { get; set; }
        public DateTime DatumRodenja { get; set; }

        public List<PovijestBolestiVM> PovijestiBolesti { get; set; }
    }

    public class PovijestBolestiVM
    {
        public int PovijestBolestiId { get; set; }
        public DateTime VrijemeOtvaranja { get; set; }
        public DateTime? VrijemeZatvaranja { get; set; }
        public string Sazetak { get; set; }
        public string Doktor { get; set; }
    }
}
