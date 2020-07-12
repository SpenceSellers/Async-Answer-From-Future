using System;
using System.Runtime.CompilerServices;

namespace TimeTravel
{
    // AnswerFromFutureAwaiter has to be a struct, because after it's "done", the async runtime is going to
    // replace it with default(AutoAsyncAwaiter). We're going to force C# to re-use the awaiter,
    // so it needs to have a default value that's more useful than "null".
    public struct AnswerFromFutureAwaiter : INotifyCompletion
    {
        private static int _nextResult;
        private readonly AnswerFromFuture _answerFromFuture;

        public AnswerFromFutureAwaiter(AnswerFromFuture c)
        {
            _answerFromFuture = c;
        }

        public void OnCompleted(Action continuation)
        {
            _answerFromFuture.Captured = new CapturedContinuation(continuation);
            continuation.Invoke();
        }

        public bool IsCompleted => false;

        public int GetResult()
        {
            return _nextResult++;
        }
    }
}