﻿using System;
using System.Web.Http;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CustomerDeposit;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CustomerDeposit")]
    [CheckToken(Order = 0)]
    public class CustomerDepositController : ApiController
    {
        private readonly ICustomerDepositService _customerDepositService;

        public CustomerDepositController(ICustomerDepositService customerDepositService)
        {
            _customerDepositService = customerDepositService;
        }

        public IHttpActionResult Get(int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(pageNumber,
                        pageSize)));
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetByCustomerId(int customerId, int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(customerId,
                        pageNumber, pageSize)));
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Add")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.Operator })]
        public IHttpActionResult Add(AddDepositRequest request)
        {
            try
            {
                _customerDepositService.Add(Mapper.Map<DomainCustomerDeposit>(request));
                return Ok();
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _customerDepositService.Delete(id);
                return Ok();
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
