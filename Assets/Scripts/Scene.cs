using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    public int currentCheckpoint = 0;

    public void FillCheckpoints(CheckpointManager manager)
    {
        foreach (Transform child in transform)
        {
            checkpoints.Add(child.gameObject);
            if (child.gameObject.GetComponent<LookInteractor>() != null)
                child.gameObject.GetComponent<LookInteractor>().enabled = false;
        }

        if (manager.sceneNumber == manager.scenes.IndexOf(this))
            checkpoints[0].GetComponent<LookInteractor>().enabled = true;
    }
}
