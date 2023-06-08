using Tasking.Shared;

namespace Tasking.Tasks.Aggregates.TaskAggregate
{
    public sealed class DueDate : ValueObject
    {
        public DateTime Value { get; }

        private DueDate(DateTime value)
            => Value = value;

        public static DueDate Create(DateTime value)
        {
            if (value < DateTime.Now)
                throw Errors.TaskAggregateErrors.DueDateLessThanCurrent;

            var dueDate = new DueDate(value);

            return dueDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
