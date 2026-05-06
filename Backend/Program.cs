using Aplication.UseCases.Movements;
using Aplication.UseCases.Products;
using Aplication.UseCases.Suppliers;
using Aplication.UseCases.Users;
using Application.Abstractions;
using Application.Common;
using Application.UseCases.Movements;
using Application.UseCases.Products;
using Application.UseCases.Suppliers;
using Application.UseCases.Webhooks;
using Backend.DTOs;
using Backend.DTOs.Movement;
using Backend.DTOs.Supplier;
using Backend.DTOs.User;
using Backend.DTOs.WebHook;
using Backend.WebSockets;
using Data;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Net.WebSockets;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddScoped<IRepository<ProductEntity>, ProductRepository>();
builder.Services.AddScoped<IRepository<MovementEntity>, MovementRepository>();
builder.Services.AddScoped<IRepository<SupplierEntity>, SupplierRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

builder.Services.AddSingleton<WebSocketConnectionManager>();
builder.Services.AddSingleton<IStockNotifier, WebSocketStockNotifier>();

// Product use cases registration
builder.Services.AddScoped<GetAllProductsHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<UpdateProductHandler>();
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<DeleteProductByIdHandler>();

// Movement use cases registration
builder.Services.AddScoped<GetAllMovementsHandler>();
builder.Services.AddScoped<GetMovementByIdHandler>();
builder.Services.AddScoped<UpdateMovementHandler>();
builder.Services.AddScoped<CreateMovementHandler>();
builder.Services.AddScoped<DeleteMovementByIdHandler>();

// Supplier use cases registration
builder.Services.AddScoped<GetAllSuppliersHandler>();
builder.Services.AddScoped<GetSupplierByIdHandler>();
builder.Services.AddScoped<UpdateSupplierHandler>();
builder.Services.AddScoped<CreateSupplierHandler>();
builder.Services.AddScoped<DeleteSupplierByIdHandler>();

// User use cases registration
builder.Services.AddScoped<CreateUserHandler>();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is InvalidOperationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exception.Message);
        }
        else if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(exception.Message);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(exception?.InnerException?.Message ?? exception?.Message ?? "An unexpected error occurred.");
        }
    });
});
app.UseWebSockets();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

#region Product endpoints
app.MapGet("product", async ([FromServices] GetAllProductsHandler useCase) =>
{
    var products = await useCase.Handle();
    return Results.Ok(products);

}).WithName("getallproducts");

app.MapGet("product/{id}", async (int id, [FromServices] GetProductByIdHandler useCase) =>
{
    var product = await useCase.Handle(id);
    return product is null ? Results.NotFound("Product not found") : Results.Ok(product);

}).WithName("getproduct");

app.MapPost("product", async (UpdateProductDTO dto, [FromServices] CreateProductHandler useCase) =>
{
    ProductEntity product = new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Code = dto.Code,
        Stock = dto.Stock,
        MinimumStock = dto.MinimumStock,
        SupplierId = dto.SupplierId,
    };
    await useCase.Handle(product);
    return Results.Created();

}).WithName("createproduct");

app.MapPut("product", async (UpdateProductDTO dto, [FromServices] UpdateProductHandler updateProductUseCase) =>
{
    ProductEntity product = new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Code = dto.Code,
        Stock = dto.Stock,
        MinimumStock = dto.MinimumStock,
        SupplierId = dto.SupplierId,
    };
    await updateProductUseCase.Handle(product);
    return Results.Ok("Updated product");

}).WithName("editproduct");

app.MapDelete("product/{id}", async (int id, [FromServices] DeleteProductByIdHandler useCase) =>
{
    await useCase.Handle(id);
    return Results.Ok();

}).WithName("deleteproduct");
#endregion

#region Movement endpoints
app.MapGet("movement", async ([FromServices] GetAllMovementsHandler useCase) =>
{
    var movements = await useCase.Handle();
    return Results.Ok(movements);

}).WithName("getallmovements");

app.MapGet("movement/{id}", async (int id, [FromServices] GetMovementByIdHandler useCase) =>
{
    var movement = await useCase.Handle(id);
    return Results.Ok(movement);

}).WithName("getmovement");

