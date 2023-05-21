using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.PovijestBolesti
{
    public interface IPovijestBolestiService
    {
        PovijestBolestiDetail GetPovijestBolestiDetail(int id);
        List<DoktorVM> GetDoktors();
        List<SobaVM> GetSobas();
        List<TerapijaVM> GetTerapijas();
        void EditPovijestBolesti(PovijestBolestiDetail pbd);
        void DeletePovijestBolesti(int id);
        void ClosePovijestBolesti(int id);
    }
}
