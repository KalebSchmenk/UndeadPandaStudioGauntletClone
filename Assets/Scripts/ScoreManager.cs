using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Did not function correctly but I dont know if something uses it so I'm not gonna delete :)
    
    [SerializeField] public static int score = 0;

    public static void AddScore(int addedScore)
    {
        score += addedScore;
    }
}
