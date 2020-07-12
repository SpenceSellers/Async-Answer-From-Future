namespace TimeTravel
{
    public class AnswerFromFuture
    {
        public CapturedContinuation Captured;
        public AnswerFromFutureAwaiter GetAwaiter()
        {
            return new AnswerFromFutureAwaiter(this);
        }
        
        public void Nope()
        {
            // Reset back to the captured program state.
            Captured.Execute();
        }
    }
}