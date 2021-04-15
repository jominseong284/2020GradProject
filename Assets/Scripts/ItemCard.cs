using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public void Setup(Item item)
    {
        this.item = item;

        equipmentImage.sprite = this.item.sprite;
        attackNumTMP.text = this.item.attack.ToString();
        goldNumTMP.text = this.item.gold.ToString();
        rubyNumTMP.text = this.item.ruby.ToString();
        levelTMP.text = this.item.level.ToString();
    }
}
