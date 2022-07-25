namespace stripe.Services
{
    public class Customer
    {
        public int Id { get; set; }
        public string StripeId { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
    }
}