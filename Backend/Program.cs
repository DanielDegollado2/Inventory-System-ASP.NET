using Aplication.UseCases.Movements;
using Aplication.UseCases.Products;
using Application.Common;
using Application.UseCases.Movements;
using Application.UseCases.Products;
using Backend.DTOs;
using Backend.DTOs.Movement;
using Data;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InventoryContext>(options =>
{
    options.UseSqlServer(connection);
});
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddScoped<IRepository<ProductEntity>, ProductRepository>();
builder.Services.AddScoped<IRepository<MovementEntity>, MovementRepository>();
builder.Services.AddScoped<IRepository<SupplierEntity>, SupplierRepository>();

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
builder.Services.AddScoped<DeleteMovementByIdHandler>();

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
            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();

#region Product endpoints
app.MapGet("product", async (GetAllProductsHandler useCase) =>
{
    var products = await useCase.Handle();
    return Results.Ok(products);

}).WithName("getallproducts");

app.MapGet("product/{id}", async (int id, GetProductByIdHandler useCase) =>
{
    var product = await useCase.Handle(id);
    return product is null ? Results.NotFound("Product not found") : Results.Ok(product);

}).WithName("getproduct");

app.MapPost("product", async (CreateProductDTO dto, CreateProductHandler useCase) =>
{
    ProductEntity product = new()
    {
        Name = dto.Name,
        Code = dto.Code,
        Stock = dto.Stock,
        MinimumStock = dto.MinimumStock,
        SupplierId = dto.SupplierId,
    };
    await useCase.Handle(product);
    return Results.Created();

}).WithName("createproduct");

app.MapPut("product", async (UpdateProductDTO dto, UpdateProductHandler updateProductUseCase) =>
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

app.MapDelete("product/{id}", async (int id, DeleteProductByIdHandler useCase) =>
{
    await useCase.Handle(id);
    return Results.Ok();

}).WithName("deleteproduct");
#endregion

#region Movement endpoints
app.MapGet("movement", async (GetAllMovementsHandler useCase) =>
{
    var movements = await useCase.Handle();
    return Results.Ok(movements);

}).WithName("getallmovements");

app.MapGet("movement/{id}", async (int id, GetMovementByIdHandler useCase) =>
{
    var movement = await useCase.Handle(id);
    return Results.Ok(movement);

}).WithName("getmovement");

app.MapPost("movement", async (CreateMovementDTO dto, CreateMovementHandler useCase) =>
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

app.MapPut("movement", async (UpdateMovementDTO dto, UpdateMovementHandler useCase) =>
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
}).WithName("editmovement");

app.MapDelete("movement/{id}", async (int id, DeleteMovementByIdHandler useCase) =>
{
    await useCase.Handle(id);
    return Results.Ok();

}).WithName("deletemovement");
#endregion

app.Run();


