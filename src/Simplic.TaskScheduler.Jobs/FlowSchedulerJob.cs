using CommonServiceLocator;
using Hangfire.Server;
using Simplic.Flow.Event;
using Simplic.Session;
using System;

namespace Simplic.TaskScheduler.Jobs
{
    /// <summary>
    /// Job that will be fired by the task scheduler service
    /// </summary>
    public class FlowSchedulerJob
    {
        private IFlowEventService eventService;
        private ISessionService sessionService;

        /// <summary>
        /// Initialize job
        /// </summary>
        public FlowSchedulerJob()
        {
            eventService = ServiceLocator.Current.GetInstance<IFlowEventService>();
            sessionService = ServiceLocator.Current.GetInstance<ISessionService>();
        }

        /// <summary>
        /// Enqueue simplic flow event
        /// </summary>
        /// <param name="schedulerName">Scheduler name. Will be used for filtering within a flow</param>
        public void Enqueue(string schedulerName)
        {
            eventService.InvokeEvent(new FlowEventArgs()
            {
                EventName = "OnSchedulerJobStarted",
                UserId = sessionService?.CurrentSession?.UserId ?? 0,
                ObjectId = schedulerName
            });
        }
    }
}
