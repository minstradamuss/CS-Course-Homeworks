namespace EventBusExample
{
    public class Publisher
    {
        private string name;

        public Publisher(string name_)
        {
            name = name_;
        }

        public void PublishEvent(string message)
        {
            EventBus.Instance.Post(name, message);
        }

        public string Name => name;
    }
}
