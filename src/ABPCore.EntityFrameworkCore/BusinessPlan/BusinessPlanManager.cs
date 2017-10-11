using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using ABPCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABPCore.BusinessPlan
{
    public class BusinessPlanManager : IDomainService
    {

        private readonly IExtendedRepository<BPEC_RetailItem> _BPEC_RetailItemRepository;
        private readonly IExtendedRepository<PM_DevelopRemark> _PM_DevelopRemarkRepository;
        public BusinessPlanManager(
            IExtendedRepository<BPEC_RetailItem> BPEC_RetailItemRepository, IExtendedRepository<PM_DevelopRemark> PM_DevelopRemarkRepository)
        {
            _BPEC_RetailItemRepository = BPEC_RetailItemRepository;
            _PM_DevelopRemarkRepository = PM_DevelopRemarkRepository;
        }


        public async Task<List<BPEC_RetailItem>> GetAllBPItems()
        {
            _PM_DevelopRemarkRepository.GetAllList();

           return await _BPEC_RetailItemRepository.GetAllListAsync();
        }

        public async Task InsertNewItem()
        {
            //await _BPEC_RetailItemRepository.InsertAsync(new BPEC_RetailItem()
            //{
            //    ItemNo = "AAAAAAA-Test1234",
            //    FamilyNo = "AAA",
            //    Loc = "LVM"
            //});

            await _BPEC_RetailItemRepository.DeleteAsync(o => o.ItemNo == "AAAAAAA-Test1234");
        }
    }
}
