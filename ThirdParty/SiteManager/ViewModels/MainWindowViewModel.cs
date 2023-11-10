namespace SiteManager.ViewModels
{
    using System;
    using System.Collections.Concurrent;

    public class MainWindowViewModel : ViewModelBase
    {
        protected override ConcurrentDictionary<string, object?> Values { get; } = new()
        {
            [nameof(Progress)] = 0,
            [nameof(IsRunning)] = false,
            [nameof(NewEnabled)] = true,
            [nameof(OpenEnabled)] = true,
            [nameof(SaveEnabled)] = false,
            [nameof(WordWrapEnabled)] = true,
            [nameof(LastEventText)] = (string?)null,
            [nameof(LastAltEventText)] = (string?)null,
        };

        // ReSharper disable MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025
#pragma warning disable format
        public bool IsRunning           { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(RunOutput.IsRunning = RunSettingsViewModel.IsRunning = value); OnPropertyChanged(); } }
        public int Progress             { get => GetValue<int>();           set { if (GetValue<int>()           == value) { return; } SetValue(Math.Min(100, Math.Max(0, value)));                            OnPropertyChanged(); } }
        public bool NewEnabled          { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
        public bool OpenEnabled         { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
        public bool SaveEnabled         { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
        public bool WordWrapEnabled     { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
        public string? LastEventText    { get => GetValue<string?>();       set { if (GetValue<string?>()       == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
        public string? LastAltEventText { get => GetValue<string?>();       set { if (GetValue<string?>()       == value) { return; } SetValue(value);                                                        OnPropertyChanged(); } }
#pragma warning restore format
        // ReSharper restore MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025

        #region Private Fields
        // ReSharper disable once StyleCop.SA1000 // Doesn't understand C# 9 implied new
        private readonly RunSettingsViewModel runSettingsViewModel = new();

        // ReSharper disable once StyleCop.SA1000 // Doesn't understand C# 9 implied new
        private readonly RunOutputViewModel runOutput = new();
        #endregion

        #region Public Properties
        public RunSettingsViewModel RunSettingsViewModel
        {
            get => runSettingsViewModel;
            protected init
            {
                if (runSettingsViewModel == value)
                {
                    return;
                }
                runSettingsViewModel = value;
                OnPropertyChanged();
            }
        }

        public RunOutputViewModel RunOutput
        {
            get => runOutput;
            protected init
            {
                if (runOutput == value)
                {
                    return;
                }
                runOutput = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
