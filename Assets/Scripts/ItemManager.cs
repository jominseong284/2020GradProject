using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject itemCardPrefab;
    [SerializeField] List<ItemCard> itemList;
    [SerializeField] Transform itemCardSpawnPoint;
    [SerializeField] Transform itemCardLeftUpper;
    [SerializeField] Transform itemCardRightLower;

    List<Item> itemBuffer;
    bool isEmpty;
    const float CARDSCALE = 1.3f;
    const float DOTWEENTIME = 0.7f;
    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
        {
            SetupItemBuffer();
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }
    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>();
        for (int i = 0; i < itemSO.items.Length; ++i)
        {
            Item item = itemSO.items[i];
            itemBuffer.Add(item);
        }

        for (int i = 0; i < itemBuffer.Count; ++i)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupItemBuffer();
        TurnManager.OnAddItemCard += AddCard;
    }

    void OnDestroy()
    {
        TurnManager.OnAddItemCard -= AddCard;
    }

    void Update()
    {
        /*
        //isEmpty = itemList.Count < 9;
        // for debugging
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        if (isEmpty)
        {
            AddCard();
        }
        */
    }

    void AddCard()
    {
        var itemCardObject = Instantiate(itemCardPrefab, itemCardSpawnPoint.position, Utils.QI);
        var itemCard = itemCardObject.GetComponent<ItemCard>();
        itemCard.Setup(PopItem());
        itemList.Add(itemCard);
        CardAlignment();
    }

    void CardAlignment()
    {
        List<PRS> originBountyCardPRSs = new List<PRS>();
        originBountyCardPRSs = GridAlignment(itemCardLeftUpper, itemCardRightLower, itemList.Count, Vector3.one * CARDSCALE);
        var targetCards = itemList;
        for (int i = 0; i < targetCards.Count; ++i)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originBountyCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, DOTWEENTIME);
        }
    }

    List<PRS> GridAlignment(Transform leftUpper, Transform rightLower, int objCount, Vector3 scale)
    {
        List<PRS> results = new List<PRS>(objCount);

        var targetPos = leftUpper.position;
        var originPosX = leftUpper.position.x;
        float xInterval = (rightLower.position.x - leftUpper.position.x) / 3;
        float yInterval = (rightLower.position.y - leftUpper.position.y) / 2;

        for (int index = 0; index < objCount; ++index)
        {
            //var targetRot = Quaternion.identity;

            if (index != 0)
            {
                targetPos.x += xInterval;
            }

            if (index % 3 == 0 && index != 0)
            {
                targetPos.y += yInterval;
                targetPos.x = originPosX;
            }
            results.Add(new PRS(targetPos, scale));
        }
        return results;
    }

    #region MyItemCard
    public void CardMouseOver(ItemCard card)
    {
        EnlargeCard(true, card);
    }

    public void CardMouseExit(ItemCard card)
    {
        EnlargeCard(false, card);
    }
    #endregion

    void EnlargeCard(bool isEnlarge, ItemCard card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -4.8f, -10f);
            card.MoveTransform(new PRS(enlargePos, Vector3.one * 3.5f), false);
        }
        else
        {
            card.MoveTransform(card.originPRS, false);
        }
        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }
}
