using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ContractManager.Domain.Interfaces.Repositories;
using System.Security.Claims;
using MediatR;
using AutoMapper;
using ContractManager.Domain.Commands.Contract;
using ContractManager.Domain;
using System.IO;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Create(CreateContractCommand request, IFormFile pdf)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await pdf.CopyToAsync(memoryStream);
                    request.File = memoryStream.ToArray();
                }
                request.CreatedBy = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var result = await _mediator.Send(request);

                return Result(request, result);
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null) return NotFound();

            var command = _mapper.Map<UpdateContractCommand>(contract);
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateContractCommand request)
        {
            if (id != request.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(request);

                return Result(request, result);
            }
            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var request = new DeleteContractCommand(id);
            var result = await _mediator.Send(request);

            if (result.HasErrors) return BadRequest(string.Join(", ", result.Errors));

            return Ok();
        }

        public async Task<ActionResult> PDF(Guid id)
        {
            var contract = await _repository.GetById(id);

            return new FileContentResult(contract.File, "application/pdf");
        }

        private IActionResult Result(ContractCommand request, Result result)
        {
            if (result.HasErrors)
            {
                result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err));
                return View(request);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
