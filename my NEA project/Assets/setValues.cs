using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setValues : MonoBehaviour
{

    public static float mapSize;
    public static float difficulty;
    public static string seed;
    public static bool randomSeed = true;



    public void MapSize(float number)
    {
        mapSize = number;
    }

    public void Difficulty(float number)
    {
        difficulty = number;
    }

    public void Seed(string input)
    {
        seed = input;
    }

    public void RandomSeed(bool random)
    {
        randomSeed = random;
    }
}


