using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] BountySO bountySO;
    [SerializeField] GameObject bountyCardPrefab;

    List<Bounty> bountyBuffer;
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
    }

    void Update()
    {
        // for debugging
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            // for debugging
            //print(PopItem().attack);
            AddCard(true);
        }

        void AddCard(bool isEmpty)
        {
            var bountyCardObject = Instantiate(bountyCardPrefab, Vector3.zero, Quaternion.identity);
            var bountyCard = bountyCardObject.GetComponent<BountyCard>();
            if (isEmpty)
            {
                bountyCard.Setup(PopItem());
            }
        }
    }
}
