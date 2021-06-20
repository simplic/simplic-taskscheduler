using Simplic.Framework.Base;
using Simplic.Localization;
using Simplic.UI.MVC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Simplic.TaskScheduler.UI
{
    /// <summary>
    /// Main scheduler editor viewmodel
    /// </summary>
    public class TaskSchedulerEditorViewModel : ViewModelBase
    {
        private ITaskSchedulerConfigurationService taskSchedulerService;
        private ILocalizationService localizationService;
        private ObservableCollection<TaskSchedulerViewModel> schedulers;
        private ObservableCollection<TaskSchedulerViewModel> removedSchedulers;
        private TaskSchedulerViewModel selectedScheduler;
        private ICommand addScheduler;
        private ICommand removeScheduler;

        /// <summary>
        /// Initialize scheduler viewmodel
        /// </summary>
        public TaskSchedulerEditorViewModel()
        {
            taskSchedulerService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskSchedulerConfigurationService>();
            localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();

            schedulers = new ObservableCollection<TaskSchedulerViewModel>();
            removedSchedulers = new ObservableCollection<TaskSchedulerViewModel>();

            foreach (var scheduler in taskSchedulerService.GetAll().OrderBy(x => x.Name))
            {
                schedulers.Add(new TaskSchedulerViewModel(scheduler)
                {
                    Parent = this
                });
            }

            if (schedulers.Count == 0)
            {
                schedulers.Add(new TaskSchedulerViewModel(new TaskSchedulerConfiguration())
                {
                    Name = "Scheduler",
                    Parent = this
                });
            }

            SelectedScheduler = schedulers.FirstOrDefault();

            AddScheduler = new RelayCommand((e) => 
            {
                var newScheduler = new TaskSchedulerViewModel(new TaskSchedulerConfiguration())
                {
                    Name = "Scheduler",
                    Parent = this
                };

                newScheduler.IsPrivate = !Development.InDevelopMode();

                IsDirty = true;
                schedulers.Add(newScheduler);
                SelectedScheduler = newScheduler;
            });

            RemoveScheduler = new RelayCommand((e) =>
            {
                var res = MessageBox.Show(localizationService.Translate("scheduler_remove_message"), localizationService.Translate("scheduler_remove_title"), MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                {
                    IsDirty = true;
                    removedSchedulers.Add(SelectedScheduler);
                    schedulers.Remove(SelectedScheduler);
                    SelectedScheduler = schedulers.FirstOrDefault();
                }
                 
            }, (e) => { return SelectedScheduler != null; });
        }

        /// <summary>
        /// Save changes
        /// </summary>
        internal void Save()
        {
            foreach (var scheduler in schedulers)
            {
                taskSchedulerService.Save(scheduler.Model);
            }

            foreach (var scheduler in removedSchedulers)
            {
                taskSchedulerService.Delete(scheduler.Model);
            }

            removedSchedulers.Clear();
        }

        /// <summary>
        /// Gets or sets a list of schedulers
        /// </summary>
        public ObservableCollection<TaskSchedulerViewModel> Schedulers
        {
            get
            {
                return schedulers;
            }

            set
            {
                schedulers = value;
            }
        }

        /// <summary>
        /// Gets or sets the currently selected scheduler
        /// </summary>
        public TaskSchedulerViewModel SelectedScheduler
        {
            get
            {
                return selectedScheduler;
            }

            set
            {
                selectedScheduler = value;
                RaisePropertyChanged(nameof(SelectedScheduler));
                RaisePropertyChanged(nameof(IsSchedulerSelected));
            }
        }

        /// <summary>
        /// Gets whether there is currently a selected scheduler
        /// </summary>
        public bool IsSchedulerSelected
        {
            get
            {
                return SelectedScheduler != null;
            }
        }

        /// <summary>
        /// Gets or sets the add scheduler command
        /// </summary>
        public ICommand AddScheduler
        {
            get
            {
                return addScheduler;
            }

            set
            {
                addScheduler = value;
            }
        }

        /// <summary>
        /// Gets or sets the remove scheduler command
        /// </summary>
        public ICommand RemoveScheduler
        {
            get
            {
                return removeScheduler;
            }

            set
            {
                removeScheduler = value;
            }
        }
    }
}
