﻿using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICustomerCreditRepository : IRepository<CustomerCredit>
    {
        CustomerCredit GetByContractNumber(string contractNumber);
    }
}