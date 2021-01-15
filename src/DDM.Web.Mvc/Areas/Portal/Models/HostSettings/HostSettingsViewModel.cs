using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DDM.Configuration.Host.Dto;
using DDM.Editions.Dto;

namespace DDM.Web.Areas.Portal.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }

        public List<string> EnabledSocialLoginSettings { get; set; } = new List<string>();
    }
}