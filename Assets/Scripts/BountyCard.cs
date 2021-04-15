using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BountyCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer bountyCard;
    [SerializeField] SpriteRenderer bountyImage;
    [SerializeField] TMP_Text attackNumTMP;
    [SerializeField] TMP_Text goldNumTMP;
    [SerializeField] TMP_Text rubyNumTMP;
    [SerializeField] TMP_Text scoreTMP;
    //[SerializeField] TMP_Text levelTextTMP;
    //[SerializeField] Sprite attack;
    //[SerializeField] Sprite gold;
    //[SerializeField] Sprite ruby;

    public Bounty bounty;

    public void Setup(Bounty bounty)
    {
        this.bounty = bounty;

        bountyImage.sprite = this.bounty.sprite;
        attackNumTMP.text = this.bounty.attack.ToString();
        goldNumTMP.text = this.bounty.getGold.ToString();
        rubyNumTMP.text = this.bounty.getRuby.ToString();
        scoreTMP.text = this.bounty.score.ToString();
    }
}
