using ISBolnicaBLL.PovijestBolesti;
using ISBolnicaDAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.Terapija
{
    public class TerapijaService : ITerapijaService
    {
        private readonly IsBolnicaContext _isBolnicaContext;

        public TerapijaService()
        {
            _isBolnicaContext = new IsBolnicaContext();
        }

        public void AddTerapija(AddTerapija t)
        {
            var terapija = new ISBolnicaDAL.DataAccess.Entities.Terapija
            {
                OpisTerapije = t.Opis
            };

            _isBolnicaContext.Terapijas.Add(terapija);
            _isBolnicaContext.SaveChanges();
        }

        public void DeleteTerapija(int id)
        {
            var t = _isBolnicaContext.Terapijas.First(x => x.TerapijaId == id);
            _isBolnicaContext.Terapijas.Remove(t);
            _isBolnicaContext.SaveChanges();
        }

        public int GetTerapijaCount()
        {
            return _isBolnicaContext.Terapijas.Count();
        }

        public List<TerapijaVM> GetTerapijas(int pageNumber, int pageSize)
        {
            var terapijas = _isBolnicaContext.Terapijas.OrderByDescending(x => x.TerapijaId).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new TerapijaVM
                {
                    TerapijaId = x.TerapijaId,
                    Terapija = x.OpisTerapije
                }).ToList();
            return terapijas;
        }
    }
}
