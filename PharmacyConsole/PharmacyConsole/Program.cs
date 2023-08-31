using PharmacyConsole.Commands;
using PharmacyConsole.DAL.Repositories.Implementation;
using PharmacyConsole.Managers.Implementation;
using PharmacyConsole.Settings;
using PharmacyConsole.Validators.Implementation;

var dbSettings = new DbSettings();
dbSettings.ProviderName = "System.Data.SqlClient";
dbSettings.ConnectionString = $@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Pharmacy;Integrated Security=True";

var productRepository = new ProductRepository(dbSettings);
var batchRepository = new BatchRepository(dbSettings);
var warehouseRepository = new WarehouseRepository(dbSettings);
var pharmacyRepository = new PharmacyRepository(dbSettings);
var commandRepository = new CommandRepository(dbSettings);
var commonRepository = new CommonRepository(dbSettings);

var bachValidator = new BatchValidator();
var pharmacyValidator = new PharmacyValidator();
var productValidator = new ProductValidator();
var warehouseValidator = new WarehouseValidator();

var batchManager = new BatchManager(batchRepository, bachValidator);
var warehouseManager = new WarehouseManager(batchRepository, warehouseRepository, warehouseValidator);
var productManager = new ProductManager(productRepository, batchRepository, productValidator);
var pharmacyManager = new PharmacyManager(pharmacyRepository, warehouseManager, pharmacyValidator);
var quantityProductCommand = new QuantityProductCommand(commandRepository);


await commonRepository.RepairData();

await quantityProductCommand.Execute();

var existProduct = await productRepository.Get(1);
var existPharmacy = await pharmacyRepository.Get(1);
var existWarehouse = await warehouseRepository.Get(1);
var existbatc = await batchRepository.Get(1);

await productManager.Insert(existProduct);
await pharmacyManager.Insert(existPharmacy);
await warehouseManager.Insert(existWarehouse);

existbatc.Quantity = -1;
await batchManager.Insert(existbatc);
existbatc.Quantity = 10;
await batchManager.Insert(existbatc);

await productManager.Delete(5);
await pharmacyManager.Delete(2);
await warehouseManager.Delete(3);
await batchManager.Delete(1);