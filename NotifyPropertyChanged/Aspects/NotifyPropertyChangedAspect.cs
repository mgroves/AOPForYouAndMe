using System;
using System.ComponentModel;
using System.Reflection;
using PostSharp.Aspects;

namespace NotifyPropertyChanged.Aspects
{
    [Serializable]
    public class NotifyPropertyChangedAspect : LocationInterceptionAspect
    {
        readonly string[] _derivedProperties;

        public NotifyPropertyChangedAspect(params string[] derived)
        {
            _derivedProperties = derived;
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            var oldValue = args.GetCurrentValue();
            var newValue = args.Value;
            if (oldValue != newValue)
            {
                args.ProceedSetValue();
                RaisePropertyChanged(args.Instance, args.LocationName);
                if(_derivedProperties != null)
                    foreach (var derivedProperty in _derivedProperties)
                        RaisePropertyChanged(args.Instance, derivedProperty);
            }
        }

        private void RaisePropertyChanged(object instance, string propertyName)
        {
            var type = instance.GetType();
            var propertyChanged = type.GetField("PropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            var handler = propertyChanged.GetValue(instance) as PropertyChangedEventHandler;
            handler(instance, new PropertyChangedEventArgs(propertyName));
        }
    }
}