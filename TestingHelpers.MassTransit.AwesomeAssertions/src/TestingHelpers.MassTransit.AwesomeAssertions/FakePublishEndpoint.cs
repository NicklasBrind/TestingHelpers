using MassTransit;

namespace TestingHelpers.MassTransit.AwesomeAssertions;

public class FakePublishEndpoint : IPublishEndpoint
{
    private readonly List<object> _publishedMessages = [];
    public IReadOnlyList<object> PublishedMessages => _publishedMessages;

    public bool HasPublished<T>() where T : class
    {
        return _publishedMessages.Exists(m => m is T);
    }

    public T GetLastPublishedMessage<T>() where T : class
    {
        return _publishedMessages.OfType<T>().Last();
    }

    public IEnumerable<T> GetAllPublishedMessagesOfType<T>() where T : class
    {
        return _publishedMessages.OfType<T>();
    }

    public void Clear()
    {
        _publishedMessages.Clear();
    }

    public Task Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(values);
        return Task.CompletedTask;
    }

    public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(values);
        return Task.CompletedTask;
    }

    public Task Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(values);
        return Task.CompletedTask;
    }

    public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
    {
        throw new NotImplementedException();
    }

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish(object message, CancellationToken cancellationToken = default)
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default)
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
    {
        _publishedMessages.Add(message);
        return Task.CompletedTask;
    }
}