using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMove1 : MonoBehaviour
{


    [SerializeField] private Transform startPos;



    private void Update()
    {
        int x = MazeGeneration.xPos;
        int y = MazeGeneration.yPos;
        int decider = MazeGeneration.decider;

        Vector3 NewPosition = new Vector3(x, 1, y);
        if (decider != 1)
        {
            transform.position = Vector3.LerpUnclamped(startPos.position, NewPosition, 10f * Time.deltaTime);
        }
    }



}


