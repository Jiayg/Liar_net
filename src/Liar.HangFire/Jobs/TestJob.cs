using System.Threading.Tasks;

namespace Liar.HangFire.Jobs
{
    public class TestJob : IBackgroundJob
    { 
        public Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
