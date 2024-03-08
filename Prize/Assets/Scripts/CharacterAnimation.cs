using System.Collections;
using UnityEngine;

public class CharacterAnimation : IAnimable
{
    [SerializeField] Transform Character;
    [SerializeField] Transform DollarImg;
    [SerializeField] Transform StartCharPos;
    [SerializeField] Transform EndCharPos;
    public override void Init(){}

    public override void Reset(){
        Character.localPosition = StartCharPos.localPosition;
        DollarImg.localPosition = Vector3.zero;
    }
    /// <summary>
    /// The DollarImg go down, meanwhile the Character go up
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Animation(){
        Vector3 Transition = EndCharPos.localPosition - StartCharPos.localPosition;
        while(Character.localPosition.y < EndCharPos.localPosition.y){
            yield return null;
            Character.localPosition += Transition * (Time.deltaTime/AnimationTime);
            DollarImg.localPosition += Vector3.down * 1.5f * (Time.deltaTime/AnimationTime);
        }
    }
}
