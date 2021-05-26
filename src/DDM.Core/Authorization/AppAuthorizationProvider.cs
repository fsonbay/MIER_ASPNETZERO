using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace DDM.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var salesInvoiceAdditionalCosts = pages.CreateChildPermission(AppPermissions.Pages_SalesInvoiceAdditionalCosts, L("SalesInvoiceAdditionalCosts"));
            salesInvoiceAdditionalCosts.CreateChildPermission(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Create, L("CreateNewSalesInvoiceAdditionalCost"));
            salesInvoiceAdditionalCosts.CreateChildPermission(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Edit, L("EditSalesInvoiceAdditionalCost"));
            salesInvoiceAdditionalCosts.CreateChildPermission(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Delete, L("DeleteSalesInvoiceAdditionalCost"));

            var salesInvoicePayments = pages.CreateChildPermission(AppPermissions.Pages_SalesInvoicePayments, L("SalesInvoicePayments"));
            salesInvoicePayments.CreateChildPermission(AppPermissions.Pages_SalesInvoicePayments_Create, L("CreateNewSalesInvoicePayment"));
            salesInvoicePayments.CreateChildPermission(AppPermissions.Pages_SalesInvoicePayments_Edit, L("EditSalesInvoicePayment"));
            salesInvoicePayments.CreateChildPermission(AppPermissions.Pages_SalesInvoicePayments_Delete, L("DeleteSalesInvoicePayment"));

            var paymentMethods = pages.CreateChildPermission(AppPermissions.Pages_PaymentMethods, L("PaymentMethods"));
            paymentMethods.CreateChildPermission(AppPermissions.Pages_PaymentMethods_Create, L("CreateNewPaymentMethod"));
            paymentMethods.CreateChildPermission(AppPermissions.Pages_PaymentMethods_Edit, L("EditPaymentMethod"));
            paymentMethods.CreateChildPermission(AppPermissions.Pages_PaymentMethods_Delete, L("DeletePaymentMethod"));

            var salesInvoices = pages.CreateChildPermission(AppPermissions.Pages_SalesInvoices, L("SalesInvoices"));
            salesInvoices.CreateChildPermission(AppPermissions.Pages_SalesInvoices_Create, L("CreateNewSalesInvoice"));
            salesInvoices.CreateChildPermission(AppPermissions.Pages_SalesInvoices_Edit, L("EditSalesInvoice"));
            salesInvoices.CreateChildPermission(AppPermissions.Pages_SalesInvoices_Delete, L("DeleteSalesInvoice"));

            var productionStatuses = pages.CreateChildPermission(AppPermissions.Pages_ProductionStatuses, L("ProductionStatuses"));
            productionStatuses.CreateChildPermission(AppPermissions.Pages_ProductionStatuses_Create, L("CreateNewProductionStatus"));
            productionStatuses.CreateChildPermission(AppPermissions.Pages_ProductionStatuses_Edit, L("EditProductionStatus"));
            productionStatuses.CreateChildPermission(AppPermissions.Pages_ProductionStatuses_Delete, L("DeleteProductionStatus"));

            var salesOrderLines = pages.CreateChildPermission(AppPermissions.Pages_SalesOrderLines, L("SalesOrderLines"));
            salesOrderLines.CreateChildPermission(AppPermissions.Pages_SalesOrderLines_Create, L("CreateNewSalesOrderLine"));
            salesOrderLines.CreateChildPermission(AppPermissions.Pages_SalesOrderLines_Edit, L("EditSalesOrderLine"));
            salesOrderLines.CreateChildPermission(AppPermissions.Pages_SalesOrderLines_Delete, L("DeleteSalesOrderLine"));

            var machines = pages.CreateChildPermission(AppPermissions.Pages_Machines, L("Machines"));
            machines.CreateChildPermission(AppPermissions.Pages_Machines_Create, L("CreateNewMachine"));
            machines.CreateChildPermission(AppPermissions.Pages_Machines_Edit, L("EditMachine"));
            machines.CreateChildPermission(AppPermissions.Pages_Machines_Delete, L("DeleteMachine"));

            var materials = pages.CreateChildPermission(AppPermissions.Pages_Materials, L("Materials"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Create, L("CreateNewMaterial"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Edit, L("EditMaterial"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Delete, L("DeleteMaterial"));

            var salesOrders = pages.CreateChildPermission(AppPermissions.Pages_SalesOrders, L("SalesOrders"));
            salesOrders.CreateChildPermission(AppPermissions.Pages_SalesOrders_Create, L("CreateNewSalesOrder"));
            salesOrders.CreateChildPermission(AppPermissions.Pages_SalesOrders_Edit, L("EditSalesOrder"));
            salesOrders.CreateChildPermission(AppPermissions.Pages_SalesOrders_Delete, L("DeleteSalesOrder"));

            var vendorCategories = pages.CreateChildPermission(AppPermissions.Pages_VendorCategories, L("VendorCategories"));
            vendorCategories.CreateChildPermission(AppPermissions.Pages_VendorCategories_Create, L("CreateNewVendorCategory"));
            vendorCategories.CreateChildPermission(AppPermissions.Pages_VendorCategories_Edit, L("EditVendorCategory"));
            vendorCategories.CreateChildPermission(AppPermissions.Pages_VendorCategories_Delete, L("DeleteVendorCategory"));

            var vendors = pages.CreateChildPermission(AppPermissions.Pages_Vendors, L("Vendors"));
            vendors.CreateChildPermission(AppPermissions.Pages_Vendors_Create, L("CreateNewVendor"));
            vendors.CreateChildPermission(AppPermissions.Pages_Vendors_Edit, L("EditVendor"));
            vendors.CreateChildPermission(AppPermissions.Pages_Vendors_Delete, L("DeleteVendor"));

            var customers = pages.CreateChildPermission(AppPermissions.Pages_Customers, L("Customers"));
            customers.CreateChildPermission(AppPermissions.Pages_Customers_Create, L("CreateNewCustomer"));
            customers.CreateChildPermission(AppPermissions.Pages_Customers_Edit, L("EditCustomer"));
            customers.CreateChildPermission(AppPermissions.Pages_Customers_Delete, L("DeleteCustomer"));

            var customerCategories = pages.CreateChildPermission(AppPermissions.Pages_CustomerCategories, L("CustomerCategories"));
            customerCategories.CreateChildPermission(AppPermissions.Pages_CustomerCategories_Create, L("CreateNewCustomerCategory"));
            customerCategories.CreateChildPermission(AppPermissions.Pages_CustomerCategories_Edit, L("EditCustomerCategory"));
            customerCategories.CreateChildPermission(AppPermissions.Pages_CustomerCategories_Delete, L("DeleteCustomerCategory"));

            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicParameters = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters, L("DynamicParameters"));
            dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Create, L("CreatingDynamicParameters"));
            dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Edit, L("EditingDynamicParameters"));
            dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Delete, L("DeletingDynamicParameters"));

            var dynamicParameterValues = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue, L("DynamicParameterValue"));
            dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Create, L("CreatingDynamicParameterValue"));
            dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Edit, L("EditingDynamicParameterValue"));
            dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Delete, L("DeletingDynamicParameterValue"));

            var entityDynamicParameters = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters, L("EntityDynamicParameters"));
            entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Create, L("CreatingEntityDynamicParameters"));
            entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Edit, L("EditingEntityDynamicParameters"));
            entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Delete, L("DeletingEntityDynamicParameters"));

            var entityDynamicParameterValues = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue, L("EntityDynamicParameterValue"));
            entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Create, L("CreatingEntityDynamicParameterValue"));
            entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Edit, L("EditingEntityDynamicParameterValue"));
            entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Delete, L("DeletingEntityDynamicParameterValue"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DDMConsts.LocalizationSourceName);
        }
    }
}