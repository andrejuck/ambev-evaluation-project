using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{

    /// <summary>
    /// Handler for processing DeleteUserCommand requests
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        public DeleteSaleHandler(
            ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the DeleteUserCommand request
        /// </summary>
        /// <param name="request">The DeleteUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            sale.PrepareForDeletion();
            var success = await _saleRepository.DeleteAsync(sale, cancellationToken);

            if (success)
            {
                foreach (var domainEvent in sale.DomainEvents)
                {
                    await _mediator.Send(domainEvent);
                }
            }

            return new DeleteSaleResponse { Success = true };
        }
    }
}
