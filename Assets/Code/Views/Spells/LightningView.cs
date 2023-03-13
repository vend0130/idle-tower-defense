using System.Collections.Generic;
using System.Threading;
using Code.Game.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Views.Spells
{
    public class LightningView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private void OnDestroy()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }

        public void CastLightning(Vector2 origin, List<EnemyView> enemyViews, int delay, float damage)
        {
            _lineRenderer.positionCount = enemyViews.Count + 1;
            _lineRenderer.SetPosition(0, origin);

            for (int i = 0; i < enemyViews.Count; i++)
            {
                _lineRenderer.SetPosition(i + 1, enemyViews[i].EnemyTransform.position);
                enemyViews[i].Health.TakeDamage(damage);
            }

            DelayToHide(delay).Forget();
        }

        private async UniTask DelayToHide(int delay)
        {
            await UniTask.Delay(delay, cancellationToken: _cancellationToken.Token);
            _lineRenderer.positionCount = 0;
        }
    }
}