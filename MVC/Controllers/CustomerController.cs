using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using static Myprotos.GRrpcCustomer;
namespace MVC.Controllers

{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
           using var channel = GrpcChannel.ForAddress("http://localhost:5024");
           var client = new GRrpcCustomerClient(channel); 
           Myprotos.CustomerList data = client.GetAll(new Myprotos.Empty());

           ViewBag.Data = data;
           return View();
        }

        public IActionResult GetCustomer(string id)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:504");
            var client = new GRrpcCustomerClient(channel);
            Myprotos.Customer cus = client.GetCustomer(new Myprotos.IDrequest() { Id = id });
            ViewBag.Customer = cus;
            return View();
        }
    }


}