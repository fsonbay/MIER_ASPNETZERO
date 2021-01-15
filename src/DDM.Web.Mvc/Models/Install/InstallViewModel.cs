using System.Collections.Generic;
using Abp.Localization;
using DDM.Install.Dto;

namespace DDM.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
