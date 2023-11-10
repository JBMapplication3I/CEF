namespace SiteManager.ViewModels
{
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using SiteManager.Annotations;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected abstract ConcurrentDictionary<string, object?> Values { get; }

        protected object? GetValue([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
            {
                return null;
            }
            return Values.TryGetValue(propertyName, out var value)
                ? value
                : null;
        }

        protected T? GetValue<T>([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
            {
                return default;
            }
            if (!Values.TryGetValue(propertyName, out var value))
            {
                return default;
            }
            if (value == null)
            {
                return default;
            }
            return (T)value;
        }

        protected void SetValue(object? value, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
            {
                return;
            }
            Values[propertyName] = value;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
