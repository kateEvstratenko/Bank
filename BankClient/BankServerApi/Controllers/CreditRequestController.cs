using System;
using System.Web.Http;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DataObjects.Requests.CreditRequest;
using DataObjects.Responses;
using DataObjects.Responses.CreditRequest;

namespace BankServerApi.Controllers
{
    [Authorize(Roles = "Operator")]
    public class CreditRequestController : ApiController
    {
        private readonly ICreditRequestService _iCreditRequestService;
        public CreditRequestController(ICreditRequestService iCreditRequestService)
        {
            _iCreditRequestService = iCreditRequestService;
        }

        [HttpPost]
        public ResponseBase Post(AddCreditRequest request)
        {
            try
            {
                var baseUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                _iCreditRequestService.Add(Mapper.Map<DomainCreditRequest>(request), request.TokenObj.UserId, 
                    request.CreditRequest.MilitaryId, request.CreditRequest.IncomeCertificate, baseUrl);
                return new ResponseBase();
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
        }

        public GetUnconfirmedCreditResponse GetUnconfirmed(AuthenticatedRequest request)
        {
            try
            {
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmed();
                return new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = unconfirmedCreditRequests
                };
            }   
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful<GetUnconfirmedCreditResponse>(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful<GetUnconfirmedCreditResponse>(ex);
            }
        }
    }
}
