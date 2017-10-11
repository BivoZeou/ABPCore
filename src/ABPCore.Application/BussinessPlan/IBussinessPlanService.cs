using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace ABPCore.BussinessPlan
{
    public interface IBussinessPlanService : IApplicationService
    {
        Task<List<BPEC_RetailItem>> GetAllBPItems();

        Task InsertNewItem();
    }
}
