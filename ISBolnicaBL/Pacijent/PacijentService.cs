using ISBolnicaDAL.DataAccess;

namespace ISBolnicaBL.Pacijent
{
    public class PacijentService : IPacijentService
    {
        private readonly IsBolnicaContext _isBolnicaContext;

        public PacijentService()
        {
            _isBolnicaContext = new IsBolnicaContext();
        }

        public List<PacijentVM> GetAllPacijent(int pageNumber, int pageSize)
        {
            var pacijenti = _isBolnicaContext.Pacijents.Skip(pageNumber * pageSize).Take(pageSize).ToList();

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
    }
}
