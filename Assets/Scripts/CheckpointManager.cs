using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityTemplateProjects;

public class CheckpointManager : MonoBehaviour
{
    public List<Scene> scenes = new List<Scene>();
    public int sceneNumber = 0;
    public Image fadeImage;
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
            StartCoroutine(FadeEffect(true));
        }
    }

    IEnumerator FadeEffect(bool fadeAway)
    {
        // fade from transparant to opaque
        if (fadeAway)
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += (Time.deltaTime / 2))
            {
                // set color with i as alpha
                fadeImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            Debug.Log("Faded out");
            RelocatePlayer();
            StartCoroutine(FadeEffect(false));
        }
        // fade from opaque to transparant
        else
        {
            yield return new WaitForSeconds(0.5f);
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime / 4))
            {
                // set color with i as alpha
                fadeImage.color = new Color(1, 1, 1, i);
                yield return null;
            }            
        }
    }

    private void RelocatePlayer()
    {
        gameManager.player.transform.GetChild(0).transform.GetChild(0).GetComponent<SimpleCameraController>().enabled = false;
        gameManager.player.transform.position = scenes[sceneNumber].transform.position;
        gameManager.player.transform.GetChild(0).transform.GetChild(0).GetComponent<SimpleCameraController>().enabled = true;
    }
}
