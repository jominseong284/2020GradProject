using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ItemCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer itemCard;
    [SerializeField] SpriteRenderer equipmentImage;
    [SerializeField] TMP_Text attackNumTMP;
    [SerializeField] TMP_Text goldNumTMP;
    [SerializeField] TMP_Text rubyNumTMP;
    [SerializeField] TMP_Text levelTMP;
    //[SerializeField] TMP_Text levelTextTMP;
    //[SerializeField] Sprite attack;
    //[SerializeField] Sprite gold;
    //[SerializeField] Sprite ruby;

    public Item item;
    public PRS originPRS;
    public void Setup(Item item)
    {
        this.item = item;

        equipmentImage.sprite = this.item.sprite;
        attackNumTMP.text = this.item.attack.ToString();
        goldNumTMP.text = this.item.gold.ToString();
        rubyNumTMP.text = this.item.ruby.ToString();
        levelTMP.text = this.item.level.ToString();
    }
    void OnMouseOver()
    {
        ItemManager.Inst.CardMouseOver(this);
    }

    void OnMouseExit()
    {
        ItemManager.Inst.CardMouseExit(this);
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            //transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }

        else
        {
            transform.position = prs.pos;
            //transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
