using System;
using System.Reflection;

namespace TimeTravel
{
    public class CapturedContinuation
    {
        private readonly Action _savedContinuation;
        private readonly int _savedStateMachineState;

        public CapturedContinuation(Action continuation)
        {
            _savedContinuation = continuation;
            // Save the state of the C# async runtime state machine so we can jump back to this point later.
            var (stateMachine, stateField) = GetAsyncStateMachineInternalStateField(continuation);
            _savedStateMachineState = (int) stateField.GetValue(stateMachine);
        }

        public void Execute()
        {
            // Reset the C# async runtime state machine back to how it looked when we first hit the await
            var (stateMachine, stateField) = GetAsyncStateMachineInternalStateField(_savedContinuation);
            stateField.SetValue(stateMachine, _savedStateMachineState);

            _savedContinuation();
        }

        private static (object StateMachine, FieldInfo StateField) GetAsyncStateMachineInternalStateField(Action continuation)
        {
            var target = continuation.Target;
            var stateMachineField = target.GetType().GetField("StateMachine");
            var stateMachine = stateMachineField.GetValue(target);
            var stateField = stateMachine.GetType().GetField("<>1__state");
            return (stateMachine, stateField);
        }
    }
}