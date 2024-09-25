using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Myprotos;
using static Myprotos.GrpcCustomer;
namespace MVC.Controllers

{
    public class CustomerController : Controller
    {
        private readonly GrpcCustomer.GrpcCustomerClient _client;

        public CustomerController(GrpcCustomer.GrpcCustomerClient client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
           
           Myprotos.CustomerList data = _client.GetAll(new Myprotos.Empty());

           ViewBag.Data = data;
           return View();
        }

        public IActionResult GetCustomer(string id)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:504");
            var client = new GrpcCustomerClient(channel);
            Myprotos.Customer cus = _client.GetCustomer(new Myprotos.IDrequest() { Id = id });
            ViewBag.Customer = cus;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Myprotos.Customer customer)
        {
            var response = _client.CreateCustomer(customer);

            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(customer);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Myprotos.Customer customer)
        {
            var response = _client.UpdateCustomer(customer);

            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(customer);
            }
        }
       
        public IActionResult Update(string id)
        {
            Myprotos.Customer customer = _client.GetCustomer(new Myprotos.IDrequest() { Id = id });
            return View(customer);
        }

        public IActionResult Delete(string id)
        {
            var response = _client.DeleteCustomer(new Myprotos.IDrequest() { Id = id });

            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View();
            }
        }
    }
}