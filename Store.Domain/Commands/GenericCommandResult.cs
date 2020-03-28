namespace Store.Domain.Commands.Interfaces
{
    public class GenericCommandResult : ICommandResult
    {
        // Properties
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GenericCommandResult(bool success, string message, object data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
    }
}