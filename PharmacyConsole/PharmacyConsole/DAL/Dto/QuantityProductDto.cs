namespace PharmacyConsole.DAL.Dto
{
    public class QuantityProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int PharmacyID { get; set; }
        public string PharmacyName { get; set; }
    }
}
