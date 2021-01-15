using Abp.AutoMapper;
using DDM.MultiTenancy.Dto;

namespace DDM.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
