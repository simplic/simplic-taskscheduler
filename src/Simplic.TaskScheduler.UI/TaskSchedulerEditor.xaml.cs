using Simplic.Framework.UI;
using Simplic.Icon;
using Simplic.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Simplic.TaskScheduler.UI
{
    /// <summary>
    /// Interaction logic for TaskSchedulerEditor.xaml
    /// </summary>
    public partial class TaskSchedulerEditor : DefaultRibbonWindow
    {
        private IIconService iconService;
        private ILocalizationService localizationService;

        /// <summary>
        /// Initialize editor
        /// </summary>
        public TaskSchedulerEditor()
        {
            InitializeComponent();

            iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();
            localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();

            // Setup datacontext
            this.DataContext = new TaskSchedulerEditorViewModel();

            // Setup window behaviour
            AddPagingObject(Guid.Empty);

            WindowMode = WindowMode.Edit;

            AllowDelete = false;
            AllowSave = true;
            AllowPaging = false;

            // Add "add" and "remove" button
            var addSchedulerButton = new RadRibbonButton()
            {
                Text = localizationService.Translate("scheduler_new"),
                LargeImage = iconService.GetByNameAsImage("action_add_16xLG"),
                SmallImage = iconService.GetByNameAsImage("action_add_16xLG"),
                Size = Telerik.Windows.Controls.RibbonView.ButtonSize.Medium
            };
        BindingOperations.SetBinding(addSchedulerButton, RadRibbonButton.CommandProperty, new Binding("AddScheduler"));

            var removeSchedulerButton = new RadRibbonButton()
            {
                Text = localizationService.Translate("scheduler_remove"),
                LargeImage = iconService.GetByNameAsImage("remove_16x"),
                SmallImage = iconService.GetByNameAsImage("remove_16x"),
                Size = Telerik.Windows.Controls.RibbonView.ButtonSize.Medium
            };
        BindingOperations.SetBinding(removeSchedulerButton, RadRibbonButton.CommandProperty, new Binding("RemoveScheduler"));

            RadRibbonDataGroup.Items.Add(addSchedulerButton);
            RadRibbonDataGroup.Items.Add(removeSchedulerButton);
        }

    /// <summary>
    /// Save changes
    /// </summary>
    /// <param name="e"></param>
    public override void OnSave(WindowSaveEventArg e)
    {
        ViewModel.Save();

        base.OnSave(e);
    }

    /// <summary>
    /// Show editor instance
    /// </summary>
    public static void ShowEditor()
    {
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            var d = new TaskSchedulerEditor();
            d.Show();
        }));
    }

    /// <summary>
    /// Show editor instance
    /// </summary>
    public static void ShowEditor(Guid appId)
    {
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            var d = new TaskSchedulerEditor();
            d.Show();
        }));
    }

    /// <summary>
    /// Gets datacontext as <see cref="TaskSchedulerEditorViewModel"/>
    /// </summary>
    public TaskSchedulerEditorViewModel ViewModel
    {
        get
        {
            return DataContext as TaskSchedulerEditorViewModel;
        }
    }
}
}
