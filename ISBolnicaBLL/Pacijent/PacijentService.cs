using ISBolnicaDAL.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ISBolnicaBLL.Pacijent
{
    public class PacijentService : IPacijentService
    {
        private readonly IsBolnicaContext _isBolnicaContext;

        public PacijentService()
        {
            _isBolnicaContext = new IsBolnicaContext();
        }

        public int GetPacijentCount()
        {
            return _isBolnicaContext.Pacijents.AsNoTracking().Count();
        }

        public List<PacijentVM> GetAllPacijent(int pageNumber, int pageSize, bool ascending, int sort)
        {
            var query = _isBolnicaContext.Pacijents.AsNoTracking();

            System.Linq.Expressions.Expression<Func<ISBolnicaDAL.DataAccess.Entities.Pacijent, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = x => x.PacijentId;
                    break;
                case 2:
                    orderSelector = x => x.Oib;
                    break;
                case 3:
                    orderSelector = x => x.Ime;
                    break;
                case 4:
                    orderSelector = x => x.Prezime;
                    break;
                case 5:
                    orderSelector = x => x.Spol;
                    break;
                case 6:
                    orderSelector = x => x.KrvnaGrupa;
                    break;
                case 7:
                    orderSelector = x => x.DatumRodenja;
                    break;
            }
            if (orderSelector != null)
                query = ascending ? query.OrderBy(orderSelector) : query.OrderByDescending(orderSelector);

            var pacijenti = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return pacijenti.Select(x => new PacijentVM
            {
                PacijentId = x.PacijentId,
                Alergije = x.Alergije ?? string.Empty,
                DatumRodenja = x.DatumRodenja.Value.Date,
                Ime = x.Ime,
                KrvnaGrupa = x.KrvnaGrupa,
                Oib = x.Oib,
                Prezime = x.Prezime,
                Spol = x.Spol
            }).ToList();
        }

        public void AddPacijent(AddPacijent pacijent)
        {
            if (_isBolnicaContext.Pacijents.Any(x => x.Oib == pacijent.Oib))
                throw new Exception("Pacijent s ovim OIB-om već se  nalazi u sustavu");

            var error = new StringBuilder();
            if (string.IsNullOrEmpty(pacijent.Ime))
                error.AppendLine("Ime je obavezno");
            if (string.IsNullOrEmpty(pacijent.Prezime))
                error.AppendLine("Prezime je obavezno");
            if (string.IsNullOrEmpty(pacijent.Spol))
                error.AppendLine("Spol je obavezan");
            if (string.IsNullOrEmpty(pacijent.KrvnaGrupa))
                error.AppendLine("Krvna grupa je obavezna");
            if (!pacijent.DatumRodenja.HasValue)
                error.AppendLine("Datum rođenja je obavezan");
            if (string.IsNullOrEmpty(pacijent.Oib))
                error.AppendLine("OIB je obavezan");
            else if (pacijent.Oib.Length != 11)
                error.AppendLine("OIB mora sadržavati 11 znamenaka");
            else if (!pacijent.Oib.ToArray<char>().All(x => char.IsDigit(x)))
                error.AppendLine("OIB mora sadržavati samo znamenke");
            else if (!ValidateOib(pacijent.Oib))
                error.AppendLine("OIB nije valjan");

            if (error.Length != 0)
                throw new Exception(error.ToString());

            var newPacijent = new ISBolnicaDAL.DataAccess.Entities.Pacijent
            {
                Oib = pacijent.Oib,
                Ime = pacijent.Ime,
                Prezime = pacijent.Prezime,
                Spol = pacijent.Spol,
                KrvnaGrupa = pacijent.KrvnaGrupa,
                DatumRodenja = pacijent.DatumRodenja,
                Alergije = string.IsNullOrEmpty(pacijent.Alergije) ? null : pacijent.Alergije
            };

            _isBolnicaContext.Pacijents.Add(newPacijent);
            _isBolnicaContext.SaveChanges();
        }

        public void DeletePacijent(int id)
        {
            var terminiUredajaToDelete = _isBolnicaContext.TerminUredajs.Where(x => x.PacijentId == id).ToList();
            _isBolnicaContext.TerminUredajs.RemoveRange(terminiUredajaToDelete);

            var povijestiBolestiToDelete = _isBolnicaContext.PovijestBolestis.Where(x => x.PacijentId == id).ToList();
            _isBolnicaContext.PovijestBolestis.RemoveRange(povijestiBolestiToDelete);

            var ids = povijestiBolestiToDelete.Select(x => x.PovijestBolestiId).ToList();
            var povBolTerpaijeToDelete = _isBolnicaContext.PovijestBolestiTerapijas.Where(x => ids.Contains(x.PovijestBolestiId)).ToList();
            _isBolnicaContext.PovijestBolestiTerapijas.RemoveRange(povBolTerpaijeToDelete);

            var pacijentToDelete = _isBolnicaContext.Pacijents.First(x => x.PacijentId == id);
            _isBolnicaContext.Pacijents.Remove(pacijentToDelete);

            _isBolnicaContext.SaveChanges();
        }

        public PacijentDetail GetPacijentDetail(int pacijentId)
        {
            var pacijent = _isBolnicaContext.Pacijents.First(x => x.PacijentId == pacijentId);

            var pacijentDetail = new PacijentDetail
            {
                PacijentId = pacijentId,
                Oib = pacijent.Oib,
                Ime = pacijent.Ime,
                Prezime = pacijent.Prezime,
                Spol = pacijent.Spol,
                KrvnaGrupa = pacijent.KrvnaGrupa,
                DatumRodenja = pacijent.DatumRodenja.Value,
                Alergije = string.IsNullOrEmpty(pacijent.Alergije) ? null : pacijent.Alergije,
                PovijestiBolesti = new List<PovijestBolestiVM>()
            };

            var povijestiBolesti = _isBolnicaContext.PovijestBolestis.Where(x => x.PacijentId == pacijentId).OrderByDescending(x => x.PovijestBolestiId).ToList();

            var doktorIds = povijestiBolesti.Select(x => x.DoktorId).ToList();

            var doktori = _isBolnicaContext.Zaposleniks.Where(x => doktorIds.Contains(x.ZaposlenikId)).ToList();

            foreach (var pb in povijestiBolesti)
            {
                var d = doktori.First(x => x.ZaposlenikId == pb.DoktorId);

                pacijentDetail.PovijestiBolesti.Add(new PovijestBolestiVM
                {
                    PovijestBolestiId = pb.PovijestBolestiId,
                    VrijemeOtvaranja = pb.VrijemeOtvaranja.Value,
                    VrijemeZatvaranja = pb.VrijemeZatvaranja,
                    Sazetak = pb.Sazetak,
                    Doktor = $"{d.Ime} {d.Prezime}"
                });
            }

            return pacijentDetail;
        }

        #region Helper methods
        public bool ValidateOib(string oib)
        {
            var a = 10;
            for (var i = 0; i < 10; i++)
            {
                a = a + int.Parse(oib.Substring(i, 1));
                a = a % 10;
                if (a == 0) a = 10;
                a *= 2;
                a = a % 11;
            }
            var kontrolni = 11 - a;
            if (kontrolni == 10) kontrolni = 0;
            if (kontrolni != int.Parse(oib.Substring(10, 1)))
                return false;
            return true;
        }
        #endregion
    }
}
