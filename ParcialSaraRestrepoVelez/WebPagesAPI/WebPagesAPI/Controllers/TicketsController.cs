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
            return View();
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTicket(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7048/api/Ticket/Get/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

                if (ticket.IsUsed == false)
                {
                    return RedirectToAction("Edit", ticket.Id);
                }
                else
                {
                    return View(ticket);
                }
            }
            catch
            {
                return RedirectToAction("NotFound");
            }
        }

        public async Task<IActionResult> NotFound()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7048/api/Ticket/Get/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);
                return View(ticket);
            } catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            try
            {
                var url = String.Format("https://localhost:7048/api/Ticket/Edit/{0}", id);
                await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
                return RedirectToAction("Success", ticket);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        public async Task<IActionResult> Success(Ticket ticket)
        {
            return View(ticket);
        }
    }

}
