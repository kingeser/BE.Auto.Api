using BE.Auto.Api.Extensions;
using Example.AutoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IProductService, ProductService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvcCore().AddAutoApi();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();