using ISBolnicaBLL.Pacijent;
using ISBolnicaMVC.Models;

namespace ISBolnicaMVC.Pacijent
{
    public class PacijentiVM
    {
        public IEnumerable<PacijentVM> Pacijenti { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
