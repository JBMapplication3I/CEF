namespace SiteManager.ViewModels.DesignerSampleData
{
    public class MainWindowViewModelDesignSample : MainWindowViewModel
    {
        public MainWindowViewModelDesignSample()
        {
            // Write values that override defaults for design time
            NewEnabled = true;
            OpenEnabled = false;
            SaveEnabled = false;
            Progress = 60;
            RunOutput = new RunOutputViewModelDesignSample();
            RunSettingsViewModel = new RunSettingsViewModelDesignSample();
            LastEventText = "The last event text";
            LastAltEventText = "The last alternate event text";
        }
    }
}
