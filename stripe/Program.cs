using stripe.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Custom Lexacom setup.
var dataAccessLayer = new DataAccessLayer(Path.Combine(Directory.GetCurrentDirectory(), "db"));
await dataAccessLayer.PutCustomer(new stripe.Services.Customer { Id = 1, Email = "a@b.com", Name = "A B" });

// TODO: make this configuration
StripeConfiguration.ApiKey = "sk_test_51L1REtJNcUVNlmQAVovLUEtV58f4pbYr2UX5qBTjvLlQY0ERZUAqVvu1WMp1G424qt1l1ycOALVFABbkPxb28KFK00BbANxsPj";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
