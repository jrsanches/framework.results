using Framework.Results.Models;
using Framework.Results.Samples.Models;
using System.Threading.Tasks;

namespace Framework.Results.Samples
{
    public class CustomerServiceGetFailUpdateSuccess : CustomerService
    {
        protected override async Task<Result<Customer>> GetCustomer()
        {
            return await Task.Run(() => Result<Customer>.Fail());
        }

        protected override async Task<Result> UpdateCustomer(Customer customer)
        {
            return await Task.Run(() => Result.Success());
        }
    }
}
