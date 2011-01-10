using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Andora.BlogSamples.UserControlLibrary.WeakEvents
{
    public class ValueChangedWeakEventSource : WeakEventSourceBase<IValueChanged>
    {
        protected override WeakEventListenerBase CreateWeakEventListener(IValueChanged eventObject)
        {
            var weakListener = new WeakEventListener<ValueChangedWeakEventSource,
                                                     IValueChanged,
                                                     ValueChangedEventArgs>(this, eventObject);
            weakListener.OnDetachAction = (listener, source) =>
            {
                source.ValueChanged -= listener.OnEvent;
            };
            weakListener.OnEventAction = (instance, source, e) =>
            {
                // fire event
                if (instance.ValueChanged != null)
                    instance.ValueChanged(source, e);
            };
            eventObject.ValueChanged += weakListener.OnEvent;

            return weakListener;
        }

        public event ValueChangedEventHandler ValueChanged;
    }

    public interface IValueChanged
    {
        event ValueChangedEventHandler ValueChanged;
    }
    public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);
    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs()
        {

        }
    }
}
