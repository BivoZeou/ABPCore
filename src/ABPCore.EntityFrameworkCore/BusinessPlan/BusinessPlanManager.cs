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
        private readonly IRepository<PH_PhotoNote> _PH_PhotoNoteRepository;
        public BusinessPlanManager(
            IExtendedRepository<BPEC_RetailItem> BPEC_RetailItemRepository, IExtendedRepository<PM_DevelopRemark> PM_DevelopRemarkRepository, IRepository<PH_PhotoNote> PH_PhotoNoteRepository)
        {
            _BPEC_RetailItemRepository = BPEC_RetailItemRepository;
            _PM_DevelopRemarkRepository = PM_DevelopRemarkRepository;
            _PH_PhotoNoteRepository = PH_PhotoNoteRepository;
        }


        public async Task<List<BPEC_RetailItem>> GetAllBPItems()
        {
            var pmList = _PM_DevelopRemarkRepository.GetAllList();

            var notesList = _PH_PhotoNoteRepository.GetAllList();

            var pmList2 = _PM_DevelopRemarkRepository.FirstOrDefault(o => o.Topic != string.Empty);

            var pmList3 = _PM_DevelopRemarkRepository.FirstOrDefault(o => o.Content != string.Empty);


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
