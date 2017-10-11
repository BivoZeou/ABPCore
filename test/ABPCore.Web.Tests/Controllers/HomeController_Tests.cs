using System.Threading.Tasks;
using ABPCore.Web.Controllers;
using Shouldly;
using Xunit;

namespace ABPCore.Web.Tests.Controllers
{
    public class HomeController_Tests: ABPCoreWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
