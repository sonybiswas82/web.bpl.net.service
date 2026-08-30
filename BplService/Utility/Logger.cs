using Microsoft.Extensions.Configuration;

namespace BplService.Utility
{
    public class Logger
    {
        private readonly string _errorPath;
        private readonly string _succPath;

        public Logger(IConfiguration configuration)
        {
            _errorPath = configuration["FilePaths:ErrorPath"]
                ?? throw new InvalidOperationException("FilePaths:ErrorPath is not configured.");
            _succPath = configuration["FilePaths:SuccPath"]
                ?? throw new InvalidOperationException("FilePaths:SuccPath is not configured.");
        }

        public void CreateErrorLogFile(Exception err)
        {
            Directory.CreateDirectory(_errorPath); // no-op if it already exists

            string pathFile = Path.Combine(_errorPath, $"Sync-Error-{DateTime.Now:yyyyMMdd}.error");

            using var sw = new StreamWriter(pathFile, true);
            sw.WriteLine("**********START ERROR : {0} **********", DateTime.Now);
            sw.WriteLine("Error Occurred On : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
            sw.WriteLine("Exception         : " + err.Message);

            if (err.InnerException != null)
            {
                sw.WriteLine("Inner Exception Type: " + err.InnerException.GetType());
                sw.WriteLine("Inner Exception: " + err.InnerException.Message);
                sw.WriteLine("Inner Source: " + err.InnerException.Source);
                if (err.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(err.InnerException.StackTrace);
                }
            }

            sw.WriteLine("Exception Type: " + err.GetType());
            sw.WriteLine("Stack Trace: ");
            if (err.StackTrace != null)
            {
                sw.WriteLine(err.StackTrace);
                sw.WriteLine();
            }

            sw.WriteLine("=============******** END ERROR ***********===================");
            sw.WriteLine();
        }

        public void CreateSuccLogFile()
        {
            Directory.CreateDirectory(_succPath);

            string pathFile = Path.Combine(_succPath, $"Sync-Succ-{DateTime.Now:yyyyMMdd}.Succ");

            using var sw = new StreamWriter(pathFile, true);
            sw.WriteLine("=============**********START SYNC : {0} **********============", DateTime.Now);
            sw.WriteLine("Synced Done On    : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
            sw.WriteLine("=============**********END SYNC ***********===================");
            sw.WriteLine();
        }
    }
}