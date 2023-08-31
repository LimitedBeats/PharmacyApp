namespace PharmacyConsole.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Batch> BatchList { get; set; }

        public override string ToString()
        {
            return $"{nameof(ProductId)}: {ProductId}, {nameof(ProductName)}, {ProductName}";
        }
    }
}
