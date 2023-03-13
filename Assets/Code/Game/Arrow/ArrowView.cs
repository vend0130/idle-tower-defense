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

        private CancellationTokenSource _cancellationToken;

        private IArrowPoolable _pool;
        private Transform _attacker;
        private float _damage;

        private void Update() =>
            _current.Translate(Vector2.up * _speed * Time.deltaTime);

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IHealth health) && _attacker.transform != health.Current)
            {
                health.TakeDamage(_damage, bloodlust: true);
                UnSpawn();
            }
        }

        private void OnDestroy() =>
            DisposeToken();

        public void InitPool(IArrowPoolable pool) =>
            _pool = pool;

        public void Activate(Transform attacker, float damege, float speed)
        {
            _cancellationToken = new CancellationTokenSource();
            _attacker = attacker;
            _damage = damege;
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
            if (!gameObject.activeSelf)
                return;

            DisposeToken();
            gameObject.SetActive(false);
            _pool.UnSpawn(this);
        }

        private void DisposeToken()
        {
            if (_cancellationToken == null)
                return;

            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
            _cancellationToken = null;
        }
    }
}