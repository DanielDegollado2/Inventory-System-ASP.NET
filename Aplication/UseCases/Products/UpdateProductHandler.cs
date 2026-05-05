using Application.Abstractions;
using Application.Common;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    public class UpdateProductHandler
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<MovementEntity> _movementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockNotifier _stockNotifier;

        public UpdateProductHandler(IRepository<ProductEntity> productRepository, IRepository<MovementEntity> movementRepository, IUnitOfWork unitOfWork, IStockNotifier stockNotifier)
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
            _stockNotifier = stockNotifier;
        }

        public async Task Handle(ProductEntity entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = await _productRepository.GetByIdAsync(entity.Id);
                if (product == null) throw new KeyNotFoundException("Product not found");

                // Detect stock change
                if (product.Stock < entity.Stock)
                {
                    int quantity = entity.Stock - product.Stock;

                    MovementEntity newMovement = new()
                    {
                        Quantity = quantity,
                        Date = DateTime.Today,
                        Type = "ADD",
                        ProductId = entity.Id,
                    };

                    await _movementRepository.AddAsync(newMovement);
                }
                else if (product.Stock > entity.Stock)
                {
                    int quantity = product.Stock - entity.Stock;

                    MovementEntity newMovement = new()
                    {
                        Quantity = quantity,
                        Date = DateTime.Today,
                        Type = "SUBSTRACT",
                        ProductId = entity.Id,
                    };

                    await _movementRepository.AddAsync(newMovement);
                }

                await _productRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();

                if (entity.Stock < entity.MinimumStock)
                {
                    await _stockNotifier.NotifyLowStock(entity);
                }
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
