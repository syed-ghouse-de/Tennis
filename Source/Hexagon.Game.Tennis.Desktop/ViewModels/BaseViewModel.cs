using Hexagon.Game.Framework.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.ViewModels
{
    /// <summary>
    /// Abstract base class for view model
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private sealed class SuspendedNotifications : IDisposable
        {
            private readonly BaseViewModel _target;
            private readonly HashSet<string> _properties = new HashSet<string>();
            private int _refCount;

            public SuspendedNotifications(BaseViewModel target)
            {
                _target = target;
            }

            public void Add(string propertyName)
            {
                _properties.Add(propertyName);
            }

            public IDisposable AddRef()
            {
                ++_refCount;
                return Disposable.Create(() =>
                {
                    if (--_refCount == 0)
                    {
                        Dispose();
                    }
                });
            }

            public void Dispose()
            {
                _target._suspendedNotifications = null;
                _properties.ForEach(x => _target.OnPropertyChanged(x));
            }
        }

        private static readonly PropertyChangedEventArgs EmptyChangeArgs = new PropertyChangedEventArgs(string.Empty);
        private static readonly IDictionary<string, PropertyChangedEventArgs> ChangedProperties = new Dictionary<string, PropertyChangedEventArgs>();

        private SuspendedNotifications _suspendedNotifications;

        public IDisposable SuspendNotifications()
        {
            if (_suspendedNotifications == null)
            {
                _suspendedNotifications = new SuspendedNotifications(this);
            }

            return _suspendedNotifications.AddRef();
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            OnPropertyChanged(ExpressionHelperName(expression));
        }

        public static string ExpressionHelperName<T>(Expression<Func<T>> expression)
        {
            var lambda = expression as LambdaExpression;
            var memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member.Name;
        }
        protected virtual void OnPropertyChanged()
        {
            OnPropertyChanged(null);
        }

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            RaisePropertyChanged(propertyName);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_suspendedNotifications != null)
            {
                _suspendedNotifications.Add(propertyName);
            }
            else
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    if (propertyName == null)
                    {
                        handler(this, EmptyChangeArgs);
                    }
                    else
                    {
                        PropertyChangedEventArgs args;
                        if (!ChangedProperties.TryGetValue(propertyName, out args))
                        {
                            args = new PropertyChangedEventArgs(propertyName);
                            ChangedProperties.Add(propertyName, args);
                        }

                        handler(this, args);
                    }
                }
            }
        }

        protected virtual bool SetPropertyAndNotify<T>(ref T existingValue, T newValue, Expression<Func<T>> expression)
        {
            if (EqualityComparer<T>.Default.Equals(existingValue, newValue))
            {
                return false;
            }

            existingValue = newValue;
            OnPropertyChanged(expression);

            return true;
        }
    }
}
