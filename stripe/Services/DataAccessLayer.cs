using Newtonsoft.Json;

namespace stripe.Services
{
    public class DataAccessLayer
    {
        private readonly string _customerFilePath;

        public DataAccessLayer(string dbPath)
        {
            Directory.CreateDirectory(dbPath);
            _customerFilePath = Path.Combine(dbPath, "customers.json");
            if (!File.Exists(_customerFilePath))
            {
                File.WriteAllText(_customerFilePath, "[]");
            };
        }
        
        public async Task<Customer> GetCustomer(int id)
        {
            var json = await File.ReadAllTextAsync(_customerFilePath);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            return customers.First(c => c.Id == id);
        }

        public async Task PutCustomer(Customer customer)
        {
            var json = await File.ReadAllTextAsync(_customerFilePath);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            var existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer == null)
            { 
                existingCustomer = new Customer { Id = customer.Id };
                customers.Add(existingCustomer);
            }
            existingCustomer.StripeId = customer.StripeId;
            existingCustomer.Email = customer.Email;
            existingCustomer.Name = customer.Name;
            await File.WriteAllTextAsync(_customerFilePath, JsonConvert.SerializeObject(customers));
        }
    }
}