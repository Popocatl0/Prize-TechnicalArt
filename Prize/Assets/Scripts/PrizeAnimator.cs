using System.Collections;
using UnityEngine;

public class PrizeAnimator : MonoBehaviour
{
    [Header("PopUp Settings")]
    [Tooltip("Duration of the initial pop up animation.")]
    [SerializeField] float  PopUpTime = 0.5f;
    [Tooltip("Duration of the pop up before disappearing.")]
    [SerializeField] float  EndTime = 2.0f;
    [SerializeField] ParticleSystem MoneyRainPrefab;

    [Space(10)]
    [Header("Sunshine")]
    [SerializeField] Transform RaysImg;
    [Tooltip("Sunshine rays rotation speed.")]
    [SerializeField] float  RaySpeed = 5;

    [Header("Side Circles")]
    [SerializeField] Transform LeftCircleImg;
    [SerializeField] Transform RightCircleImg;
    [Tooltip("Side circles rotation speed. Each cirlce rotates in different direction")]
    [SerializeField] float  CircleSpeed = 20;

    [Space(10)]
    [Header("Character Settings")]
    [Tooltip("Item that manage Character Show up and Animations")]
    [SerializeField] CharacterAnimation Character;
    [SerializeField] FaceAnimation FaceCharacter;

    [Space(10)]
    [Header("Banners Settings")]
    [Tooltip("Item that manage the upper banner pop up.")]
    [SerializeField] BannerPopUp UpperBaner;
    [Tooltip("Item that manage the lower banner pop up.")]
    [SerializeField] BannerPopUp LowerBanner;
    [Tooltip("Item that manage the Cash Win's animation.")]
    [SerializeField] CashAmount AmountText;

    ParticleSystem MoneyRain;

    private void Awake() {
        UpperBaner.Init();
        LowerBanner.Init();
        AmountText.Init();
        Character.Init();

        //Set the Cash Animation after the lower banner pop up.
        LowerBanner.SetNextAnimation(AmountText);
        //Set the Face Animation after the character show up.
        Character.SetNextAnimation(FaceCharacter);

        //The Money VFX is outside the main object for a smoother transition when disappear.
        MoneyRain = Instantiate(MoneyRainPrefab, new Vector3(0,1.8f,0), Quaternion.identity, transform.parent);
    }
    private void OnEnable() {
        Reset();
        StartCoroutine(PopUp());
    }
    void OnDisable() {
        MoneyRain.Stop();
    }
    void OnDestroy(){
        Destroy(MoneyRain);
        LowerBanner.DeleteNextAnimation(AmountText);
        Character.DeleteNextAnimation(FaceCharacter);
    }
    private void Update() {
        LoopAnimation();
    }

    /// <summary>
    /// Rotate the sunshine rays and side circles.
    /// </summary>
    void LoopAnimation(){
        RaysImg.Rotate(Vector3.forward * RaySpeed * Time.deltaTime);
        LeftCircleImg.Rotate(Vector3.back * CircleSpeed * Time.deltaTime);
        RightCircleImg.Rotate(Vector3.forward * CircleSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Reset the element position and scales. Then start the animation.
    /// </summary>
    void Reset(){
        UpperBaner.Reset();
        LowerBanner.Reset();
        AmountText.Reset();
        Character.Reset();
    }
    /// <summary>
    /// Run the pop up animation. 
    /// Next, show the banners and the character.
    /// After an amount of time, this object disappear.
    /// </summary>
    IEnumerator PopUp(){
        transform.localScale = Vector3.zero;
        MoneyRain.Play();
        while(transform.localScale.x < 1){
            yield return null;
            transform.localScale += Vector3.one * (Time.deltaTime/PopUpTime);
        }
        transform.localScale = Vector3.one;

        UpperBaner.RunAnimation();
        LowerBanner.RunAnimation();
        Character.RunAnimation();

        yield return new WaitForSeconds(EndTime);
        MoneyRain.Stop();
        while(transform.localScale.x > 0){
            yield return null;
            transform.localScale -= Vector3.one * (Time.deltaTime/PopUpTime);
        }
        gameObject.SetActive(false);
    }
}
