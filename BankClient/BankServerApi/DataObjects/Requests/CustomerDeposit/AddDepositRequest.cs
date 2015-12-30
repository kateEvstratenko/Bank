using System;
using Core.Enums;
using DAL.Entities;

namespace BankServerApi.DataObjects.Requests.CustomerDeposit
{
    public class AddDepositRequest
    {
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DepositId { get; set; }
        public Customer Customer { get; set; }
    }
}