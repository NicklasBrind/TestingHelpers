using AwesomeAssertions;
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;

namespace MassTransit.Testing.AwesomeAssertions;

public class FakePublishEndpointAssertions : ReferenceTypeAssertions<FakePublishEndpoint, FakePublishEndpointAssertions>
{
    public FakePublishEndpointAssertions(FakePublishEndpoint subject, AssertionChain chain)
        : base(subject, chain)
    {
    }

    protected override string Identifier => nameof(FakePublishEndpoint);

    public AndConstraint<FakePublishEndpointAssertions> HavePublished<T>(string because = "", params object[] becauseArgs) where T : class
    {
        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.PublishedMessages.OfType<T>())
            .ForCondition(messages => messages.Any())
            .FailWith("Expected a published message of type {0}{reason}, but found none. Published message types: {1}",
                typeof(T).Name,
                Subject.PublishedMessages.Select(m => m.GetType().Name));

        return new AndConstraint<FakePublishEndpointAssertions>(this);
    }

    public AndConstraint<FakePublishEndpointAssertions> NotHavePublished<T>(string because = "", params object[] becauseArgs) where T : class
    {
        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.PublishedMessages.OfType<T>())
            .ForCondition(messages => !messages.Any())
            .FailWith("Expected no published message of type {0}{reason}, but found {1}",
                typeof(T).Name,
                Subject.PublishedMessages.OfType<T>().Count());

        return new AndConstraint<FakePublishEndpointAssertions>(this);
    }

    public AndConstraint<FakePublishEndpointAssertions> HavePublishedCount<T>(int expectedCount, string because = "", params object[] becauseArgs) where T : class
    {
        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.PublishedMessages.OfType<T>().Count())
            .ForCondition(count => count == expectedCount)
            .FailWith("Expected {0} published message(s) of type {1}{reason}, but found {2}",
                expectedCount,
                typeof(T).Name,
                Subject.PublishedMessages.OfType<T>().Count());

        return new AndConstraint<FakePublishEndpointAssertions>(this);
    }

    public AndWhichConstraint<FakePublishEndpointAssertions, T> HavePublishedSingle<T>(string because = "", params object[] becauseArgs) where T : class
    {
        var messages = Subject.PublishedMessages.OfType<T>().ToList();

        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => messages)
            .ForCondition(m => m.Count == 1)
            .FailWith("Expected exactly 1 published message of type {0}{reason}, but found {1}",
                typeof(T).Name,
                messages.Count);

        return new AndWhichConstraint<FakePublishEndpointAssertions, T>(this, messages.SingleOrDefault()!);
    }

    public AndWhichConstraint<FakePublishEndpointAssertions, T> HaveLastPublished<T>(string because = "", params object[] becauseArgs) where T : class
    {
        var messages = Subject.PublishedMessages.OfType<T>().ToList();

        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => messages)
            .ForCondition(m => m.Count > 0)
            .FailWith("Expected a published message of type {0}{reason}, but found none",
                typeof(T).Name);

        return new AndWhichConstraint<FakePublishEndpointAssertions, T>(this, messages.Last());
    }
}