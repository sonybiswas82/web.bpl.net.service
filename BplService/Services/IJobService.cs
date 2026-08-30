namespace BplService.Services
{
    public interface IJobService
    {
        void fireAndForgetJob();
        void delayedJob();
        void recurringJob();
        void continuationJob();

        void ase_team_update_job();
    }
}
