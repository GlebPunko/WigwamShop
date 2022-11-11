using AutoMapper;
using Basket.Application.CustomException;
using Basket.Application.Exception;
using Basket.Application.Interfaces;
using Basket.Application.Models;
using Basket.Application.Validator;
using Basket.Domain.Entities;
using Basket.Infastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace Basket.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly CreateOrderValidator _createValidator;
        private readonly UpdateOrderValidator _updateValidator;

        public OrderService(IBasketRepository repository, IMapper mapper,
            UpdateOrderValidator updateValidator, CreateOrderValidator createValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<ViewOrderModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<ViewOrderModel>>(orders);
        }

        public async Task<ViewOrderModel> GetAsync(int id, CancellationToken cancellationToken)
        {
            if (id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var order = await _repository.GetAsync(id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(nameof(order));
            }

            return _mapper.Map<ViewOrderModel>(order);
        }

        public async Task<ViewOrderModel> CreateAsync(CreateOrderModel model, CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAsync(model, options => options.ThrowOnFailures(), cancellationToken);

            var map = _mapper.Map<Order>(model);

            var createdOrder = await _repository.CreateAsync(map, cancellationToken);

            if (createdOrder is null)
            {
                throw new ArgumentNullException(nameof(createdOrder));
            }

            await _repository.SaveAsync(cancellationToken);

            return _mapper.Map<ViewOrderModel>(createdOrder);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var order = await _repository.GetAsync(id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(nameof(order));
            }

            await _repository.DeleteAsync(order, cancellationToken);

            await _repository.SaveAsync(cancellationToken);
        }

        public async Task<ViewOrderModel> UpdateAsync(UpdateOrderModel model, CancellationToken cancellationToken)
        {
            if (model.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            await _updateValidator.ValidateAsync(model, options => options.ThrowOnFailures(), cancellationToken);

            var map = _mapper.Map<Order>(model);

            var updatedOrder = await _repository.UpdateAsync(map, cancellationToken);

            if (updatedOrder is null)
            {
                throw new ArgumentNullException(nameof(updatedOrder));
            }

            await _repository.SaveAsync(cancellationToken);

            return _mapper.Map<ViewOrderModel>(updatedOrder);
        }
    }
}
