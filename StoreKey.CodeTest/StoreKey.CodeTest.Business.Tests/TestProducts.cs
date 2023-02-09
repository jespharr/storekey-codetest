namespace StoreKey.CodeTest.Business.Tests
{
    internal static class TestProducts
    {
        public static readonly Product CocaCola = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Coca-Cola",
            Ean13 = "5449000000996",
            Price = 11.90m
        };

        public static readonly Product CocaColaLight = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Coca-Cola Light",
            Ean13 = "5449000050205",
            Price = 11.90m
        };

        public static readonly Product CokeZero = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Coke Zero",
            Ean13 = "5449000131805",
            Price = 11.90m
        };

        public static readonly Product Kexchoklad = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Kexchoklad",
            Ean13 = "7330869133322 ",
            Price = 8.95m
        };

        public static readonly Product CoCo = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "CoCo",
            Ean13 = "7622210812254",
            Price = 11.90m
        };

        public static readonly Product Japp = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Japp",
            Ean13 = "7040110663408",
            Price = 10.95m
        };
    }
}