app.MapPost("movement", async (CreateMovementDTO dto, [FromServices] CreateMovementHandler useCase) =>
{
    MovementEntity movement = new()
    {
        Quantity = dto.Quantity,
        Date = dto.Date,
        Type = dto.Type,
        ProductId = dto.ProductId,
    };
    await useCase.Handle(movement);

    return Results.Created();

}).WithName("createmovement");

app.MapPut("movement", async (UpdateMovementDTO dto, [FromServices] UpdateMovementHandler useCase) =>
{
    MovementEntity movement = new()
    {
        Id = dto.Id,
        Quantity = dto.Quantity,
        Date = dto.Date,
        Type = dto.Type,
        ProductId = dto.ProductId,
    };
    await useCase.Handle(movement);

    return Results.Ok("Movement created");

}).WithName("editmovement");

app.MapDelete("movement/{id}", async (int id, [FromServices] DeleteMovementByIdHandler useCase) =>
{
    await useCase.Handle(id);
    return Results.Ok();

}).WithName("deletemovement");
#endregion

#region Supplier endpoints
app.MapGet("supplier", async ([FromServices] GetAllSuppliersHandler useCase) =>
{
    var suppliers = await useCase.Handle();
    return Results.Ok(suppliers);

}).WithName("getallsuppliers");

app.MapGet("supplier/{id}", async (int id, [FromServices] GetSupplierByIdHandler useCase) =>
{
    var supplier = await useCase.Handle(id);
    return Results.Ok(supplier);

}).WithName("getsupplier");

app.MapPost("supplier", async (CreateSupplierDTO dto, [FromServices] CreateSupplierHandler useCase) =>
{
    SupplierEntity supplier = new()
    {
        Name = dto.Name,
        PhoneNumber = dto.PhoneNumber,
        Email = dto.Email
    };
    await useCase.Handle(supplier);

    return Results.Created();

}).WithName("createsupplier");

app.MapPut("supplier", async (UpdateSupplierDTO dto, [FromServices] UpdateSupplierHandler useCase) =>
{
    SupplierEntity supplier = new()
    {
        Id = dto.Id,
        Name = dto.Name,
        PhoneNumber = dto.PhoneNumber,
        Email = dto.Email
    };
    await useCase.Handle(supplier);

    return Results.Ok("Updated supplier");
}).WithName("editsupplier");

app.MapDelete("supplier/{id}", async (int id, [FromServices] DeleteSupplierByIdHandler useCase) =>
{
    await useCase.Handle(id);
    return Results.Ok();

}).WithName("deletesupplier");
#endregion

#region User endpoint
app.MapGet("user", async ([FromServices] GetAllSuppliersHandler useCase) =>
{
    var suppliers = await useCase.Handle();
    return Results.Ok(suppliers);

}).WithName("getallusers");

app.MapGet("user/{id}", async (int id, [FromServices] GetSupplierByIdHandler useCase) =>
{
    var supplier = await useCase.Handle(id);
    return Results.Ok(supplier);

}).WithName("getuser");

app.MapPost("user", async (CreateUserDTO dto, [FromServices] CreateUserHandler useCase) =>
{
    UserEntity user = new()
    {
        Username = dto.UserName,
        Password = dto.Password,
        Role = dto.Role,
    };
    await useCase.Handle(user);

    return Results.Created();

}).WithName("createuser");
#endregion

#region WebSocket endpoint
app.Map("ws", async (HttpContext context, WebSocketConnectionManager manager) =>
{
    var socket = await context.WebSockets.AcceptWebSocketAsync();
    var id = Guid.NewGuid().ToString();
    manager.CreateConnection(id, socket);
    var buffer = new byte[1024];

    if (context.WebSockets.IsWebSocketRequest)
    {
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                break;
            }
        }

        manager.RemoveConnection(id);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});
#endregion

#region WebHook endpoint
app.MapPost("wh", async (WebHookDTO dto, [FromServices] RegisterWebhookHandler useCase) =>
{
   
});
#endregion

app.Run();


