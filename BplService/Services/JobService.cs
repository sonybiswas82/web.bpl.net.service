using BplService.Utility;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BplService.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger _logger;
        private DataReader _dataReader = new DataReader();
        IConfiguration _configuration;
        Logger _loggerUtility;  

        public JobService(ILogger<JobService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void continuationJob()
        {
            _logger.LogInformation("Continuation job executed at: {time}", DateTimeOffset.Now);
        }

        public void delayedJob()
        {
            _logger.LogInformation("Delayed job executed at: {time}", DateTimeOffset.Now);
        }

        public void fireAndForgetJob()
        {
            _logger.LogInformation("Fire-and-forget job executed at: {time}", DateTimeOffset.Now);
        }

        public void recurringJob()
        {
            _logger.LogInformation("Recurring job executed at: {time}", DateTimeOffset.Now);
        }

        public void ase_team_update_job()
        {
            _loggerUtility = new Logger(_configuration);
            string conn = _configuration.GetConnectionString("ConnDbOrderSummaryBPL14");
            _dataReader.ExecuteNonQueryForSP("usp_AseTeamUpdate", conn);
            _loggerUtility.CreateSuccLogFile();
        }
    }
}
