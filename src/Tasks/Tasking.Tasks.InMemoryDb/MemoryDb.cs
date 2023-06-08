namespace Tasking.Tasks.InMemoryDb
{
    internal class MemoryDb
    {
        public Dictionary<Guid, Aggregates.TaskAggregate.Task> Tasks { get; }

        public MemoryDb()
        {
            Tasks = new();
        }
    }
}
