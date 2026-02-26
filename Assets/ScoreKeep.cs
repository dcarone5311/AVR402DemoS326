using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreKeep : MonoBehaviour
{

    public static ScoreKeep instance;
    [HideInInspector]
    public int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Coins Collected: " + score.ToString();
    }
}
