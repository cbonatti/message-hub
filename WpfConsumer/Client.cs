namespace WpfConsumer
{
    public class Client
    {
        public Client(string connectionId, string input)
        {
            ConnectionId = connectionId;
            Input = input;
        }

        public string ConnectionId { get; private set; }
        public string Input { get; private set; }

        public Client SetInput(string input)
        {
            Input = input;
            return this;
        }
    }
}
