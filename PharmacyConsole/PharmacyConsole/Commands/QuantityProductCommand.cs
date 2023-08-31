using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.Commands
{
    public class QuantityProductCommand
    {
        private readonly ICommandRepository commandRepository;
        public QuantityProductCommand(ICommandRepository commandRepository)
        {
            this.commandRepository = commandRepository;
        }

        public async Task Execute()
        {
            Console.WriteLine("Write a pharmacyID from 1 to 5");

            if (!Int32.TryParse(Console.ReadLine(), out int pharmacyID))
            {
                Console.WriteLine("It's not number");
                return;
            }

            var list = await commandRepository.SelectQuantityProduct(pharmacyID);

            if (list.Count == 0)
            {
                Console.WriteLine("Not products for current pharmacy");
                return;
            }

            var first = list[0];

            Console.WriteLine($"{first.PharmacyName} has {list.Count} product");

            foreach (var item in list)
            {
                Console.WriteLine($"ProductName: {item.ProductName}, Quantity: {item.Quantity}");
            }
        }
    }
}
