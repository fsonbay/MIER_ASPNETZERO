using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Customers;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.Customers;
using DDM.Customers.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Customers)]
    public class CustomersController : DDMControllerBase
    {
        private readonly ICustomersAppService _customersAppService;

        public CustomersController(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;
        }

        public ActionResult Index()
        {
            var model = new CustomersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Customers_Create, AppPermissions.Pages_Customers_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCustomerForEditOutput output;
            output = await _customersAppService.GetCustomerForEdit(new NullableIdDto { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditCustomerModalViewModel>(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCustomerModal(int id)
        {
            var getCustomerForViewDto = await _customersAppService.GetCustomerForView(id);

            var model = new CustomerViewModel()
            {
                Customer = getCustomerForViewDto.Customer
                ,
                CustomerCategoryName = getCustomerForViewDto.CustomerCategoryName

            };

            return PartialView("_ViewCustomerModal", model);
        }

    }
}