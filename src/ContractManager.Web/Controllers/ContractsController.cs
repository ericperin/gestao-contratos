using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ContractManager.Domain.Interfaces.Repositories;
using System.Security.Claims;
using MediatR;
using AutoMapper;
using ContractManager.Domain.Commands.Contract;

namespace ContractManager.Web.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly IContractRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContractsController(IContractRepository repository,
                              IMediator mediator,
                              IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.FilterAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormContractCommand request)
        {
            if (ModelState.IsValid)
            {
                request.CreatedBy = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _mediator.Send(request);

                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Edit(contract);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contract = await _repository.GetById(id);
            await _repository.Delete(contract);

            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(Guid id)
        {
            return _repository.Filter(e => e.Id == id).Any();
        }
    }
}
