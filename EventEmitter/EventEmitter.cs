namespace EventEmitter
{

    public class EventEmitter : IEventEmitter
    {        
        private Dictionary<string, Action<object[]>> events;

        private Dictionary<string, Action<object[]>> eventsRunOnce;

        public EventEmitter()
        {
            events = new Dictionary<string, Action<object[]>>();
            eventsRunOnce = new Dictionary<string, Action<object[]>>();
        }

        public void AddEventListener(string eventName, Action<object[]> _event)
        {
            On(eventName, _event);
        }


        public void RemoveEventListener(string eventName, Action<object[]> _event)
        {
            Off(eventName, _event);
        }


        public void RemoveAllListeners(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                events.Remove(eventName);
            }
        }


        public void On(string eventName, Action<object[]> _event)
        {
            if (events.ContainsKey(eventName))
            {                
                events[eventName] +=  _event;
            }
            else
            {
                events.Add(eventName, _event);
            }
        }



        public void Off(string eventName, Action<object[]> _event)
        {
            OffAux(events, eventName, _event);
            OffAux(eventsRunOnce, eventName, _event);
        }


        private void OffAux(Dictionary<string, Action<object[]>> eventsAux, string eventName, Action<object[]> _event)
        {
            if (eventsAux.ContainsKey(eventName))
            {
                eventsAux[eventName] -= _event;

                if (eventsAux[eventName] is null)
                {
                    eventsAux.Remove(eventName);
                }
            }
        }



        public void Once(string eventName, Action<object[]> _event) 
        {
            if (eventsRunOnce.ContainsKey(eventName))
            {
                eventsRunOnce[eventName] += _event;
            }
            else
            {
                eventsRunOnce.Add(eventName, _event);
            }
        }


        public void Emit(string eventName, object[] parameters)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName](parameters);
            }

            if (eventsRunOnce.ContainsKey(eventName))
            {
                eventsRunOnce[eventName](parameters);                
                OffAux(eventsRunOnce, eventName, eventsRunOnce[eventName]);
            }
        }


    }
}
