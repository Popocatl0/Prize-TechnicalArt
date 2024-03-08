using System.Collections;
using UnityEngine;

public class BannerPopUp : IAnimable
{
    float startSize;
    public override void Init() {
        startSize = transform.localScale.x;
    }

    public override void Reset(){
        transform.localScale = Vector3.zero;
    }
    /// <summary>
    /// Simple Pop up animation. The scale go from 0 to the startSize
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Animation(){
        while(transform.localScale.x < startSize){
            yield return null;
            transform.localScale += Vector3.one * startSize * (Time.deltaTime/AnimationTime);
        }
        transform.localScale = Vector3.one * startSize;
    }
}
