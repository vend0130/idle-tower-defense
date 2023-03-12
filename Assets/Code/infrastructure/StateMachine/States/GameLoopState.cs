using UnityEngine;

namespace Code.infrastructure.StateMachine.States
{
    public class GameLoopState : IDefaultState
    {
        public void Enter()
        {
            Debug.Log("enter");
        }

        public void Exit()
        {
        }
    }
}