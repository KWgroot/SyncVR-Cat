using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Scene> scenes = new List<Scene>();
    private int sceneNumber = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            scenes.Add(child.GetComponent<Scene>());
        }
    }

    public void ConditionMet(GameObject checkpoint)
    {
        if (scenes[sceneNumber].checkpoints.Find(x => x.name == checkpoint.name))
        {
            Debug.Log("FOUND YOU"); //if last checkpoint, next scene
            // Go to next checkpoint or scene, idk someething
        }
    }
}
