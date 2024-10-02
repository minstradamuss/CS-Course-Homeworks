namespace EventBusExample
{
    public class Subscriber
    {
        private string name;

        public Subscriber(string name_)
        {
            name = name_;
        }

        public void OnEventReceived(string publisherName, string message)
        {
            Console.WriteLine("{0} received message from {1}: {2}", name, publisherName, message);
        }
    }
}
