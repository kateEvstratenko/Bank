﻿using BankServerApi.Models;
using BLL.Classes;
using Core;

namespace BankServerApi.DataObjects.Responses.CreditRequest
{
    public class GetСonfirmedCreditResponse : ResponseBase
    {
        public CustomPagedList<ShortCreditRequest> CreditRequests { get; set; }
    }
}