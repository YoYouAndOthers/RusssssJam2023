using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterViewController : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem fxDashDust;
    public string triggerKey_DashIn;
    public string triggerKey_DashFly;
    public string triggerKey_Run;
    public string triggerKey_Idle;
    public string DirectionKey_X;
    public string DirectionKey_Y;
    public float dashDelay = 0.3f;
    public float dashDuration = 0.3f;
    public float dashEndTime = 0.3f;

    private AnimationState currentState;
    
    public enum AnimationState
    {
        Idle, Run, Dash
    }

    public void PlayAnimation(AnimationState animationState, Vector3 worldDir)
    {
        StopAllCoroutines();
        Vector2 dir = worldDir;
        animator.SetFloat(DirectionKey_X, dir.x);
        animator.SetFloat(DirectionKey_Y, dir.y);
        
        if (animationState != currentState)
        {
            switch (animationState)
            {
                case AnimationState.Idle:
                    animator.SetTrigger(triggerKey_Idle);
                    break;
                case AnimationState.Run:
                    animator.SetTrigger(triggerKey_Run);
                    break;
                case AnimationState.Dash:
                
                    fxDashDust.transform.right = -dir;
                    fxDashDust.Play();
                
                    animator.SetTrigger(triggerKey_DashIn); // Приказали стартануть
                    StartCoroutine(DashIn(dashDelay));  
                
                    IEnumerator DashIn(float time)
                    {
                        yield return new WaitForSeconds(time);
                        animator.SetTrigger(triggerKey_DashFly); // Приказали перейти в полет
                        StartCoroutine(DashFly(dashDuration));
                    }
                
                    IEnumerator DashFly(float time)
                    {
                        yield return new WaitForSeconds(time);
                        animator.SetTrigger(triggerKey_DashIn); // Приказали перейти приземление
                        StartCoroutine(DashEnd(dashEndTime));
                    }
                
                    IEnumerator DashEnd(float time)
                    {
                        yield return new WaitForSeconds(time);
                        animator.SetTrigger(triggerKey_Idle); // Приказали перейти в айдл
                    }
                    break;
            }
            currentState = animationState;
        }
    }
}
