using ISBolnicaBLL.Pacijent;
using Microsoft.AspNetCore.Mvc;

namespace ISBolnicaBllTest
{
    public class Tests
    {

        [Test]
        public void TestOib1()
        {
            var pacijentService = new PacijentService();

            var oib = "18339044416";

            Assert.IsTrue(pacijentService.ValidateOib(oib));
        }

        [Test]
        public void TestOib2()
        {
            var pacijentService = new PacijentService();

            var oib = "18339044415";

            Assert.IsFalse(pacijentService.ValidateOib(oib));
        }
    }
}