using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public static int score = 0;

    public static void AddScore(int addedScore)
    {
        score += addedScore;
    }
}
