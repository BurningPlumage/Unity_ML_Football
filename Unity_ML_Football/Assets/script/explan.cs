using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explan : MonoBehaviour
{
    public GameObject target;

    public void toExplain()
    {
        if (target.activeSelf)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }
    }
}
