using Code.infrastructure.StateMachine.States;

namespace Code.infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}