using ISBolnicaBLL.PovijestBolesti;
using ISBolnicaMVC.Models;

namespace ISBolnicaMVC.Terapija
{
    public class TerapijeVM
    {
        public List<TerapijaVM> Terapijas { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
