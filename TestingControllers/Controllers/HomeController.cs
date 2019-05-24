using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestingControllers.Models;
using TestingControllers.Repository;
using TestingControllers.ViewModels;

namespace TestingControllers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public HomeController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sessionList = await _sessionRepository.ListAsync();
            var model = sessionList.Select(session => new SessionViewModel()
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SessionViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sessionRepository.AddAsync(new SessionModel()
            {
                DateCreated = DateTime.Now,
                Name = viewModel.Name
            });
            return RedirectToAction(actionName: nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
