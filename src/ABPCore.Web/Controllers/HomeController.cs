using System.Threading.Tasks;
using ABPCore.BussinessPlan;
using ABPCore.EECPlus;
using Microsoft.AspNetCore.Mvc;

namespace ABPCore.Web.Controllers
{
    public class HomeController : ABPCoreControllerBase
    {
        private readonly IBussinessPlanService _BussinessPlanService;
        private readonly IEECPlusService _EECPlusService;

        //public HomeController(IBussinessPlanService BussinessPlanService)
        //{
        //    _BussinessPlanService = BussinessPlanService;
        //}

        public HomeController(IEECPlusService EECPlusService, IBussinessPlanService BussinessPlanService)
        {
            _EECPlusService = EECPlusService;
            _BussinessPlanService = BussinessPlanService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _EECPlusService.GetAllPhotoNotes());
        }

        public async Task<ActionResult> About()
        {
            //await _BussinessPlanService.InsertNewItem();
            return View(await _BussinessPlanService.GetAllBPItems());
        }
    }
}