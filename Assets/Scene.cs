using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scene : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            checkpoints.Add(child.gameObject);
            child.gameObject.GetComponent<LookInteractor>().enabled = false;
        }
        checkpoints[0].GetComponent<LookInteractor>().enabled = true;
    }
}
