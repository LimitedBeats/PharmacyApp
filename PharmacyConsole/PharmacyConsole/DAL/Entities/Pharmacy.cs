namespace PharmacyConsole.DAL.Entities
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PhoneNumber { get; set; }

        public List<Warehouse> WarehouseList { get; set; }

        public override string ToString()
        {
            return $"{nameof(PharmacyId)}: {PharmacyId}, {nameof(PharmacyName)}: {PharmacyName}, {nameof(PhoneNumber)}: {PhoneNumber}";
        }
    }
}
