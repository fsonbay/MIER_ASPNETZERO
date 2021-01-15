using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Authorization.Delegation;
using DDM.Authorization.Users.Delegation;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Components.PortalActiveUserDelegationsCombobox
{
    public class PortalActiveUserDelegationsComboboxViewComponent : DDMViewComponent
    {
        private readonly IUserDelegationAppService _userDelegationAppService;
        private readonly IUserDelegationConfiguration _userDelegationConfiguration;

        public PortalActiveUserDelegationsComboboxViewComponent(
            IUserDelegationAppService userDelegationAppService, 
            IUserDelegationConfiguration userDelegationConfiguration)
        {
            _userDelegationAppService = userDelegationAppService;
            _userDelegationConfiguration = userDelegationConfiguration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {
            var activeUserDelegations = await _userDelegationAppService.GetActiveUserDelegations();
            var model = new ActiveUserDelegationsComboboxViewModel
            {
                UserDelegations = activeUserDelegations,
                UserDelegationConfiguration = _userDelegationConfiguration
            };

            return View(model);
        }
    }
}
