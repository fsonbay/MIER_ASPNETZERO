using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.CustomerCategories;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.CustomerCategories;
using DDM.CustomerCategories.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_CustomerCategories)]
    public class CustomerCategoriesController : DDMControllerBase
    {
        private readonly ICustomerCategoriesAppService _customerCategoriesAppService;

        public CustomerCategoriesController(ICustomerCategoriesAppService customerCategoriesAppService)
        {
            _customerCategoriesAppService = customerCategoriesAppService;
        }

        public ActionResult Index()
        {
            var model = new CustomerCategoriesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CustomerCategories_Create, AppPermissions.Pages_CustomerCategories_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCustomerCategoryForEditOutput getCustomerCategoryForEditOutput;

            if (id.HasValue)
            {
                getCustomerCategoryForEditOutput = await _customerCategoriesAppService.GetCustomerCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getCustomerCategoryForEditOutput = new GetCustomerCategoryForEditOutput
                {
                    CustomerCategory = new CreateOrEditCustomerCategoryDto()
                };
            }

            var viewModel = new CreateOrEditCustomerCategoryModalViewModel()
            {
                CustomerCategory = getCustomerCategoryForEditOutput.CustomerCategory,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCustomerCategoryModal(int id)
        {
            var getCustomerCategoryForViewDto = await _customerCategoriesAppService.GetCustomerCategoryForView(id);

            var model = new CustomerCategoryViewModel()
            {
                CustomerCategory = getCustomerCategoryForViewDto.CustomerCategory
            };

            return PartialView("_ViewCustomerCategoryModal", model);
        }

    }
}