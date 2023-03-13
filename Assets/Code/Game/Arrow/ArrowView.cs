using System.Threading;
using Code.Factories.Arrow;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Game.Arrow
{
    public class ArrowView : MonoBehaviour
    {
        [SerializeField] private Transform _current;
        [SerializeField] private float _speed;

        private const int TimeMove = 3000;
        
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        
        private IArrowPoolable _pool;

        private void Update() => 
            _current.Translate(Vector2.up * _speed  * Time.deltaTime);

        private void OnDestroy()
        {
            if(_cancellationToken == null)
                return;
            
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }

        public void InitPool(IArrowPoolable pool) => 
            _pool = pool;

        public void Activate(float speed)
        {
            _speed = speed;
            gameObject.SetActive(true);
            MoveDelay().Forget();
        }

        private async UniTask MoveDelay()
        {
            await UniTask.Delay(TimeMove, cancellationToken: _cancellationToken.Token);
            UnSpawn();
        }

        private void UnSpawn()
        {
            if(!gameObject.activeSelf)
                return;
            
            gameObject.SetActive(false);
            _pool.UnSpawn(this);
        }
    }
}