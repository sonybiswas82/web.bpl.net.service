using BplService.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BplService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager; 

    public JobController(IJobService jobService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
    {
        _jobService = jobService;
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
    }

    //[HttpGet("/fireAndForgetJob")]
    //public ActionResult CreateFireAndForgetJob()
    //{
    //    _backgroundJobClient.Enqueue(() => _jobService.fireAndForgetJob());
    //    return Ok();
    //}

    //[HttpGet("/delayedJob")]
    //public ActionResult CreateDelayedJob()
    //{
    //    _backgroundJobClient.Schedule(() => _jobService.delayedJob(), TimeSpan.FromSeconds(120));
    //    return Ok();
    //}

    //[HttpGet("/recurringJob")]
    //public ActionResult CreateRecurringJob()
    //{
    //    _recurringJobManager.AddOrUpdate("recurring-job", () => _jobService.recurringJob(), Cron.MinuteInterval(10));
    //    return Ok();
    //}

    //[HttpGet("/continuationJob")]
    //public ActionResult CreateContinuationJob()
    //{
    //    var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.fireAndForgetJob());
    //    _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.continuationJob());
    //    return Ok();
    //}

    [HttpGet("/ase_team_update")] // this is URL
    public ActionResult ase_team_update_job()
    {
        _recurringJobManager.AddOrUpdate("CallStoredProcedure", () => _jobService.ase_team_update_job(), Cron.MinuteInterval(5));
        return Ok();
    }
}
