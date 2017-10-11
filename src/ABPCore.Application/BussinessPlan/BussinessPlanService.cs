using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using ABPCore.BusinessPlan;
using ABPCore.EECPlus;

namespace ABPCore.BussinessPlan
{
    public class BussinessPlanService : ApplicationService,IBussinessPlanService
    {
        private readonly BusinessPlanManager _bpManager;
        private readonly EECPlusManager _epManager;
        public BussinessPlanService(BusinessPlanManager BusinessPlanManager, EECPlusManager epManager)
        {
            _bpManager = BusinessPlanManager;
            _epManager = epManager;
        }

        public async Task<List<BPEC_RetailItem>> GetAllBPItems()
        {

            //await _epManager.GetAllPhotoNotes();

            return await _bpManager.GetAllBPItems();
        }


        public async Task InsertNewItem()
        {
            await _bpManager.InsertNewItem();
        }
    }
}
