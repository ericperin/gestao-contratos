using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces.Repositories;
using ContractManager.Domain.Notifications;
using MediatR;

namespace ContractManager.Domain.Commands.Contract
{
    public class ContractHandler : IRequestHandler<CreateContractCommand, Result>,
                           IRequestHandler<UpdateContractCommand, Result>,
                           IRequestHandler<DeleteContractCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly IContractRepository _ContractRepository;

        public ContractHandler(IMediator mediator,
                       IContractRepository ContractRepository)
        {
            _mediator = mediator;
            _ContractRepository = ContractRepository;
        }

        private IEnumerable<string> GetErrors(ContractCommand request) =>
            request.ValidationResult.Errors.Select(err => err.ErrorMessage);

        public async Task<Result> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            if (request.IsValid())
                if (await _ContractRepository.GetById(request.Id) == null)
                    await _ContractRepository.Create(
                        new Entities.Contract(request.ClientName, request.Type, request.QuantityTraded, request.NegotiatedValue, request.StartedAt, request.Duration, request.File, request.CreatedBy)
                    );
                else
                {
                    var message = "The Contract ID already exists.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    result.AddError(message);
                }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }
            return result;
        }

        public async Task<Result> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            if (request.IsValid())
            {
                var contract = await _ContractRepository.GetById(request.Id);
                if (contract != null)
                {
                    contract.Update(request.ClientName, request.Type, request.QuantityTraded, request.NegotiatedValue, request.StartedAt, request.Duration, request.File);
                    await _ContractRepository.Edit(contract);
                }
                else
                {
                    var message = "The contract cannot be found.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    result.AddError(message);
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }
            return result;
        }


        public async Task<Result> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            if (request.IsValid())
            {
                var Contract = await _ContractRepository.GetById(request.Id);
                if (Contract != null)
                    await _ContractRepository.Delete(Contract);
                else
                {
                    var message = "The Contract cannot be found.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    result.AddError(message);
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }

            return result;
        }
    }
}
