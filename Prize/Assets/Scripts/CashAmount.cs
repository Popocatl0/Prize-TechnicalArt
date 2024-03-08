using System.Collections;
using TMPro;
using UnityEngine;

public class CashAmount : IAnimable
{
    [SerializeField] int  FinalAmount = 1000000;
    TextMeshPro AmountText;
   public override void Init() {
        AmountText = GetComponent<TextMeshPro>();
    }
    public override void Reset(){
        AmountText.text = "$0.00";
    }
    /// <summary>
    /// Starts a Roll up animation until the amount reaches the Final Amount.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Animation(){
        int amount = 0;
        while(amount < FinalAmount){
            yield return null;
            amount += Mathf.CeilToInt(FinalAmount * (Time.deltaTime/AnimationTime));
            AmountText.text = amount.ToString("C");
        }
        amount = FinalAmount;
        AmountText.text = amount.ToString("C");
    }
}
