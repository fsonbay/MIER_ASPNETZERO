using System.Collections.Generic;
using DDM.Authorization.Delegation;
using DDM.Authorization.Users.Delegation.Dto;

namespace DDM.Web.Areas.Portal.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
