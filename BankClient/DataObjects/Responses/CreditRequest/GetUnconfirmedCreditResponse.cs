﻿using BLL.Classes;
using BLL.Models;

namespace DataObjects.Responses.CreditRequest
{
    public class GetUnconfirmedCreditResponse: ResponseBase
    {
        public CustomPagedList<ShortCreditRequest> CreditRequests { get; set; }
    }
}
