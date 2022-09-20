using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using matcrm.data.Models.Request;
using matcrm.data.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using matcrm.service.Common;
using matcrm.service.Services;
using matcrm.data.Models.Tables;

namespace matcrm.api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ServiceArticleController : Controller
    {
        private IMapper _mapper;
        private readonly IServiceArticleService _serviceArticleService;
        private readonly IServiceArticleHourService _serviceArticleHourService;
        private readonly IServiceArticleCategoryService _serviceArticleCategoryService;
        private readonly ICurrencyService _currencyService;
        private int UserId = 0;
        private int TenantId = 0;
        public ServiceArticleController(IMapper mapper,
        IServiceArticleService serviceArticleService,
        IServiceArticleHourService serviceArticleHourService,
        IServiceArticleCategoryService serviceArticleCategoryService,
        ICurrencyService currencyService)
        {
            _mapper = mapper;
            _serviceArticleService = serviceArticleService;
            _serviceArticleHourService = serviceArticleHourService;
            _serviceArticleCategoryService = serviceArticleCategoryService;
            _currencyService = currencyService;
        }


        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<ServiceArticleAddResponse>> Add([FromBody] ServiceArticleAddRequest requestmodel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            var model = _mapper.Map<ServiceArticle>(requestmodel);
            if (model.Id == null || model.Id == 0)
            {
                model.CreatedBy = UserId;
            }
            model.TenantId = TenantId;
            var serviceArticleObj = await _serviceArticleService.CheckInsertOrUpdate(model);
            if (serviceArticleObj != null && requestmodel.UnitPrice != null)
            {
                ServiceArticleHour serviceArticleHourObj = new ServiceArticleHour();
                serviceArticleHourObj.UnitPrice = requestmodel.UnitPrice;
                serviceArticleHourObj.ServiceArticleId = serviceArticleObj.Id;
                var serviceArticleHourAddUpdateObj = await _serviceArticleHourService.CheckInsertOrUpdate(serviceArticleHourObj);
            }
            ServiceArticleAddResponse serviceArticleAddResponseObj = new ServiceArticleAddResponse();
            serviceArticleAddResponseObj = _mapper.Map<ServiceArticleAddResponse>(serviceArticleObj);
            if (requestmodel.CategoryId != null)
            {
                var categoryObj = _serviceArticleCategoryService.GetById(requestmodel.CategoryId.Value);
                serviceArticleAddResponseObj.CategoryName = categoryObj.Name;
            }
            if (requestmodel.CurrencyId != null)
            {
                var currencyObj = _currencyService.GetById(requestmodel.CurrencyId.Value);
                if (currencyObj != null)
                {
                    serviceArticleAddResponseObj.UnitPrice = currencyObj.Symbol + "" + requestmodel.UnitPrice;
                }
                else
                {
                    serviceArticleAddResponseObj.UnitPrice = Convert.ToString(requestmodel.UnitPrice);
                }
            }
            //serviceArticleAddResponseObj.UnitPrice = requestmodel.UnitPrice;
            return new OperationResult<ServiceArticleAddResponse>(true, System.Net.HttpStatusCode.OK, "Service article added successfully", serviceArticleAddResponseObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPut]
        public async Task<OperationResult<ServiceArticleAddResponse>> Update([FromBody] ServiceArticleAddRequest requestmodel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            var model = _mapper.Map<ServiceArticle>(requestmodel);

            model.TenantId = TenantId;
            var serviceArticleObj = await _serviceArticleService.CheckInsertOrUpdate(model);
            if (serviceArticleObj != null && requestmodel.UnitPrice != null)
            {
                ServiceArticleHour serviceArticleHourObj = new ServiceArticleHour();
                serviceArticleHourObj = _serviceArticleHourService.GetByServiceArticleId(serviceArticleObj.Id);
                serviceArticleHourObj.Id = serviceArticleHourObj.Id;
                serviceArticleHourObj.UnitPrice = requestmodel.UnitPrice;
                serviceArticleHourObj.ServiceArticleId = serviceArticleObj.Id;
                var serviceArticleHourAddUpdateObj = await _serviceArticleHourService.CheckInsertOrUpdate(serviceArticleHourObj);
            }
            ServiceArticleAddResponse serviceArticleAddResponseObj = new ServiceArticleAddResponse();
            serviceArticleAddResponseObj = _mapper.Map<ServiceArticleAddResponse>(serviceArticleObj);
            if (requestmodel.CategoryId != null)
            {
                var categoryObj = _serviceArticleCategoryService.GetById(requestmodel.CategoryId.Value);
                serviceArticleAddResponseObj.CategoryName = categoryObj.Name;
            }
            if (requestmodel.CurrencyId != null)
            {
                var currencyObj = _currencyService.GetById(requestmodel.CurrencyId.Value);
                if (currencyObj != null)
                {
                    serviceArticleAddResponseObj.UnitPrice = currencyObj.Symbol + "" + requestmodel.UnitPrice;
                }
                else
                {
                    serviceArticleAddResponseObj.UnitPrice = Convert.ToString(requestmodel.UnitPrice);
                }
            }
            return new OperationResult<ServiceArticleAddResponse>(true, System.Net.HttpStatusCode.OK, "Service article updated successfully", serviceArticleAddResponseObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet]
        public async Task<OperationResult<List<ServiceArticleListResponse>>> List()
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            var serviceArticleList = _serviceArticleService.GetByTenant(TenantId);

            var serviceArticleListResponseList = _mapper.Map<List<ServiceArticleListResponse>>(serviceArticleList);
            if (serviceArticleListResponseList != null && serviceArticleListResponseList.Count() > 0)
            {
                foreach (var item in serviceArticleListResponseList)
                {
                    if (item != null && item.CategoryId != null)
                    {
                        var categoryObj = serviceArticleList.Where(t => t.ServiceArticleCategory.Id == item.CategoryId).FirstOrDefault();
                        if (categoryObj != null)
                        {
                            item.CategoryName = categoryObj.ServiceArticleCategory.Name;
                        }
                        var serviceArticleHourObj = _serviceArticleHourService.GetByServiceArticleId(item.Id);
                        if (serviceArticleHourObj != null)
                        {
                            item.UnitPrice = Convert.ToString(serviceArticleHourObj.UnitPrice);
                        }
                        var currencyObj = serviceArticleList.Where(t => t.Currency.Id == item.CurrencyId).FirstOrDefault();
                        if (currencyObj != null)
                        {
                            item.UnitPrice = currencyObj.Currency.Symbol + "" + item.UnitPrice;
                        }
                    }
                }
            }

            return new OperationResult<List<ServiceArticleListResponse>>(true, System.Net.HttpStatusCode.OK, "", serviceArticleListResponseList);
        }

        [Authorize(Roles = "Admin,TenantManager,TenantAdmin, TenantUser, ExternalUser")]
        [HttpGet("{Id}")]
        public async Task<OperationResult<ServiceArticleDetailResponse>> Detail(int Id)
        {
            ServiceArticle serviceArticleObj = new ServiceArticle();

            ServiceArticleDetailResponse serviceArticleDetailResponseObj = new ServiceArticleDetailResponse();

            serviceArticleObj = _serviceArticleService.GetById(Id);
            var serviceArticleHourObj = _serviceArticleHourService.GetByServiceArticleId(Id);
            serviceArticleDetailResponseObj = _mapper.Map<ServiceArticleDetailResponse>(serviceArticleObj);
           
            if (serviceArticleHourObj != null && serviceArticleHourObj.UnitPrice != null)
            {
                serviceArticleDetailResponseObj.UnitPrice = serviceArticleHourObj.UnitPrice.Value;
            }

            return new OperationResult<ServiceArticleDetailResponse>(true, System.Net.HttpStatusCode.OK, "", serviceArticleDetailResponseObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{Id}")]
        public async Task<OperationResult> Remove(long Id)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            if (Id != null && Id > 0)
            {
                var serviceArticleObj = await _serviceArticleService.DeleteServiceArticle(Id);

                return new OperationResult(true, System.Net.HttpStatusCode.OK, "", Id);
            }
            else
            {
                return new OperationResult(false, System.Net.HttpStatusCode.OK, "Please provide id", Id);
            }
        }

    }
}