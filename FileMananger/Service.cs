using System.ServiceProcess;
using System.Threading;
using DB.Samples;

namespace FileWatcherService
{
    public partial class Service : ServiceBase
    {
        Logger logger;
        public Service()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
            var dataManager = new DataManager();
            dataManager.GetData(1, 5, logger.scrDirPath);
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
