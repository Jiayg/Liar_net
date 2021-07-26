using Liar.Application.Contracts.IServices;

namespace Liar.Services
{
    public class TestService :  ITestService
    {
        public string get()
        {
            return "hello world";
        }
    }
}
