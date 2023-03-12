namespace Code.infrastructure.StateMachine.States
{
    public interface IState
    {
        void Exit();
    }

    public interface IPayloadState<TPayload> : IState
    {
        void Enter(TPayload payload);
    }
}