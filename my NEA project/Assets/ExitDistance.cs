using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDistance : MonoBehaviour
{

    public GameObject ExitCell;
    public GameObject ExitCell1;

    public float dist;
    public float dist2;

    void Update()
    {
        
        float minDist = 1;
        dist = Vector3.Distance(ExitCell.transform.position, transform.localPosition);
        dist2 = Vector3.Distance(ExitCell1.transform.position, transform.localPosition);

        if (dist < minDist || dist2 < minDist)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }


}
