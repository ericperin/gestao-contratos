using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ContractManager.Data.Interfaces;

namespace ContractManager.Web.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly IContractRepository _repository;

        public ContractsController(IContractRepository repository)
        {
            _repository = repository;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            return View(await _repository.FilterAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientName,Type,QuantityTraded,NegotiatedValue,StartedAt,Duration,File,CreatedBy,Id,DeletedAt,CreatedAt")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                contract.Id = Guid.NewGuid();
                await _repository.Create(contract);
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var contract = await _repository.GetById(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientName,Type,QuantityTraded,NegotiatedValue,StartedAt,Duration,File,CreatedBy,Id,DeletedAt,CreatedAt")] Contract contract)
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

        // GET: Contracts/Delete/5
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

        // POST: Contracts/Delete/5
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
