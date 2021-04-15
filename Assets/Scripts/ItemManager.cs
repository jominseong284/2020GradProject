using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject itemCardPrefab;

    List<Item> itemBuffer;
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
    }

    void Update()
    {
        // for debugging
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            // for debugging
            //print(PopItem().attack);
            // for debugging
            AddCard(true);
        }
    }

    void AddCard(bool isEmpty)
    {
        var itemCardObject = Instantiate(itemCardPrefab, Vector3.zero, Quaternion.identity);
        var itemCard = itemCardObject.GetComponent<ItemCard>();
        if (isEmpty)
        {
            itemCard.Setup(PopItem());
        }
    }
}
