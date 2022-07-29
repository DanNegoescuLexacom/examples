var app = WebApplication.Create(args);

app.MapGet("/add", (int a, int b) => a + b);
app.MapGet("/subtract", (int a, int b) => a - b);
app.MapGet("/multiply", (int a, int b) => a * b);
app.MapGet("/divide", (int a, int b) => a / b);

app.Run();
