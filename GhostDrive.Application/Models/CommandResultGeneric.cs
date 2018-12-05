namespace GhostDrive.Application.Models
{
    public class CommandResult<T>
    {
        private CommandResult(string failureReason)
        {
            FailureReason = failureReason;
        }

        private CommandResult(T result)
        {
            Result = result;
        }

        public T Result { get; }

        public string FailureReason { get; }

        public bool IsSuccess => string.IsNullOrEmpty(FailureReason);

        public static CommandResult<T> Success(T result)
        {
            return new CommandResult<T>(result);
        }

        public static CommandResult<T> Fail(string reason)
        {
            return new CommandResult<T>(reason);
        }

        public static implicit operator bool(CommandResult<T> result)
        {
            return result.IsSuccess;
        }
    }
}
