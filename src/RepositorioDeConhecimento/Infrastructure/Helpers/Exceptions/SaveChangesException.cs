namespace RepositorioDeConhecimento.Infrastructure.Helpers.Exceptions
{
    public class SaveChangesException : Exception
    {
        public SaveChangesException(string message) : base(message)
        {
        }

        public SaveChangesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public int EntityId { get; set; } = 0;

        public string Entity { get; set; } = string.Empty;
    }
}
