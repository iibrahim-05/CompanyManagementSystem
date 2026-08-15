using Microsoft.AspNetCore.Mvc;
using CompanyManagementSystem.Models;

namespace CompanyManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private static readonly List<Dept> _departments = new()
        {
            new Dept { Id = 1, Name = "Human Resources", Description = "Handles hiring and employee relations." },
            new Dept { Id = 2, Name = "Sales", Description = "Handles customer relationships and sales." },
            new Dept { Id = 3, Name = "Cybersecurity", Description = "Handles cybersecurity measures." },
            new Dept { Id = 4, Name = "Design", Description = "Handles design-related tasks." },
            new Dept { Id = 5, Name = "Marketing", Description = "Handles marketing and advertising." },
            new Dept { Id = 6, Name = "Finance", Description = "Handles financial planning and analysis." },
            new Dept { Id = 7, Name = "Development", Description = "Handles development-related tasks." },
        };

        public IActionResult Index()
        {
            return View(_departments);
        }

        public IActionResult Create()
        {
            return View(new Dept());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dept model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var nextId = _departments.Any() ? _departments.Max(d => d.Id) + 1 : 1;
            model.Id = nextId;
            _departments.Add(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var dept = _departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
                return NotFound();
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dept model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = _departments.FirstOrDefault(d => d.Id == model.Id);
            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Description = model.Description;

            return RedirectToAction(nameof(Index));
        }
    }
}
