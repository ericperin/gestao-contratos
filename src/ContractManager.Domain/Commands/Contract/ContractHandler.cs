using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly Result _result;

        public ContractHandler(IMediator mediator, IContractRepository ContractRepository)
        {
            _mediator = mediator;
            _ContractRepository = ContractRepository;
            _result = new Result();
        }

        private IEnumerable<string> GetErrors(ContractCommand request) => request.ValidationResult.Errors.Select(err => err.ErrorMessage);

        public async Task<Result> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var warnings = CheckBalance(request);
                if (!warnings.Any())
                    await _ContractRepository.Create(
                        new Entities.Contract(
                            request.ClientName,
                            request.Type,
                            request.QuantityTraded,
                            request.NegotiatedValue,
                            request.StartedAt,
                            request.Duration,
                            request.File,
                            request.CreatedBy)
                    );
                else
                {// O contrato de venda não pode ser adicionado pois ultrapassou o saldo em alguns meses
                    var message = "Os seguinte meses estão com saldo insuficiente:";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    _result.AddError(message);
                    foreach (var month in warnings)
                    {
                        _result.AddError(month.ToString());
                    }
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                _result.AddErrors(GetErrors(request));
            }
            return _result;
        }

        public async Task<Result> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var contract = await _ContractRepository.GetById(request.Id);
                if (contract != null)
                {
                    contract.Update(request.ClientName,
                        request.Type,
                        request.QuantityTraded,
                        request.NegotiatedValue,
                        request.StartedAt,
                        request.Duration,
                        request.File);

                    await _ContractRepository.Edit(contract);
                }
                else
                {
                    var message = "O contrato não foi encontrado.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    _result.AddError(message);
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                _result.AddErrors(GetErrors(request));
            }
            return _result;
        }

        public async Task<Result> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                await _ContractRepository.Delete(request.Id);
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                _result.AddErrors(GetErrors(request));
            }

            return _result;
        }

        /// <summary>
        /// Um contrato de venda só pode ser adicionado se a quantidade total disponível para venda 
        /// (contratos de compra - contratos de venda) para cada um dos meses for igual ou superior 
        /// em cada um dos meses do novo contrato.
        /// </summary>
        private IDictionary<string, string> CheckBalance(CreateContractCommand contract)
        {
            var result = new Dictionary<string, string>();
            if (contract.Type == Enums.ETypeOfContract.Compra) return result;

            for (DateTime month = contract.StartedAt; contract.FinishedAt.CompareTo(month) > 0; month = month.AddMonths(1))
            {
                var contracts = _ContractRepository.Filter(x => x.StartedAt <= month && month <= x.FinishedAt);
                var contratosCompra = contracts.Where(x => x.Type == Enums.ETypeOfContract.Compra).Sum(x => x.QuantityTraded);
                var contratosVenda = contracts.Where(x => x.Type == Enums.ETypeOfContract.Venda).Sum(x => x.QuantityTraded);

                var totalDisponivelParaVenda = contratosCompra - contratosVenda;
                if (totalDisponivelParaVenda < contract.QuantityTraded)
                {// Se a quantidade total disponível para venda para cada um dos meses for menor em cada um dos meses
                    result.Add(month.ToString("MM/yyyy"), $"Saldo: {totalDisponivelParaVenda.ToString("C2")}");
                }
            }

            return result;
        }
    }
}
