using Microsoft.AspNetCore.Mvc;
using Stripe;
using stripe.Services;
using Customer = stripe.Services.Customer;

namespace stripe.Controllers
{
    
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {
        private readonly DataAccessLayer _dataAccessLayer;
        private readonly CustomerService _customerService;
        private readonly SetupIntentService _setupIntentService;

        public CustomerController()
        {
            // todo: use DI to inject dependencies
            _dataAccessLayer = new DataAccessLayer(Path.Combine(Directory.GetCurrentDirectory(), "db"));
            _customerService = new CustomerService();
            _setupIntentService = new SetupIntentService();
        }

        [HttpGet("{id:int}/setupintent")]
        public async Task<IActionResult> GetSetupIntent(int id)
        {       
            try
            {
                var customer = await GetStripeCustomer(id);
                var options = new SetupIntentCreateOptions
                {
                    Customer = customer.Id,
                    // todo: make this configurable so we can extend in future
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var result = await _setupIntentService.CreateAsync(options);

                return Ok(new { clientSecret = result.ClientSecret });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Stripe.Customer> GetStripeCustomer(int id)
        {
            try
            {
                var customer = await _dataAccessLayer.GetCustomer(id);
                var stripeCustomer = string.IsNullOrEmpty(customer.StripeId) 
                    ? null 
                    : await _customerService.GetAsync(customer.StripeId);

                // if we didn't find a customer in Stripe, create one
                if (stripeCustomer == null)
                {
                    stripeCustomer = await _customerService.CreateAsync(new CustomerCreateOptions
                    {
                        Email = customer.Email,
                        Name = customer.Name
                    });

                    customer.StripeId = stripeCustomer.Id;
                    await _dataAccessLayer.PutCustomer(customer);
                }

                return stripeCustomer;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}