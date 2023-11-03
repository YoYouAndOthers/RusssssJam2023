using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  [RequireComponent(typeof(Collider2D))]
  public class Fist : MonoBehaviour
  {
    [SerializeField] private float _speed;
    
    private IDamageMaker _damageMaker;
    private Collider2D _collider;
    private Vector3 _initialPosition;

    public void Initialize(int damagableLayers)
    {
      _initialPosition = transform.localPosition;
      _collider = GetComponent<Collider2D>();
      _collider.enabled = false;
      _collider.isTrigger = true;
      _collider.callbackLayers = new LayerMask { value = damagableLayers };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out IDamagable damageable))
      {
        _damageMaker.TryApply(damageable);
      }
    }

    public async UniTask MakeHit(Vector2 direction, float reach, IDamageMaker damageMaker)
    {
      _damageMaker = damageMaker;
      _collider.enabled = true;
      Vector2 directionNormalized = direction.normalized;
      await MoveFist(directionNormalized, reach + transform.localPosition.magnitude);
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

    private IEnumerator MoveFist(Vector2 directionNormalized, float reach)
    {
      float distance = reach;
      while (distance > 0)
      {
        float deltaTime = Time.deltaTime;
        yield return null;
        distance -= deltaTime * _speed;
        transform.Translate(directionNormalized * deltaTime * _speed);
      }
    }
  }
}