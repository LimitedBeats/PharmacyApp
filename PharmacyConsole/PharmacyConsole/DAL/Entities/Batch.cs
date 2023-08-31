namespace PharmacyConsole.DAL.Entities
{
    public class Batch
    {
        public int BatchId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        public override string ToString()
        {
            return $"{nameof(BatchId)}: {BatchId} {nameof(Quantity)}: {Quantity}";
        }
    }
}
