using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace ThreeTargets
{
    public class LiveTileAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public LiveTileAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// スケジュールされたタスクを実行するエージェント
        /// </summary>
        /// <param name="task">
        /// 呼び出されたタスク
        /// </param>
        /// <remarks>
        /// このメソッドは、定期的なタスクまたはリソースを集中的に使用するタスクの呼び出し時に呼び出されます
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            var setting = IOManager.GetManager().LoadFromAgent();
            var manifests = IOManager.GetManager().LoadManifests(false);
            var message = "";
            if (setting.ID.Equals(Guid.Empty))
            {
                var random = new Random();
                message = manifests[random.Next(2)].Title;
            }
            else
            {
                message = manifests.Where(m => m.ID.Equals(setting.ID)).FirstOrDefault().Title;
            }
            var tile = ShellTile.ActiveTiles.FirstOrDefault();
            var newTile = new StandardTileData
            {
                // Title = "TEST",
                // BackBackgroundImage = new Uri(@"https://twimg0-a.akamaihd.net/profile_images/1339209451/dog_icon3_master.png"),
                Count = 3,
                BackTitle = "目標",
                BackContent = message,
            };

            tile.Update(newTile);

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
#endif

            NotifyComplete();
        }
    }
}