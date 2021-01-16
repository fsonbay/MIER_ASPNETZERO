using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using DDM.Authorization;

namespace DDM.Web.Areas.Portal.Startup
{
    public class PortalNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "Portal/HostDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Dashboard)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Machines,
                        L("Machines"),
                        url: "Portal/Machines",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Machines)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Materials,
                        L("Materials"),
                        url: "Portal/Materials",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Materials)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.SalesOrders,
                        L("SalesOrders"),
                        url: "Portal/SalesOrders",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_SalesOrders)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.VendorCategories,
                        L("VendorCategories"),
                        url: "Portal/VendorCategories",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_VendorCategories)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Vendors,
                        L("Vendors"),
                        url: "Portal/Vendors",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Vendors)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Customers,
                        L("Customers"),
                        url: "Portal/Customers",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Customers)
                    )
                )
                .AddItem(new MenuItemDefinition(
                    PortalPageNames.Host.Tenants,
                    L("Tenants"),
                    url: "Portal/Tenants",
                    icon: "flaticon-list-3",
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Host.Editions,
                        L("Editions"),
                        url: "Portal/Editions",
                        icon: "flaticon-app",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                    )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "Portal/TenantDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Administration,
                        L("References"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.CustomerCategories,
                        L("CustomerCategories"),
                        url: "Portal/CustomerCategories",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_CustomerCategories)
                    )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.ProductionStatuses,
                        L("ProductionStatuses"),
                        url: "Portal/ProductionStatuses",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_ProductionStatuses)
                    )
                )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "Portal/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.Roles,
                            L("Roles"),
                            url: "Portal/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.Users,
                            L("Users"),
                            url: "Portal/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.Languages,
                            L("Languages"),
                            url: "Portal/Languages",
                            icon: "flaticon-tabs",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Languages)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "Portal/AuditLogs",
                            icon: "flaticon-folder-1",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_AuditLogs)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "Portal/Maintenance",
                            icon: "flaticon-lock",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Maintenance)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Tenant.SubscriptionManagement,
                            L("Subscription"),
                            url: "Portal/SubscriptionManagement",
                            icon: "flaticon-refresh",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.UiCustomization,
                            L("VisualSettings"),
                            url: "Portal/UiCustomization",
                            icon: "flaticon-medical",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_UiCustomization)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.WebhookSubscriptions,
                            L("WebhookSubscriptions"),
                            url: "Portal/WebhookSubscription",
                            icon: "flaticon2-world",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_WebhookSubscription)
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Common.DynamicEntityParameters,
                            L("DynamicParameters"),
                            icon: "flaticon-interface-8"
                        ).AddItem(new MenuItemDefinition(
                                PortalPageNames.Common.DynamicParameters,
                                L("Definitions"),
                                url: "Portal/DynamicParameter",
                                icon: "flaticon-map",
                                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_DynamicParameters)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                PortalPageNames.Common.EntityDynamicParameters,
                                L("EntityDynamicParameters"),
                                url: "Portal/EntityDynamicParameter",
                                icon: "flaticon-map",
                                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_EntityDynamicParameters)
                            )
                        )
                    ).AddItem(new MenuItemDefinition(
                            PortalPageNames.Host.Settings,
                            L("Settings"),
                            url: "Portal/HostSettings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            PortalPageNames.Tenant.Settings,
                            L("Settings"),
                            url: "Portal/Settings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_Settings)
                        )
                    )
                ).AddItem(new MenuItemDefinition(
                        PortalPageNames.Common.DemoUiComponents,
                        L("DemoUiComponents"),
                        url: "Portal/DemoUiComponents",
                        icon: "flaticon-shapes",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DDMConsts.LocalizationSourceName);
        }
    }
}