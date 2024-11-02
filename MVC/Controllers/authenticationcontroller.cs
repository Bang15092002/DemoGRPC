using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Myprotos;
using static Myprotos.GrpcCustomer;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly GrpcCustomer.GrpcCustomerClient _client;

        public AuthenticationController(GrpcCustomer.GrpcCustomerClient client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Authentication(Myprotos.Customer customer)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5024");
            var client = new GrpcCustomerClient(channel);
            Myprotos.Customer cus = _client.GetCustomer(new Myprotos.IDrequest() { Id = customer.Id });
            if (cus == null)
            {
                return View();
            }
            ViewBag.Customer = cus;
            return View();
        }
    }
}
