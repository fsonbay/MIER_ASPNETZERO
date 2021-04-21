using DDM.SalesInvoices.Dtos;
using DDM.SalesInvoices;
using DDM.ProductionStatuses.Dtos;
using DDM.ProductionStatuses;
using DDM.SalesOrderLines;
using DDM.Machines.Dtos;
using DDM.Machines;
using DDM.Materials.Dtos;
using DDM.Materials;
using DDM.SalesOrders.Dtos;
using DDM.SalesOrders;
using DDM.VendorCategories.Dtos;
using DDM.VendorCategories;
using DDM.Vendors.Dtos;
using DDM.Vendors;
using DDM.Customers.Dtos;
using DDM.Customers;
using DDM.CustomerCategories.Dtos;
using DDM.CustomerCategories;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityParameters;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using DDM.Auditing.Dto;
using DDM.Authorization.Accounts.Dto;
using DDM.Authorization.Delegation;
using DDM.Authorization.Permissions.Dto;
using DDM.Authorization.Roles;
using DDM.Authorization.Roles.Dto;
using DDM.Authorization.Users;
using DDM.Authorization.Users.Delegation.Dto;
using DDM.Authorization.Users.Dto;
using DDM.Authorization.Users.Importing.Dto;
using DDM.Authorization.Users.Profile.Dto;
using DDM.Chat;
using DDM.Chat.Dto;
using DDM.DynamicEntityParameters.Dto;
using DDM.Editions;
using DDM.Editions.Dto;
using DDM.Friendships;
using DDM.Friendships.Cache;
using DDM.Friendships.Dto;
using DDM.Localization.Dto;
using DDM.MultiTenancy;
using DDM.MultiTenancy.Dto;
using DDM.MultiTenancy.HostDashboard.Dto;
using DDM.MultiTenancy.Payments;
using DDM.MultiTenancy.Payments.Dto;
using DDM.Notifications.Dto;
using DDM.Organizations.Dto;
using DDM.Sessions.Dto;
using DDM.WebHooks.Dto;

namespace DDM
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditSalesInvoiceDto, SalesInvoice>().ReverseMap();
            configuration.CreateMap<SalesInvoiceDto, SalesInvoice>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductionStatusDto, ProductionStatus>().ReverseMap();
            configuration.CreateMap<ProductionStatusDto, ProductionStatus>().ReverseMap();
            configuration.CreateMap<CreateOrEditMachineDto, Machine>().ReverseMap();
            configuration.CreateMap<MachineDto, Machine>().ReverseMap();
            configuration.CreateMap<CreateOrEditMaterialDto, Material>().ReverseMap();
            configuration.CreateMap<MaterialDto, Material>().ReverseMap();




            
            configuration.CreateMap<CreateOrEditVendorCategoryDto, VendorCategory>().ReverseMap();
            configuration.CreateMap<VendorCategoryDto, VendorCategory>().ReverseMap();
            configuration.CreateMap<CreateOrEditVendorDto, Vendor>().ReverseMap();
            configuration.CreateMap<VendorDto, Vendor>().ReverseMap();
            configuration.CreateMap<CreateOrEditCustomerDto, Customer>().ReverseMap();
            configuration.CreateMap<CustomerDto, Customer>().ReverseMap();
            configuration.CreateMap<CreateOrEditCustomerCategoryDto, CustomerCategory>().ReverseMap();
            configuration.CreateMap<CustomerCategoryDto, CustomerCategory>().ReverseMap();



            //Sales Order
            configuration.CreateMap<SalesOrderDto, SalesOrder>().ReverseMap();










            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();





            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicParameter, DynamicParameterDto>().ReverseMap();
            configuration.CreateMap<DynamicParameterValue, DynamicParameterValueDto>().ReverseMap();
            configuration.CreateMap<EntityDynamicParameter, EntityDynamicParameterDto>()
                .ForMember(dto => dto.DynamicParameterName,
                    options => options.MapFrom(entity => entity.DynamicParameter.ParameterName));
            configuration.CreateMap<EntityDynamicParameterDto, EntityDynamicParameter>();

            configuration.CreateMap<EntityDynamicParameterValue, EntityDynamicParameterValueDto>().ReverseMap();
            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}