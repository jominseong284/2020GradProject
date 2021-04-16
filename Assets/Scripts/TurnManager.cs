using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("카드 배분이 빨라집니다")] bool fastMode;
    //[SerializeField] [Tooltip("시작 재화 개수를 정합니다")] int[] startGoodsCount;

    [Header("Properties")]
    public bool isLoading;
    public bool myTurn;

    enum ETurnMode { Random, My, Other }
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);

    const int MAXBOUNTYCARDCOUNT = 6;
    const int MAXITEMCARDCOUNT = 10;

    public static Action OnAddItemCard;
    public static Action OnAddBountyCard;

    void GameSetup()
    {
        // for debugging
        if (fastMode)
        {
            delay05 = new WaitForSeconds(0.05f);
        }

        switch (eTurnMode)
        {
            case ETurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
        //StartCoroutine(StartTurnCo());
    }
    public IEnumerator StartGameCo()
    {
        GameSetup();
        isLoading = true;
        // action
        for (int i = 0; i < MAXITEMCARDCOUNT; ++i)
        {
            OnAddItemCard?.Invoke();
            yield return delay05;
        }

        for (int i = 0; i < MAXBOUNTYCARDCOUNT; ++i)
        {
            OnAddBountyCard?.Invoke();
            yield return delay05;
        }
        StartCoroutine(StartTurnCo());
    }
    
    IEnumerator StartTurnCo()
    {
        isLoading = true;
        if (myTurn)
        {
            GameManager.Inst.MyTurnMessage("My turn");
        }
        yield return delay05;
        isLoading = false;
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
    }
}