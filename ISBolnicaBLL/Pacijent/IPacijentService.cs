namespace ISBolnicaBLL.Pacijent
{
    public interface IPacijentService
    {
        int GetPacijentCount();
        List<PacijentVM> GetAllPacijent(int pageNumber, int pageSize, bool ascending, int sort);
        void AddPacijent(AddPacijent pacijent);
        void DeletePacijent(int id);
        PacijentDetail GetPacijentDetail(int pacijentId);
    }
}
