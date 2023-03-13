using Code.infrastructure.StateMachine;
using Code.infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Game.UI
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private Button _againButton;
        [SerializeField] private string _nextSceneName = "Level";

        [Inject]
        public void Constructor(IStateMachine stateMachine) =>
            _againButton.onClick.AddListener(() => stateMachine.Enter<LoadLevelState, string>(_nextSceneName));
    }
}