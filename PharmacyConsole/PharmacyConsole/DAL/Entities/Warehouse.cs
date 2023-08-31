namespace PharmacyConsole.DAL.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public int PharmacyId { get; set; }
        public List<Batch> BatchList { get; set; }

        public override string ToString()
        {
            return $"{nameof(WarehouseId)}: {WarehouseId}, {nameof(WarehouseName)} : {WarehouseName}";
        }
    }
}
