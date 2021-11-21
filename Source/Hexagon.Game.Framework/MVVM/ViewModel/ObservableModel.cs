using Hexagon.Game.Framework.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.MVVM.ViewModel
{
    /// <summary>
    /// Observable model for notifying property change
    /// </summary>
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Class to suspend noftification
        /// </summary>
        private sealed class SuspendedNotifications : IDisposable
        {
            private readonly ObservableModel _target;
            private readonly HashSet<string> _properties = new HashSet<string>();
            private int _refCount;

            /// <summary>
            /// Parameterized constructor
            /// </summary>
            /// <param name="target">Target to suspend</param>
            public SuspendedNotifications(ObservableModel target)
            {
                _target = target;
            }

            /// <summary>
            /// Add property to suspend
            /// </summary>
            /// <param name="propertyName"></param>
            public void Add(string propertyName)
            {
                // Add the property in the list
                _properties.Add(propertyName);
            }

            /// <summary>
            /// Refernece 
            /// </summary>
            /// <returns></returns>
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

            /// <summary>
            /// Dispose suspend notifictions
            /// </summary>
            public void Dispose()
            {
                _target._suspendedNotifications = null;
                _properties.ForEach(x => _target.OnPropertyChanged(x));
            }
        }

        private static readonly PropertyChangedEventArgs EmptyChangeArgs = 
            new PropertyChangedEventArgs(string.Empty);
        private static readonly IDictionary<string, PropertyChangedEventArgs> ChangedProperties = 
            new Dictionary<string, PropertyChangedEventArgs>();
        private SuspendedNotifications _suspendedNotifications;

        /// <summary>
        /// Default construtor
        /// </summary>
        public ObservableModel()
        {   
        }

        /// <summary>
        /// Method to suspend notifications
        /// </summary>
        /// <returns></returns>
        public IDisposable SuspendNotifications()
        {            
            if (_suspendedNotifications == null)      
                _suspendedNotifications = new SuspendedNotifications(this);
         
            return _suspendedNotifications.AddRef();
        }

        /// <summary>
        /// On property chaanged 
        /// </summary>
        /// <typeparam name="T">Type to change the property</typeparam>
        /// <param name="expression">Expression</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            // Call OnPropertyChanges by using func
            OnPropertyChanged(ExpressionHelperName(expression));
        }

        /// <summary>
        /// Expression to extract the name of the member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ExpressionHelperName<T>(Expression<Func<T>> expression)
        {
            var lambda = expression as LambdaExpression;
            var memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// On property changed method
        /// </summary>
        protected virtual void OnPropertyChanged()
        {
            OnPropertyChanged(null);
        }

        /// <summary>
        /// Set the property 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="val"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raise property changes event
        /// </summary>
        /// <param name="property"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            // Invoke property changed event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// On property changes event for specific property name
        /// </summary>
        /// <param name="propertyName">Property name to trigger propety changed name</param>
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

        /// <summary>
        /// Set property and nofity 
        /// </summary>
        /// <typeparam name="T">Type to set the property</typeparam>
        /// <param name="existingValue">Existing value</param>
        /// <param name="newValue">New Value</param>
        /// <param name="expression">Expression</param>
        /// <returns>Return bool</returns>
        protected virtual bool SetPropertyAndNotify<T>(
            ref T existingValue, T newValue, Expression<Func<T>> expression)
        {
            if (EqualityComparer<T>.Default.Equals(existingValue, newValue))
                return false;

            existingValue = newValue;
            OnPropertyChanged(expression);

            return true;
        }
    }
}