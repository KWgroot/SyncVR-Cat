using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    private CheckpointManager checkpointManager;
    public int currentCheckpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = transform.parent.GetComponent<CheckpointManager>();

        foreach (Transform child in transform)
        {
            checkpoints.Add(child.gameObject);
            child.gameObject.GetComponent<LookInteractor>().enabled = false;
        }

        if (checkpointManager.sceneNumber == checkpointManager.scenes.IndexOf(this))
            checkpoints[0].GetComponent<LookInteractor>().enabled = true;
    }
}
