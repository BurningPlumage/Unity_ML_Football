using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    public static bool isgoal = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name== "Ball")
        {
            isgoal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Ball")
        {
            isgoal = false;
        }
    }
}
