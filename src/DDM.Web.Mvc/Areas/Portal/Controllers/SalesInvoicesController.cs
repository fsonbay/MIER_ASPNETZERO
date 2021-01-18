using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.SalesInvoices;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.SalesInvoices;
using DDM.SalesInvoices.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_SalesInvoices)]
    public class SalesInvoicesController : DDMControllerBase
    {
        private readonly ISalesInvoicesAppService _salesInvoicesAppService;

        public SalesInvoicesController(ISalesInvoicesAppService salesInvoicesAppService)
        {
            _salesInvoicesAppService = salesInvoicesAppService;
        }

        public ActionResult Index()
        {
            var model = new SalesInvoicesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_SalesInvoices_Create, AppPermissions.Pages_SalesInvoices_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetSalesInvoiceForEditOutput getSalesInvoiceForEditOutput;

            if (id.HasValue)
            {
                getSalesInvoiceForEditOutput = await _salesInvoicesAppService.GetSalesInvoiceForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getSalesInvoiceForEditOutput = new GetSalesInvoiceForEditOutput
                {
                    SalesInvoice = new CreateOrEditSalesInvoiceDto()
                };
                getSalesInvoiceForEditOutput.SalesInvoice.Date = DateTime.Now;
            }

            var viewModel = new CreateOrEditSalesInvoiceModalViewModel()
            {
                SalesInvoice = getSalesInvoiceForEditOutput.SalesInvoice,
                SalesOrderNumber = getSalesInvoiceForEditOutput.SalesOrderNumber,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewSalesInvoiceModal(int id)
        {
            var getSalesInvoiceForViewDto = await _salesInvoicesAppService.GetSalesInvoiceForView(id);

            var model = new SalesInvoiceViewModel()
            {
                SalesInvoice = getSalesInvoiceForViewDto.SalesInvoice
                ,
                SalesOrderNumber = getSalesInvoiceForViewDto.SalesOrderNumber

            };

            return PartialView("_ViewSalesInvoiceModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_SalesInvoices_Create, AppPermissions.Pages_SalesInvoices_Edit)]
        public PartialViewResult SalesOrderLookupTableModal(int? id, string displayName)
        {
            var viewModel = new SalesInvoiceSalesOrderLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_SalesInvoiceSalesOrderLookupTableModal", viewModel);
        }

    }
}