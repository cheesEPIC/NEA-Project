using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float currentTime;
    public float startingTime = 0;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime;
    }

}
