namespace EventBusExample
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBus eventBus = EventBus.Instance;

            Publisher publisher1 = new Publisher("Publisher 1");
            Publisher publisher2 = new Publisher("Publisher 2");

            Subscriber subscriber1 = new Subscriber("Subscriber 1");
            Subscriber subscriber2 = new Subscriber("Subscriber 2");

            eventBus.Subscribe(publisher1.Name, subscriber1.OnEventReceived);
            eventBus.Subscribe(publisher1.Name, subscriber2.OnEventReceived);
            eventBus.Subscribe(publisher2.Name, subscriber2.OnEventReceived);

            publisher1.PublishEvent("Event from Publisher 1");

            publisher2.PublishEvent("Event from Publisher 2");

            eventBus.Unsubscribe(publisher1.Name, subscriber2.OnEventReceived);

            publisher1.PublishEvent("Another event from Publisher 1");
        }
    }
}
