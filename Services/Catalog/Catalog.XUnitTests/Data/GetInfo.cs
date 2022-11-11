using Catalog.Domain.Entities;

namespace Catalog.XUnitTests.Data
{
    internal class GetInfo
    {
        public static List<SellerInfo> GetSellers() => new()
        {
            new SellerInfo()
            {
                Id = 1,
                SellerDescription = "Test test lalal",
                SellerName = "Alex",
                SellerPhoneNumber = "+37294838123"
            },
            new SellerInfo()
            {
                Id = 2,
                SellerDescription = "Tesawdawdt tesawdwdt awdawd",
                SellerName = "Hleb",
                SellerPhoneNumber = "+37294838123"
            },
        };

        public static List<WigwamsInfo> GetWigwams() => new()
        {
            new WigwamsInfo()
            {
                Id = 1,
                Description = "Test test test test test",
                Height = 10,
                Weight = 10,
                Width = 10,
                Price = 200m,
                WigwamsName = "tEST TEST TEST TEST",
                SellerInfoId = 1,
                SellerInfo = new SellerInfo()
                {
                    Id = 1,
                    SellerDescription = "Test test lalal",
                    SellerName = "Alex",
                    SellerPhoneNumber = "+37294838123"
                },
            },
            new WigwamsInfo()
            {
                Id = 2,
                Description = "Test2 test2 test 2test 2test2",
                Height = 20,
                Weight = 20,
                Width = 20,
                Price = 21000m,
                WigwamsName = "tEST2 TEST 2TEST 2TEST2",
                SellerInfoId = 2,
                SellerInfo = new SellerInfo()
                {
                    Id = 2,
                    SellerDescription = "Tesawdawdt tesawdwdt awdawd",
                    SellerName = "Hleb",
                    SellerPhoneNumber = "+37294838123"
                },
            }
        };
    }
}
