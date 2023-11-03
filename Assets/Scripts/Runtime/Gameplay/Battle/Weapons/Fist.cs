using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  [RequireComponent(typeof(Collider2D))]
  public class Fist : MonoBehaviour
  {
    [SerializeField] private float _speed;
    private CancellationTokenSource _cancellationTokenSource;
    private Collider2D _collider;

    private IDamageMaker _damageMaker;
    private Vector3 _initialPosition;
    private int _piercingCount;
    private int _piercingStat;

    private void OnDestroy()
    {
      _cancellationTokenSource?.Cancel();
      _cancellationTokenSource?.Dispose();
      _cancellationTokenSource = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!other.TryGetComponent(out IDamagable damageable))
        return;

      if (_piercingStat == -1)
      {
        _damageMaker.TryApply(damageable);
        return;
      }

      if (_piercingCount-- <= 0)
      {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = null;
      }
      else
      {
        _damageMaker.TryApply(damageable);
      }
    }

    public void Initialize(int damagableLayers, float piercingCount)
    {
      _piercingStat = (int)piercingCount;
      _initialPosition = transform.localPosition;
      _collider = GetComponent<Collider2D>();
      _collider.enabled = false;
      _collider.isTrigger = true;
      _collider.callbackLayers = new LayerMask { value = damagableLayers };
    }

    public async UniTask MakeHit(Vector2 direction, float reach, IDamageMaker damageMaker)
    {
      _piercingCount = _piercingStat;
      _cancellationTokenSource = new CancellationTokenSource();
      _damageMaker = damageMaker;
      _collider.enabled = true;
      Vector2 directionNormalized = direction.normalized;
      await MoveFist(directionNormalized, reach + transform.localPosition.magnitude, _cancellationTokenSource.Token);
      _collider.enabled = false;
      await ReturnToStart();
    }

    private IEnumerator ReturnToStart()
    {
      while ((transform.localPosition - _initialPosition).magnitude > 0.01f)
      {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition, Time.deltaTime * _speed);
        yield return null;
      }

      transform.localPosition = _initialPosition;
    }

    private IEnumerator MoveFist(Vector2 directionNormalized, float reach, CancellationToken cancellationToken)
    {
      float distance = reach;
      while (distance > 0)
      {
        if (cancellationToken.IsCancellationRequested)
          yield break;
        float deltaTime = Time.deltaTime;
        yield return null;
        distance -= deltaTime * _speed;
        transform.Translate(directionNormalized * deltaTime * _speed);
      }
    }
  }
}