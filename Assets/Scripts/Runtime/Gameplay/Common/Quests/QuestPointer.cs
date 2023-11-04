using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests
{
  public class QuestPointer : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _minDistanceToShowArrow = 5f;
    private IQuestStateMachine _questStateMachine;

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine)
    {
      _questStateMachine = questStateMachine;
    }

    private void Update()
    {
      if (_questStateMachine.CurrentState == null)
        return;

      Vector3 delta = (Vector3)_questStateMachine.CurrentState.GetPosition() - transform.position;
      _spriteRenderer.enabled = !(delta.magnitude < _minDistanceToShowArrow);
      Vector3 direction = delta.normalized;
      transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, direction), Vector3.forward);
    }
  }
}