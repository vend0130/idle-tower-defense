using UnityEngine;

namespace Code.infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        public void Enter(string sceneName)
        {
            //TODO
            Debug.Log(sceneName);
        }

        public void Exit()
        {
        }
    }
}