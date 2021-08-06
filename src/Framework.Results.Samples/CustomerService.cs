using Framework.Results.Models;
using Framework.Results.Samples.Models;
using System.Threading.Tasks;

namespace Framework.Results.Samples
{
    public abstract class CustomerService
    {
        protected abstract Task<Result<Customer>> GetCustomer();

        protected abstract Task<Result> UpdateCustomer(Customer customer);

        public async Task<bool> ProcessCustomerCheckingResult()
        {
            var customerResult = await GetCustomer();

            if (customerResult.Succeeded)
            {
                var updateResult = await UpdateCustomer(customerResult.Data);

                if (updateResult.Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<bool> ProcessCustomerThrowingException()
        {
            var customer = (await GetCustomer())
                .GetDataThrowsException();

            (await UpdateCustomer(customer))
                .GetSuccessOrThrowsException();

            return true;
        }

        public async Task<bool> ProcessCustomerReturningResultStatus()
        {
            var customerResult = await GetCustomer();

            if (customerResult.Failed)
                return customerResult.Succeeded;

            return (await UpdateCustomer(customerResult.Data)).Succeeded;
        }

        public async Task<bool> ProcessCustomerReturningCallbackResultStatus()
        {
            return (await (await GetCustomer())
                .OnSuccessReturn(async (e) => await UpdateCustomer(e.Data))).Succeeded;
        }

        public async Task<Result> ProcessCustomerReturningResult()
        {
            var customerResult = await GetCustomer();

            if (customerResult.Failed)
                return customerResult;

            return await UpdateCustomer(customerResult.Data);
        }

        public async Task<Result> ProcessCustomerReturningCallbackResult()
        {
            return await (await GetCustomer())
                .OnSuccessReturn(async (e) => await UpdateCustomer(e.Data));
        }
    }
}
