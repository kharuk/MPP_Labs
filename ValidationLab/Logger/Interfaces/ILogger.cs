namespace Logger.Interfaces
{
    /// <summary>
    /// This class is needed to logging validation errors.
    /// </summary>
    public interface ILogger
    {
        void Info(string message);

        void Warn(string message);

        void Debug(string message);

        void Error(string message);

        void Fatal(string message);
  }
}
