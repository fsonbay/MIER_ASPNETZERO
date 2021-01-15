using DDM.Machines.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.Machines
{
    public class CreateOrEditMachineModalViewModel
    {
        public CreateOrEditMachineDto Machine { get; set; }

        public bool IsEditMode => Machine.Id.HasValue;
    }
}