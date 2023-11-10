namespace SiteManager.ViewModels
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Documents;
    using Events;

    public class RunOutputViewModel : ViewModelBase
    {
        protected override ConcurrentDictionary<string, object?> Values { get; } = new()
        {
            [nameof(Progress)] = 0,
            [nameof(IsRunning)] = false,
            [nameof(Output)] = string.Empty,
            [nameof(Document)] = (FlowDocument?)null,
        };

        // ReSharper disable MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025
#pragma warning disable format
        public bool IsRunning         { get => GetValue<bool>();          set { if (GetValue<bool>()          == value) { return; } SetValue(value);                             OnPropertyChanged(); IsRunningChanged?.Invoke(this, new(value));                    } }
        public int Progress           { get => GetValue<int>();           set { if (GetValue<int>()           == value) { return; } SetValue(Math.Min(100, Math.Max(0, value))); OnPropertyChanged(); ProgressChanged?.Invoke(this, new(GetValue<int>(), null));     } }
        public string? Output         { get => GetValue<string?>();       set { if (GetValue<string?>()       == value) { return; } SetValue(value);                             OnPropertyChanged(); OutputChanged?.Invoke(this, new(GetValue<string?>()));         } }
        public FlowDocument? Document { get => GetValue<FlowDocument?>(); set { if (GetValue<FlowDocument?>() == value) { return; } SetValue(value);                             OnPropertyChanged(); DocumentChanged?.Invoke(this, new(GetValue<FlowDocument?>())); } }
#pragma warning restore format
        // ReSharper restore MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025

        public event IsRunningChangedEventHandler? IsRunningChanged;

        public event ProgressChangedEventHandler? ProgressChanged;

        public event OutputChangedEventHandler? OutputChanged;

        public event DocumentChangedEventHandler? DocumentChanged;

        public event EventHandler? Started;

        public event EventHandler? Finished;

        public event CancelEventHandler? Cancelled;

        public Task StartAsync(RunSettingsViewModel runSettings, CancellationToken cancellationToken)
        {
            // Force to a separate thread;
            return Task.Run(
                async () =>
                {
                    try
                    {
                        // Set up
                        QuickSetup.QuickSetup.ProgressChanged += QuickSetup_ProgressChanged;
                        QuickSetup.QuickSetup.DocumentChanged += QuickSetup_DocumentChanged;
                        QuickSetup.QuickSetup.OutputChanged += QuickSetup_OutputChanged;
                        Started?.Invoke(this, EventArgs.Empty);
                        IsRunning = true;
                        Progress = 0;
                        // Do
                        await QuickSetup.QuickSetup.RunAsync(runSettings, cancellationToken).ConfigureAwait(false);
                        // Tear down
                        Progress = 100;
                        IsRunning = false;
                        Finished?.Invoke(this, EventArgs.Empty);
                        QuickSetup.QuickSetup.OutputChanged -= QuickSetup_OutputChanged;
                        QuickSetup.QuickSetup.DocumentChanged -= QuickSetup_DocumentChanged;
                        QuickSetup.QuickSetup.ProgressChanged -= QuickSetup_ProgressChanged;
                    }
                    catch (OperationCanceledException)
                    {
                        IsRunning = false;
                        Cancelled?.Invoke(this, new(true));
                    }
                },
                cancellationToken);
        }

        private void QuickSetup_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void QuickSetup_DocumentChanged(object? sender, DocumentChangedEventArgs e)
        {
            Document = e.Document;
        }

        private void QuickSetup_OutputChanged(object? sender, OutputChangedEventArgs e)
        {
            Output = e.Output;
        }
    }
}
