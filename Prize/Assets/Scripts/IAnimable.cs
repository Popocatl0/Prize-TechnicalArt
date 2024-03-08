using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class to manage animations
/// </summary>
public abstract class IAnimable : MonoBehaviour
{
    [SerializeField] protected float  AnimationTime = 0.5f;
    [SerializeField] protected float  Delay;
    delegate void NextAnimation();
    NextAnimation nextAnimation;
    /// <summary>
    /// Initialize object parameters, position and scale.
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// Reset object parameters, position and scale.
    /// </summary>
    public abstract void Reset();
    /// <summary>
    /// Define a concret animation, that will be use in RunAnimation
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator Animation();

    /// <summary>
    /// Call the animation 
    /// </summary>
    public void RunAnimation(){
        StartCoroutine(MainAnimation());
    }
    /// <summary>
    /// Wait the Delay time, then runs the animations.
    /// If there is a next animation assigned, run it.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator MainAnimation() {
        yield return new WaitForSeconds(Delay);
        yield return Animation();
        if(nextAnimation!=null) nextAnimation();
    }
    /// <summary>
    /// Assigns an Animation that will be run when this finishes.
    /// </summary>
    /// <param name="Animable"></param>
    public void SetNextAnimation(IAnimable Animable){
       nextAnimation += Animable.RunAnimation;
    }
    /// <summary>
    /// Delete an Animation
    /// </summary>
    /// <param name="Animable"></param>
    public void DeleteNextAnimation(IAnimable Animable){
       nextAnimation -= Animable.RunAnimation;
    }
}
