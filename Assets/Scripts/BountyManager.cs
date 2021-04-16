using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] BountySO bountySO;
    [SerializeField] GameObject bountyCardPrefab;
    [SerializeField] List<BountyCard> bountyList;
    [SerializeField] Transform bountyCardSpawnPoint;
    [SerializeField] Transform bountyCardLeftUpper;
    [SerializeField] Transform bountyCardRightLower;

    List<Bounty> bountyBuffer;
    bool isEmpty;
    const float CARDSCALE = 1.3f;
    const float DOTWEENTIME = 0.7f;

    public Bounty PopItem()
    {
        if (bountyBuffer.Count == 0)
        {
            SetupBountyBuffer();
        }

        Bounty bounty = bountyBuffer[0];
        bountyBuffer.RemoveAt(0);
        return bounty;
    }
    void SetupBountyBuffer()
    {
        bountyBuffer = new List<Bounty>();
        for (int i = 0; i < bountySO.items.Length; ++i)
        {
            Bounty bounty = bountySO.items[i];
            bountyBuffer.Add(bounty);

        }

        for (int i = 0; i < bountyBuffer.Count; ++i)
        {
            int rand = Random.Range(i, bountyBuffer.Count);
            Bounty temp = bountyBuffer[i];
            bountyBuffer[i] = bountyBuffer[rand];
            bountyBuffer[rand] = temp;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupBountyBuffer();
        TurnManager.OnAddBountyCard += AddCard;
    }

    void OnDestroy()
    {
        TurnManager.OnAddBountyCard -= AddCard;
    }

    void Update()
    {
        /*
        //isEmpty = bountyList.Count < 6;
        // for debugging
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        if (isEmpty)
        {
            AddCard();
        }
        */
    }

    void AddCard()
    {
        var bountyCardObject = Instantiate(bountyCardPrefab, bountyCardSpawnPoint.position, Utils.QI);
        var bountyCard = bountyCardObject.GetComponent<BountyCard>();
        bountyCard.Setup(PopItem());
        bountyList.Add(bountyCard);
        CardAlignment();
    }

    void CardAlignment()
    {
        List<PRS> originBountyCardPRSs;// = new List<PRS>();
        originBountyCardPRSs = GridAlignment(bountyCardLeftUpper, bountyCardRightLower, bountyList.Count, Vector3.one * CARDSCALE);
        var targetCards = bountyList;
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

    #region MyBountyCard
    public void CardMouseOver(BountyCard card)
    {
        EnlargeCard(true, card);
    }

    public void CardMouseExit(BountyCard card)
    {
        EnlargeCard(false, card);
    }
    #endregion

    void EnlargeCard(bool isEnlarge, BountyCard card)
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
