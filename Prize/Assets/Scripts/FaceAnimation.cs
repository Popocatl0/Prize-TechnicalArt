using System.Collections;
using UnityEngine;

public class FaceAnimation : IAnimable
{
    [Header("Eye Settings")]
    [SerializeField] Material EyesMat;
    [Tooltip("Size of the cell that contains the Eye Texture, inside the material.")]
    [SerializeField] Vector2 EyesGrid;
    [Tooltip("Coordinates for the Eye Texture, start from (0,0) from UpLeft. Each texture coordinates are in Int")]
    [SerializeField] Vector2 EyesTarget;

    [Header("Brows Settings")]
    [SerializeField] Material BrowsMat;
    [Tooltip("Size of the cell that contains the Brows Texture, inside the material.")]
    [SerializeField] Vector2 BrowsGrid;
    
    [Tooltip("Coordinates for the Brows Texture, start from (0,0) from UpLeft. Each texture coordinates are in Int")]
    [SerializeField] Vector2 BrowsTarget;

    [Header("Mouth Settings")]
    [SerializeField] Material MouthMat;
    [Tooltip("Size of the cell that contains the Mouth Texture, inside the material.")]
    [SerializeField] Vector2 MouthGrid;
    
    [Tooltip("Coordinates for the Mouth Texture, start from (0,0) from UpLeft. Each texture coordinates are in Int")]
    [SerializeField] Vector2 MouthTarget;

    public override void Init(){}

    public override void Reset(){
        EyesMat.SetTextureOffset("_MainTex", Vector2.zero);
        BrowsMat.SetTextureOffset("_MainTex", Vector2.zero);
        MouthMat.SetTextureOffset("_MainTex", Vector2.zero);
    }
    /// <summary>
    /// Change the face expresion for a certain time, then return to the default expresion
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Animation(){
        EyesMat.SetTextureOffset("_MainTex", new Vector2( EyesGrid.x * EyesTarget.x, EyesGrid.y * -EyesTarget.y));
        BrowsMat.SetTextureOffset("_MainTex", new Vector2( BrowsGrid.x * BrowsTarget.x, BrowsGrid.y * -BrowsTarget.y));
        MouthMat.SetTextureOffset("_MainTex", new Vector2( MouthGrid.x * MouthTarget.x, MouthGrid.y * -MouthTarget.y));
        yield return new WaitForSeconds(AnimationTime);
        EyesMat.SetTextureOffset("_MainTex", Vector2.zero);
        BrowsMat.SetTextureOffset("_MainTex", Vector2.zero);
        MouthMat.SetTextureOffset("_MainTex", Vector2.zero);
    }
}
