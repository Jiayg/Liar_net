using Liar.Application.Contracts.IServices;

namespace Liar.Services
{
    public class TestService : LiarAppService, ITestService
    {
        public string get()
        {
            return "hello world";
        }
    }
}
