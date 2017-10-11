using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using ABPCore.EntityFrameworkCore;

namespace ABPCore.EECPlus
{
    public class EECPlusManager: IDomainService
    {

        private readonly IRepository<PH_PhotoNote> _PH_PhotoNoteRepository;
        private readonly IExtendedRepository<BPEC_RetailItem> _BPEC_RetailItemRepository;
        public EECPlusManager(
            IRepository<PH_PhotoNote> PH_PhotoNoteRepository, IExtendedRepository<BPEC_RetailItem> BPEC_RetailItemRepository)
        {
            _PH_PhotoNoteRepository = PH_PhotoNoteRepository;
            _BPEC_RetailItemRepository = BPEC_RetailItemRepository;
        }

        public async Task<List<PH_PhotoNote>> GetAllPhotoNotes()
        {
            //_PH_PhotoNoteRepository.Insert(new PH_PhotoNote()
            //{
            //    Note = "AAAAAAAAAAAAAAAAAAAAAAA",
            //    CreatedBy = "ZEB",
            //    CreatedDate = new DateTime(2017, 12, 12)
            //});

            _PH_PhotoNoteRepository.Delete(o => o.CreatedBy == "ZEB");

            //var result= _BPEC_RetailItemRepository.GetAllList();

            return await _PH_PhotoNoteRepository.GetAllListAsync();
        }
    }
}
