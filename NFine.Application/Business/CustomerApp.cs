using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain.Entity.Business;
using NFine.Domain.IRepository.Business;
using NFine.Repository.Business;

namespace NFine.Application.Business
{
    public class CustomerApp
    {
        private ICustomerRepository customerRepository = new CustomerRepository();
        private ICustomerAccountRepository customerAccountRepository = new CustomerAccountRepository();

        public List<CustomerEntity> GetTypeCustomers(string typeId)
        {
            List<CustomerEntity> customers = customerRepository.IQueryable().ToList().
                Where(t => t.F_CustomerTypeId == typeId).ToList();
            return customers;
        }

        public List<CustomerEntity> GetCustomers()
        {
            List<CustomerEntity> customers = customerRepository.IQueryable().ToList();
            return customers;
        }

        public List<CustomerAccountEntity> GetCustomerAccounts(string customerId)
        {
            var expQuery = ExtLinq.True<CustomerAccountEntity>();
            expQuery = expQuery.And(t => t.F_CustomerId == customerId);
            List<CustomerAccountEntity> accounts = customerAccountRepository.IQueryable(expQuery).ToList();
            return accounts;
        }

        public CustomerEntity GetCustomerForm(string keyValue)
        {
            return customerRepository.FindEntity(keyValue);
        }

        public CustomerAccountEntity GetCustomerAccountForm(string keyValue)
        {
            return customerAccountRepository.FindEntity(keyValue);
        }

        public void DeleteCustomerForm(string keyValue)
        {
            customerRepository.Delete(t => t.F_Id == keyValue);
        }

        public void DeleteCustomerAccountForm(string keyValue)
        {
            customerAccountRepository.Delete(t => t.F_Id == keyValue);
        }

        public void SubmitCustomerForm(CustomerEntity customerEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                customerEntity.Modify(keyValue);
                customerRepository.Update(customerEntity);
            }
            else
            {
                customerEntity.Create();
                customerRepository.Insert(customerEntity);
            }
        }

        public void SubmitCustomerAccountForm(CustomerAccountEntity accountEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                accountEntity.Modify(keyValue);
                customerAccountRepository.Update(accountEntity);
            }
            else
            {
                accountEntity.Create();
                customerAccountRepository.Insert(accountEntity);
            }
        }
    }
}
