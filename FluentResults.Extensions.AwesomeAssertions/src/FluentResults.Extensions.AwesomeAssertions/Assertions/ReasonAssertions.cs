using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;
using System;

namespace FluentResults.Extensions.AwesomeAssertions
{
    public class ReasonAssertions : ReferenceTypeAssertions<IReason, ReasonAssertions>
    {
        public ReasonAssertions(IReason subject, AssertionChain chain)
            : base(subject, chain)
        { }

        protected override string Identifier => nameof(IReason);

        public AndWhichConstraint<ReasonAssertions, IReason> HaveMetadata(string metadataKey, object metadataValue, string because = "", params object[] becauseArgs)
        {
            AssertionChain.GetOrCreate()
                   .BecauseOf(because, becauseArgs)
                   .Given(() => Subject.Metadata)
                   .ForCondition(metadata =>
                                 {
                                     metadata.TryGetValue(metadataKey, out var actualMetadataValue);
                                     return Equals(actualMetadataValue, metadataValue);
                                 })
                   .FailWith($"Reason should contain '{metadataKey}' with '{metadataValue}', but not contain it");

            return new AndWhichConstraint<ReasonAssertions, IReason>(this, Subject);
        }

        public new AndWhichConstraint<ReasonAssertions, IReason> Satisfy<TReason>(Action<TReason> action) where TReason : class, IReason
        {
            var specificReason = Subject as TReason;

            AssertionChain.GetOrCreate()
                   .Given(() => Subject)
                   .ForCondition(reason => reason is TReason)
                   .FailWith($"Reason should be of type '{typeof(TReason)}', but is of type '{Subject.GetType()}'");

            action(specificReason);

            return new AndWhichConstraint<ReasonAssertions, IReason>(this, Subject);
        }
    }
}