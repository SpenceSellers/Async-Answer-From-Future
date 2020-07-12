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
            var (stateMachine, stateField) = GetAsyncStateMachineInternalStateField(continuation);
            _savedStateMachineState = (int) stateField.GetValue(stateMachine);
        }

        public void Execute()
        {
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