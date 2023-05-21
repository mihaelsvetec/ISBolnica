using ISBolnicaBLL.PovijestBolesti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.Terapija
{
    public interface ITerapijaService
    {
        List<TerapijaVM> GetTerapijas(int pageNumber, int pageSize);
        int GetTerapijaCount();
        void AddTerapija(AddTerapija t);
        void DeleteTerapija(int id);
    }
}
