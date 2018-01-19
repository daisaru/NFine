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
    public class ContractApp
    {
        private IContractRepository contractRepository = new ContractRepository();

        public List<ContractEntity> GetContracts()
        {
            List<ContractEntity> contracts = contractRepository.IQueryable().ToList();
            return contracts;
        }

        public ContractEntity GetContractForm(string keyValue)
        {
            return contractRepository.FindEntity(keyValue);
        }

        public void DeleteContractForm(string keyValue)
        {
            contractRepository.Delete(t => t.F_Id == keyValue);
        }

        public void SubmitContractForm(ContractEntity contractEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                contractEntity.Modify(keyValue);
                contractRepository.Update(contractEntity);
            }
            else
            {
                contractEntity.Create();
                contractRepository.Insert(contractEntity);
            }
        }
    }
}
