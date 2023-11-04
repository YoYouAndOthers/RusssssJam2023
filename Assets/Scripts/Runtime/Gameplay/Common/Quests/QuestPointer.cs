using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests
{
  public class QuestPointer : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _minDistanceToShowArrow = 5f;
    private IQuestStateMachine _questStateMachine;
    private Vector3 _destination;

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine)
    {
      _questStateMachine = questStateMachine;
    }

    private void Awake()
    {
      _questStateMachine.CurrentState.Where( k => k is QuestWithDirectionState questWithDirectionState)
        .Subscribe(_ =>
        {
          _destination = ((QuestWithDirectionState)_questStateMachine.CurrentState.Value).GetPosition();
          _spriteRenderer.enabled = true;
          enabled = true;
        })
        .AddTo(this);
      _questStateMachine.CurrentState.Where(k => k is not QuestWithDirectionState)
        .Subscribe(_ =>
        {
          _spriteRenderer.enabled = false;
          enabled = false;
        })
        .AddTo(this);
    }

    private void Update()
    {
      Vector3 delta = _destination - transform.position;
      _spriteRenderer.enabled = !(delta.magnitude < _minDistanceToShowArrow);
      Vector3 direction = delta.normalized;
      transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, direction), Vector3.forward);
    }
  }
}