namespace ISBolnicaBL.Pacijent
{
    public interface IPacijentService
    {
        List<PacijentVM> GetAllPacijent(int pageNumber, int pageSize);
    }
}
