using Grpc.Core;
using MygRPC.Models;
using Myprotos;
using static Myprotos.GRrpcCustomer;
namespace MygRPC.Services
{
    public class CustomerService : GRrpcCustomerBase
    {
        public readonly AppDBContext _db;

        public CustomerService(AppDBContext db)
        {
            this._db = db;
        }

        public override Task<CustomerList> GetAll(Empty request, ServerCallContext context)
        {
            CustomerList respond = new CustomerList();
            var cusList = from obj in this._db.Customers
                          select new Myprotos.Customer()
                          {
                              Id = obj.Id,
                              Name = obj.Name,
                              Address = obj.address
                          };
            respond.Customers.AddRange(cusList);
            return Task.FromResult(respond);
        }

        public override Task<Myprotos.Customer> GetCustomer(IDrequest request, ServerCallContext context)
        {
            var obj = this._db.Customers.Find(request.Id);
            if (obj != null)
            {
                Myprotos.Customer cus = new Myprotos.Customer()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Address = obj.address
                };
                return Task.FromResult(cus);
            }
            throw new RpcException(new Status(StatusCode.NotFound, "Customer not found!!!"));

        }
    }
}