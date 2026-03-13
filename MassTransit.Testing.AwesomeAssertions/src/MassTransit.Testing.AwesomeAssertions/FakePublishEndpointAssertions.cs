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

    public AndWhichConstraint<FakePublishEndpointAssertions, IReadOnlyList<T>> HavePublished<T>(string because = "", params object[] becauseArgs) where T : class
    {
        var messages = Subject.PublishedMessages.OfType<T>().ToList();

        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => messages)
            .ForCondition(m => m.Count > 0)
            .FailWith("Expected a published message of type {0}{reason}, but found none. Published message types: {1}",
                typeof(T).Name,
                Subject.PublishedMessages.Select(m => m.GetType().Name));

        return new AndWhichConstraint<FakePublishEndpointAssertions, IReadOnlyList<T>>(this, messages);
    }

    public AndConstraint<FakePublishEndpointAssertions> BeEmpty(string because = "", params object[] becauseArgs)
    {
        AssertionChain.GetOrCreate()
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.PublishedMessages)
            .ForCondition(messages => messages.Count == 0)
            .FailWith("Expected no published messages{reason}, but found {0}: {1}",
                Subject.PublishedMessages.Count,
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
}