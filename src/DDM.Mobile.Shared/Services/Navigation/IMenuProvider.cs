using System.Collections.Generic;
using MvvmHelpers;
using DDM.Models.NavigationMenu;

namespace DDM.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}