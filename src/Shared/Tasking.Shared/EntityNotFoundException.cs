namespace Tasking.Shared
{
    public sealed class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string code, string message) 
            : base(code, message)
        {
        }
    }
}
