using Grpc.Core;
using MygRPC.Models;
using Myprotos;
using static Myprotos.GrpcCustomer;
namespace MygRPC.Services
{
    public class CustomerService : GrpcCustomerBase
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
        public override async Task<CustomerResponse> CreateCustomer(Myprotos.Customer request, ServerCallContext context)
        {
            var newCustomer = new Models.Customer()
            {
                Id = request.Id,
                Name = request.Name,
                address = request.Address
            };

            _db.Customers.Add(newCustomer);
            await _db.SaveChangesAsync();

            return new CustomerResponse
            {
                Success = true,
                Message = "Customer created successfully!"
            };
        }


        public override async Task<CustomerResponse> UpdateCustomer(Myprotos.Customer request, ServerCallContext context)
        {
            var exCus = await _db.Customers.FindAsync(request.Id);
            if (exCus == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Customer not found!!!"));
            }

            exCus.Name = request.Name;
            exCus.address = request.Address;

            _db.Customers.Update(exCus);
            await _db.SaveChangesAsync();

            return new CustomerResponse
            {
                Success = true,
                Message = "Customer updated successfully!"
            };
        }


        public override async Task<CustomerResponse> DeleteCustomer(IDrequest request, ServerCallContext context)
        {
            var existingCustomer = await _db.Customers.FindAsync(request.Id);
            if (existingCustomer == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Customer not found!!!"));
            }

            _db.Customers.Remove(existingCustomer);
            await _db.SaveChangesAsync();

            return new CustomerResponse
            {
                Success = true,
                Message = "Customer deleted successfully!"
            };
        }
    }
}