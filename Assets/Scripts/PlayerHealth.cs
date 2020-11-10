using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Text[] scores;
    public GameObject game;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void ScoreUpdate()
    {
        if (index >= 3)
        {
            game.SetActive(true);
            return;
        }
        scores[index].color = Color.white;
        index += 1;
    }
}
