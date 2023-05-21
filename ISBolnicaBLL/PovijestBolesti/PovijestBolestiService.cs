using ISBolnicaDAL.DataAccess;
using ISBolnicaDAL.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBolnicaBLL.PovijestBolesti
{
    public class PovijestBolestiService : IPovijestBolestiService
    {
        private readonly IsBolnicaContext _isBolnicaContext;

        public PovijestBolestiService()
        {
            _isBolnicaContext = new IsBolnicaContext();
        }

        public PovijestBolestiDetail GetPovijestBolestiDetail(int id)
        {
            var pb = _isBolnicaContext.PovijestBolestis.First(x => x.PovijestBolestiId == id);
            var pacijent = _isBolnicaContext.Pacijents.First(x => x.PacijentId == pb.PacijentId);
            var doktor = _isBolnicaContext.Zaposleniks.First(x => x.ZaposlenikId == pb.DoktorId);
            var soba = _isBolnicaContext.Sobas.FirstOrDefault(x => x.SobaId == pb.SobaId);
            var terapijaIds = _isBolnicaContext.PovijestBolestiTerapijas.Where(x => x.PovijestBolestiId == id).Select(x => x.TerapijaId).ToList();
            var terapije = _isBolnicaContext.Terapijas.Where(x => terapijaIds.Contains(x.TerapijaId)).ToList();

            var pbd = new PovijestBolestiDetail
            {
                Dijagnoza = pb.Dijagnoza,
                Doktor = $"{doktor.Ime} {doktor.Prezime}",
                DoktorId = doktor.ZaposlenikId,
                Odjel = soba != null ? _isBolnicaContext.Odjels.First(x => x.OdjelId == soba.OdjelId).Ime : string.Empty,
                Pacijent = $"{pacijent.Ime} {pacijent.Prezime}",
                PacijentId = pacijent.PacijentId,
                PovijestBolestiId = pb.PovijestBolestiId,
                Sazetak = pb.Sazetak,
                Simptomi = pb.Simptomi,
                Soba = soba != null ? soba.OznakaSobe : string.Empty,
                SobaId = soba != null ? soba.SobaId : 0,
                VrijemeOtvaranja = pb.VrijemeOtvaranja.Value,
                VrijemeZatvaranja = pb.VrijemeZatvaranja,
                TerapijaId = pb.TerapijaId.HasValue ? pb.TerapijaId.Value : 0
            };

            return pbd;
        }

        public List<DoktorVM> GetDoktors()
        {
            var doktorIds = _isBolnicaContext.ZdravstveniRadniks.Where(x => x.RadnoMjesto == 1).Select(x => x.ZaposlenikId).ToList();
            var doktors = _isBolnicaContext.Zaposleniks
                .Where(x => doktorIds.Contains(x.ZaposlenikId))
                .Select(x => new DoktorVM
                {
                    DoktorId = x.ZaposlenikId,
                    Doktor = $"{x.Ime} {x.Prezime}"
                }).ToList();
            return doktors;
        }

        public List<SobaVM> GetSobas()
        {
            var sobas = _isBolnicaContext.Sobas.Select(x => new SobaVM
            {
                SobaId = x.SobaId,
                Soba = $"{x.OznakaSobe} ({_isBolnicaContext.Odjels.First(y => y.OdjelId == x.OdjelId).Ime})"
            }).ToList();

            return sobas;
        }

        public List<TerapijaVM> GetTerapijas()
        {
            var terapijas = _isBolnicaContext.Terapijas.Select(x => new TerapijaVM
            {
                TerapijaId = x.TerapijaId,
                Terapija = x.OpisTerapije
            }).ToList();

            return terapijas;
        }

        public void EditPovijestBolesti(PovijestBolestiDetail pbd)
        {
            var error = new StringBuilder();
            if (pbd.DoktorId == 0 || pbd.DoktorId == null)
                error.AppendLine("Doktor je obavezan");

            if (error.Length != 0)
                throw new Exception(error.ToString());

            var pb = _isBolnicaContext.PovijestBolestis.FirstOrDefault(x => x.PovijestBolestiId == pbd.PovijestBolestiId) ?? new ISBolnicaDAL.DataAccess.Entities.PovijestBolesti();

            pb.Dijagnoza = pbd.Dijagnoza;
            pb.DoktorId = pbd.DoktorId;
            pb.Sazetak = pbd.Sazetak;
            pb.Simptomi = pbd.Simptomi;
            if (pbd.SobaId != 0)
                pb.SobaId = pbd.SobaId;
            if (pbd.TerapijaId != 0)
                pb.TerapijaId = pbd.TerapijaId;

            if (pbd.PovijestBolestiId == 0 || pbd.PovijestBolestiId == null)
            {
                pb.PacijentId = pbd.PacijentId;
                pb.VrijemeOtvaranja = DateTime.UtcNow;
                _isBolnicaContext.PovijestBolestis.Add(pb);
            }
            else
            {
                _isBolnicaContext.PovijestBolestis.Update(pb);
            }
            _isBolnicaContext.SaveChanges();
        }

        public void DeletePovijestBolesti(int id)
        {
            var toDelete = _isBolnicaContext.PovijestBolestis.First(x => x.PovijestBolestiId == id);
            _isBolnicaContext.PovijestBolestis.Remove(toDelete);
            _isBolnicaContext.SaveChanges();
        }

        public void ClosePovijestBolesti(int id)
        {
            var pb = _isBolnicaContext.PovijestBolestis.First(x => x.PovijestBolestiId == id);
            pb.VrijemeZatvaranja = DateTime.UtcNow;
            _isBolnicaContext.PovijestBolestis.Update(pb);
            _isBolnicaContext.SaveChanges();
        }
    }
}
