namespace EventBusExample
{
    public delegate void PublisherEventHandler(string publisherName, string message);

    public class EventBus
    {
        private static readonly Lazy<EventBus> instance = new Lazy<EventBus>(() => new EventBus());
        private Dictionary<string, PublisherEventHandler> publisherEvents = new Dictionary<string, PublisherEventHandler>();

        private EventBus() { }

        public static EventBus Instance => instance.Value;

        public void Subscribe(string publisherName, PublisherEventHandler handler)
        {
            if (publisherEvents.ContainsKey(publisherName))
            {
                publisherEvents[publisherName] += handler;
            }
            else
            {
                publisherEvents[publisherName] = handler;
            }
        }

        public void Unsubscribe(string publisherName, PublisherEventHandler handler)
        {
            if (publisherEvents.ContainsKey(publisherName))
            {
                publisherEvents[publisherName] -= handler;
                if (publisherEvents[publisherName] == null)
                {
                    publisherEvents.Remove(publisherName);
                }
            }
        }

        public void Post(string publisherName, string message)
        {
            if (publisherEvents.ContainsKey(publisherName))
            {
                publisherEvents[publisherName]?.Invoke(publisherName, message);
            }
        }
    }
}
