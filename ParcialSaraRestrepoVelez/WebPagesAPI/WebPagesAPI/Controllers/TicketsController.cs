using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialSaraRestrepoVelez.DAL.Entities;

namespace WebPagesAPI.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public TicketsController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7048/api/Ticket/Get";
            List<Ticket> Tickets = JsonConvert.DeserializeObject<List<Ticket>>(await _httpClient.CreateClient().GetStringAsync(url));
            return View(Tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            try
            {
                var url = "https://localhost:7048/api/Ticket/Create";
                await _httpClient.CreateClient().PostAsJsonAsync(url, ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicket()
        {
            return View();        }
        
        [HttpPost]
        public async Task<IActionResult> GetTicket(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7048/api/Tickets/Get/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                var ticket = JsonConvert.DeserializeObject<Ticket>(json);
                return RedirectToAction("Edit", ticket);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var url = String.Format("https://localhost:7048/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            var url = String.Format("https://localhost:7048/api/Tickets/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
            return RedirectToAction("Index");
        }
    }

}
