using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.PovijestBolesti
{
    public class PovijestBolestiDetail
    {
        public int PovijestBolestiId { get; set; }
        public DateTime VrijemeOtvaranja { get; set; }
        public DateTime? VrijemeZatvaranja { get; set; }
        public string Sazetak { get; set; }
        public string Simptomi { get; set; }
        public string Dijagnoza { get; set; }
        public int PacijentId { get; set; }
        public string Pacijent { get; set; }
        public int DoktorId { get; set; }
        public string Doktor { get; set; }
        public int SobaId { get; set; }
        public string Soba { get; set; }
        public string Odjel { get; set; }
        public int TerapijaId { get; set; }
    }

}
