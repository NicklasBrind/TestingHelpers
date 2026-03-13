namespace MassTransit.Testing.AwesomeAssertions.Test;

public class HavePublishedCountTests
{
    [Fact]
    public async Task Correct_count_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should().HavePublishedCount<OrderCreated>(2);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Wrong_count_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should().HavePublishedCount<OrderCreated>(2);

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected 2 published message*OrderCreated*but found 1*");
    }

    [Fact]
    public void Zero_count_when_none_published_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().HavePublishedCount<OrderCreated>(0);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Only_counts_matching_type()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCancelled(1));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should().HavePublishedCount<OrderCreated>(2);

        action.Should().NotThrow();
    }
}
