namespace PropertyComparison
{
    public class InvoiceAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public static InvoiceAddress FromAddress(Address address)
        {
            return new InvoiceAddress
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }
    }
}