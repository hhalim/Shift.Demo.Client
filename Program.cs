using Shift.Demo.Jobs;
using Shift.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shift.Demo.Client
{
    class Program
    {
        private static JobClient jobClient;
        private static List<string> addedJobIDs;
        static void Main(string[] args)
        {
            InitShiftClient();

            ConsoleKeyInfo cki;
            var breakFlag = true;
            do
            {
                
                cki = DisplayMenu();  // show the key as you read it
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        AddJob();
                        break;
                    case "2":
                        ShowJobsProgress();
                        break;
                    case "3":
                        StopJobs();
                        break;
                    case "4":
                        PauseJobs();
                        break;
                    case "5":
                        ContinueJobs();
                        break;
                    case "6":
                        ResetJobs();
                        break;
                    case "7":
                        DeleteJobs();
                        break;
                    case "8":
                        breakFlag = false;
                        break;
                }
            } while (breakFlag);

            //clean up jobs when exiting
            if(addedJobIDs.Count > 0)
            {
                DeleteJobs();
            }
        }

        static public ConsoleKeyInfo DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Shift Client Demo");
            Console.WriteLine("1. Add a 'Hello World!' job.");
            Console.WriteLine("2. Show progress for jobs.");
            Console.WriteLine("3. Send 'STOP' command.");
            Console.WriteLine("4. Send 'PAUSE' command.");
            Console.WriteLine("5. Send 'CONTINUE' command.");
            Console.WriteLine("6. Reset all jobs.");
            Console.WriteLine("7. Delete jobs.");
            Console.WriteLine("8. Exit.");
            Console.WriteLine("Press (8) key to exit.");
            return Console.ReadKey(false);
        }

        private static void InitShiftClient()
        {
            var config = new Shift.ClientConfig();
            config.DBConnectionString = ConfigurationManager.ConnectionStrings["ShiftDBConnection"].ConnectionString;
            config.DBAuthKey = ConfigurationManager.AppSettings["DocumentDBAuthKey"];
            config.StorageMode = ConfigurationManager.AppSettings["StorageMode"];

            //config.EncryptionKey = "[OPTIONAL_ENCRYPTIONKEY]"; //optional, will encrypt parameters in DB

            jobClient = new JobClient(config);

            addedJobIDs = new List<string>();
        }

        private static void AddJob()
        {
            var job = new TestJob();
            var progress = new SynchronousProgress<ProgressInfo>(); //just a place holder will be replaced by progress object from the server
            var cancelToken = (new CancellationTokenSource()).Token; //just a place holder will be replaced by Token object from the server
            var pauseToken = (new PauseTokenSource()).Token; //just a place holder will be replaced by Token object from the server
            var jobID = jobClient.Add("Shift.Demo.Client", () => job.Start("Hello World!", progress, cancelToken, pauseToken));
            addedJobIDs.Add(jobID);
            Console.WriteLine();
            Console.WriteLine("==> New job added to Shift DB table");
        }

        private static void StopJobs()
        {
            jobClient.SetCommandStop(addedJobIDs);

            Console.WriteLine();
            Console.WriteLine("==> 'STOP' command is sent.");
        }

        private static void PauseJobs()
        {
            jobClient.SetCommandPause(addedJobIDs);

            Console.WriteLine();
            Console.WriteLine("==> 'PAUSE' command is sent.");
        }

        private static void ContinueJobs()
        {
            jobClient.SetCommandContinue(addedJobIDs);

            Console.WriteLine();
            Console.WriteLine("==> 'CONTINUE' command is sent.");
        }

        private static void DeleteJobs()
        {
            jobClient.DeleteJobs(addedJobIDs);
            addedJobIDs.Clear();

            Console.WriteLine();
            Console.WriteLine("==> Delete Job(s) on Shift DB is completed.");
        }

        private static void ResetJobs()
        {
            jobClient.ResetJobs(addedJobIDs);

            Console.WriteLine();
            Console.WriteLine("==> Reset Job(s) on Shift DB table is completed.");
        }

        private static void ShowJobsProgress()
        {
            if (addedJobIDs.Count > 0)
            {
                Console.Clear();
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;
                WriteAt("*** Press ESC to exit progress ***", 0, 0);
                do
                {
                    var index = 1;
                    foreach (var jobID in addedJobIDs)
                    {
                        var progress = jobClient.GetProgress(jobID);
                        if (progress != null)
                        {
                            var msg = progress.JobID + ": " + string.Format("{0}%", progress.Percent.GetValueOrDefault()) + " " + progress.StatusLabel + " " + progress.Updated.GetValueOrDefault().ToString("MM/dd/yy hh:mm:ss tt");
                            WriteAt(msg, 0, index);
                            index++;
                        }
                        Thread.Sleep(1000);
                    }
                }
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));

                Console.WriteLine();
                Console.WriteLine("==> Get Job(s) Progress is completed.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("==> No Job(s) Progress found.");
            }

        }

        protected static int origRow;
        protected static int origCol;
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
