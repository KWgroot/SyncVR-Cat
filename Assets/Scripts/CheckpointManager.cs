using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Scene> scenes = new List<Scene>();
    public int sceneNumber = 0;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = transform.parent.GetComponent<GameManager>();

        foreach (Transform child in transform)
        {
            scenes.Add(child.GetComponent<Scene>());
        }
    }

    public void ConditionMet(GameObject checkpoint)
    {        
        if (scenes[sceneNumber].checkpoints.Find(x => x.name == checkpoint.name) && scenes[sceneNumber].currentCheckpoint != scenes[sceneNumber].checkpoints.Count - 1)
        {
            Debug.Log("Next checkpoint");
            scenes[sceneNumber].currentCheckpoint++;
            scenes[sceneNumber].checkpoints[scenes[sceneNumber].currentCheckpoint].GetComponent<LookInteractor>().enabled = true;
            checkpoint.GetComponent<LookInteractor>().enabled = false;
            gameManager.selected = false;
        }
        else if (scenes[sceneNumber].checkpoints.Find(x => x.name == checkpoint.name) && scenes[sceneNumber].currentCheckpoint == scenes[sceneNumber].checkpoints.Count - 1)
        {
            Debug.Log("Next scene");
            checkpoint.GetComponent<LookInteractor>().enabled = false;
            gameManager.selected = false;
            sceneNumber++;
            scenes[sceneNumber].checkpoints[0].GetComponent<LookInteractor>().enabled = true;
            
        }
    }
}
