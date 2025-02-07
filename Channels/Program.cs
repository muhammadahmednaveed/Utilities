using System.Collections.Concurrent;

namespace Channels
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var channel = new Channel<string>();
            Task.WaitAll(Consumer(channel), Producer(channel, "a"));
        }

        public static async Task Producer(IWrite<string> writer, string id)
        {
            for (int i = 0; i < 100; i++)
            {
                writer.Push(i.ToString());
                Console.WriteLine($"{id} pushed {i}");
                await Task.Delay(1000);
            }
            writer.Complete();
        }

        public static async Task Consumer(IRead<string> reader)
        {
            while (!reader.IsComplete())
            {
                var msg = await reader.Read();
                Console.WriteLine($"Msg: {msg}");
                await Task.Delay(500);
            }
        }
    }
    public interface IRead<T>
    {
        Task<T> Read();
        bool IsComplete();
    }

    public interface IWrite<T>
    {
        void Push(T msg);
        void Complete();
    }
    public class Channel<T> : IRead<T>, IWrite<T>
    {
        private bool _finished;
        private readonly ConcurrentQueue<T> _queue;
        private readonly SemaphoreSlim _flag;

        public Channel()
        {
            _queue = new ConcurrentQueue<T>();
            _flag = new SemaphoreSlim(0);
        }

        public void Push(T msg)
        {
            _queue.Enqueue(msg);
            _flag.Release();
        }

        public async Task<T> Read()
        {
            await _flag.WaitAsync();
            _queue.TryDequeue(out var msg);
            return msg;
        }

        public void Complete()
        {
            _finished = true;
        }

        public bool IsComplete()
        {
            return _finished && _queue.IsEmpty;
        }
    }
}
