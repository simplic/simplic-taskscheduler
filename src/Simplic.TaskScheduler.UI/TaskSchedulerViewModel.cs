using Simplic.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.TaskScheduler.UI
{
    /// <summary>
    /// Single scheduler viewmodel
    /// </summary>
    public class TaskSchedulerViewModel : ViewModelBase
    {
        private TaskSchedulerConfiguration model;

        /// <summary>
        /// Initialize scheduler viewmodel
        /// </summary>
        /// <param name="model">Model instance</param>
        public TaskSchedulerViewModel(TaskSchedulerConfiguration model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets the model instance
        /// </summary>
        public TaskSchedulerConfiguration Model
        {
            get
            {
                return model;
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                RaisePropertyChanged(nameof(Name));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the current machine name
        /// </summary>
        public string MachineName
        {
            get
            {
                return model.MachineName;
            }
            set
            {
                model.MachineName = value;
                RaisePropertyChanged(nameof(MachineName));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the application server name
        /// </summary>
        public string AppServerName
        {
            get
            {
                return model.AppServerName;
            }
            set
            {
                model.AppServerName = value;
                RaisePropertyChanged(nameof(AppServerName));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets whether the scheduler is private
        /// </summary>
        public bool IsPrivate
        {
            get
            {
                return model.IsPrivate;
            }
            set
            {
                model.IsPrivate = value;
                RaisePropertyChanged(nameof(IsPrivate));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the execution mode
        /// </summary>
        public ExecutionTimeMode ExecutionTimeMode
        {
            get
            {
                return model.ExecutionTimeMode;
            }
            set
            {
                model.ExecutionTimeMode = value;
                RaisePropertyChanged(nameof(ExecutionTimeMode));
                RaisePropertyChanged(nameof(IsSecondsEnabled));
                RaisePropertyChanged(nameof(IsCronEnabled));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets a list of available time modes
        /// </summary>
        public IList<ExecutionTimeMode> ExecutionTimeModes
        {
            get;
        } = new List<ExecutionTimeMode> { TaskScheduler.ExecutionTimeMode.Seconds, TaskScheduler.ExecutionTimeMode.Cron };

        /// <summary>
        /// Gets whether seconds are enabled
        /// </summary>
        public bool IsSecondsEnabled
        {
            get
            {
                return ExecutionTimeMode == ExecutionTimeMode.Seconds;
            }
        }

        /// <summary>
        /// Gets whether cron is enabled
        /// </summary>
        public bool IsCronEnabled
        {
            get
            {
                return ExecutionTimeMode == ExecutionTimeMode.Cron;
            }
        }

        /// <summary>
        /// Gets or sets the recurring seconds
        /// </summary>
        public int Seconds
        {
            get
            {
                return model.Seconds;
            }
            set
            {
                model.Seconds = value;
                RaisePropertyChanged(nameof(Seconds));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets cron "minute" settings
        /// </summary>
        public string Minute
        {
            get
            {
                return model.Minute;
            }
            set
            {
                model.Minute = value;
                RaisePropertyChanged(nameof(Minute));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets cron "hour" settings
        /// </summary>
        public string Hour
        {
            get
            {
                return model.Hour;
            }
            set
            {
                model.Hour = value;
                RaisePropertyChanged(nameof(Hour));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets cron "day" settings
        /// </summary>
        public string Day
        {
            get
            {
                return model.Day;
            }
            set
            {
                model.Day = value;
                RaisePropertyChanged(nameof(Day));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets cron "month" settings
        /// </summary>
        public string Month
        {
            get
            {
                return model.Month;
            }
            set
            {
                model.Month = value;
                RaisePropertyChanged(nameof(Month));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets cron "DayOfWeek" settings
        /// </summary>
        public string DayOfWeek
        {
            get
            {
                return model.DayOfWeek;
            }
            set
            {
                model.DayOfWeek = value;
                RaisePropertyChanged(nameof(DayOfWeek));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets whether the job is active
        /// </summary>
        public bool IsActive
        {
            get
            {
                return model.IsActive;
            }
            set
            {
                model.IsActive = value;
                RaisePropertyChanged(nameof(IsActive));
                IsDirty = true;
            }
        }
    }
}
