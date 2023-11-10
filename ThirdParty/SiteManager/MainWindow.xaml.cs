namespace SiteManager
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Threading;
    using SiteManager.Events;
    // using Microsoft.Web.Administration;
    using SiteManager.ViewModels;

    /// <content>Interaction logic for MainWindow.xaml.</content>
    public partial class MainWindow : IDisposable
    {
        private bool disposed;

        private CancellationTokenSource? cancellationTokenSource;

        private readonly Paragraph? Paragraph;

        public MainWindow()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.web.administration?redirectedfrom=MSDN&view=iis-dotnet
            InitializeComponent();
            CastedDataContext.RunOutput.Started += RunOutput_Started;
            CastedDataContext.RunOutput.IsRunningChanged += RunOutput_OnIsRunningChanged;
            CastedDataContext.RunOutput.ProgressChanged += RunOutput_ProgressChanged;
            CastedDataContext.RunOutput.OutputChanged += RunOutput_OutputChanged;
            CastedDataContext.RunOutput.DocumentChanged += RunOutput_DocumentChanged;
            CastedDataContext.RunOutput.Finished += RunOutput_Finished;
            CastedDataContext.RunOutput.Cancelled += RunOutput_Cancelled;
        }

        ~MainWindow()
        {
            Dispose(false);
        }

        private MainWindowViewModel CastedDataContext => (MainWindowViewModel)DataContext;

        private void MenuItemFileNew_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Loaded Detection
            // TODO: Dirty Detection
        }

        private void MenuItemFileOpen_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Loaded Detection
            // TODO: Dirty Detection
        }

        private void MenuItemFileSave_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Loaded Detection
            // TODO: Dirty Detection
        }

        private void MenuItemFileExit_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Dirty Detection
            Application.Current.Shutdown();
        }

        private void ButtonCEFDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonSolutionItemsDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonDataModelDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonWEBDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonPortalsDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonImagesDirectory_OnClick(object sender, RoutedEventArgs e)
        {
        }

#pragma warning disable IDE1006 // Naming Styles
        private async void MenuItemRun_OnClick(object sender, RoutedEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            CancelWork();
            try
            {
                // ReSharper disable once StyleCop.SA1000 // Doesn't understand C# 9 implied new
                cancellationTokenSource = new();
                var token = cancellationTokenSource.Token;
                await Dispatcher.InvokeAsync(
                        () => CastedDataContext.RunOutput.StartAsync(
                            runSettings: CastedDataContext.RunSettingsViewModel,
                            cancellationToken: token))
                    .Task
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CancelWork();
            }
        }

        private void MenuItemCancel_OnClick(object sender, RoutedEventArgs e)
        {
            CancelWork();
        }

        private void CancelWork()
        {
            if (cancellationTokenSource == null)
            {
                return;
            }
            cancellationTokenSource.Cancel();
            EndWork();
        }

        private void EndWork()
        {
            if (cancellationTokenSource == null)
            {
                return;
            }
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }

        private void RunOutput_DocumentChanged(object? sender, DocumentChangedEventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    // TxtOutput2.Document = e.Document;
                });
        }

        private void RunOutput_OutputChanged(object? sender, OutputChangedEventArgs e)
        {
            // TODO
        }

        private void RunOutput_OnIsRunningChanged(object? sender, IsRunningChangedEventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    CastedDataContext.IsRunning = e.IsRunning;
                });
        }

        private void RunOutput_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    CastedDataContext.Progress = e.ProgressPercentage;
                    TxtOutput.ScrollToEnd();
                });
        }

        private void RunOutput_Started(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    CastedDataContext.LastEventText = "Started detected!";
                });
        }

        private void RunOutput_Finished(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    MessageBox.Show(
                        this,
                        "Process complete!",
                        "Running Standard CEF Setup",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.OK);
                });
            EndWork();
        }

        private void RunOutput_Cancelled(object? sender, CancelEventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    CastedDataContext.LastEventText = "Cancel detected!";
                    MessageBox.Show(
                        this,
                        "Process cancelled!",
                        "Running Standard CEF Setup",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.OK);
                });
            EndWork();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                cancellationTokenSource?.Dispose();
            }
            disposed = true;
        }
        #endregion
    }
}
