namespace ISBolnicaBLL.Pacijent
{
    public class PacijentVM
    {
        public int PacijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Oib { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodenja { get; set; }
        public string KrvnaGrupa { get; set; }
        public string Alergije { get; set; }
    }
}
