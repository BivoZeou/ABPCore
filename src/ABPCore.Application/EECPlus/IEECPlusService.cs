using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace ABPCore.EECPlus
{
    public interface IEECPlusService : IApplicationService
    {
         Task<List<PH_PhotoNote>> GetAllPhotoNotes();
    }
}
