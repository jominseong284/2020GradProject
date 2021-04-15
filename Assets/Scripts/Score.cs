using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 1;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
