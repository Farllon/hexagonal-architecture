using Tasking.Shared;

namespace Tasking.Tasks.Aggregates.TaskAggregate
{
    public sealed class Title : ValueObject
    {
        public string Value { get; }

        public const int MinLength = 5;
        public const int MaxLength = 30;

        private Title(string value)
            => Value = value;

        public static Title Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength)
                throw Errors.TaskAggregateErrors.ShortTitle;

            if (value.Length > MaxLength)
                throw Errors.TaskAggregateErrors.LongTitle;

            var title = new Title(value);
            
            return title;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
