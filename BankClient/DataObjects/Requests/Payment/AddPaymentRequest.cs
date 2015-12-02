using System;
using BLL.Models;
using Core.Enums;

namespace DataObjects.Requests.CreditRequest
{
    public class AddPaymentRequest
    {
        public string ContractNumber { get; set; }
        public double Sum { get; set; }
    }
}