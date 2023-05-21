using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialSaraRestrepoVelez.DAL.Entities;

namespace WebPagesAPI.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7048/api/Ticket/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Ticket> Tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            return View(Tickets);
        }
    }
}
