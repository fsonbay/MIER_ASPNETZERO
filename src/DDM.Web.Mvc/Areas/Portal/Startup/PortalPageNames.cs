namespace DDM.Web.Areas.Portal.Startup
{
    public class PortalPageNames
    {
        public static class Common
        {
            public const string Machines = "Machines.Machines";
            public const string Materials = "Materials.Materials";
            public const string SalesOrders = "SalesOrders.SalesOrders";
            public const string VendorCategories = "VendorCategories.VendorCategories";
            public const string Vendors = "Vendors.Vendors";
            public const string Customers = "Customers.Customers";
            public const string CustomerCategories = "CustomerCategories.CustomerCategories";
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";
            public const string WebhookSubscriptions = "Administration.WebhookSubscriptions";
            public const string DynamicEntityParameters = "Administration.DynamicEntityParameters";
            public const string DynamicParameters = "Administration.DynamicParameters";
            public const string EntityDynamicParameters = "Administration.EntityDynamicParameters";

            //FS
            public const string References = "References";

        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";
            public const string Dashboard = "Dashboard";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}