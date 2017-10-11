using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using ABPCore.BusinessPlan;
using ABPCore.BussinessPlan;

namespace ABPCore.EECPlus
{
    public class EECPlusService: ApplicationService,IEECPlusService
    {
        private readonly EECPlusManager _epManager;
        public EECPlusService(EECPlusManager epManager)
        {
            _epManager = epManager;
        }

        public async Task<List<PH_PhotoNote>> GetAllPhotoNotes()
        {
            return await _epManager.GetAllPhotoNotes();
        }
    }
}
