using TofikKursovoyApiModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TofikKursovoyApiModels.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KursovoyDbContext>(options =>
{
    options.UseNpgsql("server=localhost;user id=postgres;password=1234;database=KursovoyDb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("GetDiscounts", (KursovoyDbContext db) => db.Discounts.ToList());

app.MapGet("GetProducts", (KursovoyDbContext db) => db.Products.ToList());

app.MapGet("GetTypes", (KursovoyDbContext db) => db.Typeoftechnics.ToList());

app.MapGet("GetBrands", (KursovoyDbContext db) => db.Brands.ToList());

app.MapPost("AddClient", Client (KursovoyDbContext db , Client client)=>
{
    db.Clients.Add(client);
    db.SaveChanges();
    return client;

});
app.MapGet("GetClient", Client (KursovoyDbContext db, string uuid) =>
{
    var find = db.Clients.Where(user => user.Id == uuid).FirstOrDefault();
    if(find == null)
    {
        return null;
    }
    return find;
});

app.MapDelete("DeleteCartProduct", (KursovoyDbContext db, int id) =>
{
    var product = db.Productorders.Where(x => x.IdProduct == id && x.IdOrder == 2).FirstOrDefault();
    db.Productorders.Remove(product);
    db.SaveChanges();
});
app.MapPost("RefreshClient", bool (KursovoyDbContext db, Client client) =>
{
    if (client == null)
    {
        return false;
    }

    var findClient = db.Clients.Where(user => user.Id == client.Id).FirstOrDefault();
    if (findClient == null)
    {
        return false;
    }
    findClient.Name = client.Name;
    findClient.Surname = client.Surname;
    findClient.Patronymic = client.Patronymic;
    findClient.Phone = client.Phone;
    db.SaveChanges();
    return true;
});
app.MapPost("AddToCart",(KursovoyDbContext db, CombineToCart combine) =>
{
    var order = db.Orders.Where(x => x.IdClient == combine.Uuid).FirstOrDefault();
    Productorder productorder = new Productorder()
    {
        IdOrder = order.Id,
        IdProduct = combine.IdProduct,
    };
    db.Productorders.Add(productorder);
    db.SaveChanges();
});
app.MapGet("GetCart", List<Product> (KursovoyDbContext db, string uuid) =>
{
    var order = db.Orders.Where(x => x.IdClient == uuid).FirstOrDefault();

    var users = from orders in db.Orders
                join Productorder in db.Productorders on orders.Id equals Productorder.IdOrder
                join Products in db.Products on Productorder.IdProduct equals Products.Id
                select new
                {
                    Id = Products.Id,
                    Brandname = Products.Brandname,
                    Photoname = Products.Photoname,
                    Name = Products.Name,
                    ShortDescription = Products.ShortDescription,
                    Cost = Products.Cost,
                    Existance = Products.Existance,
                    Description = Products.Description,
                };
    List<Product> answer = users.Select(x => new Product
    {
        Id = x.Id,
        Brandname = x.Brandname,
        Photoname =x.Photoname,
        Name = x.Name,
        ShortDescription = x.ShortDescription,
        Cost = x.Cost,
        Existance = x.Existance,
        Description = x.Description,
    }).ToList();
    if(answer.Count == 0)
    {
        return null;
    }
    return answer;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

