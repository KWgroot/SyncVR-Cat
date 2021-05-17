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
    private AudioSource startAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = transform.parent.GetComponent<GameManager>();

        foreach (Transform child in transform)
        {
            scenes.Add(child.GetComponent<Scene>());
            child.gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);

        foreach (Scene scene in scenes)
            scene.FillCheckpoints(this);
    }

    public void ConditionMet(GameObject checkpoint)
    {        
        if (scenes[sceneNumber].checkpoints.Find(x => x.name == checkpoint.name) && scenes[sceneNumber].currentCheckpoint != scenes[sceneNumber].checkpoints.Count - 1)
        {
            Debug.Log("Next checkpoint");
            scenes[sceneNumber].currentCheckpoint++;
            scenes[sceneNumber].checkpoints[scenes[sceneNumber].currentCheckpoint].GetComponent<LookInteractor>().enabled = true;
            gameManager.dontCancel = false;
            if (gameManager.highlightPerObject != null)
            {
                gameManager.highlightPerObject.HighLight(false);
            }
            checkpoint.GetComponent<LookInteractor>().enabled = false;
            gameManager.selected = false;
        }
        else if (scenes[sceneNumber].checkpoints.Find(x => x.name == checkpoint.name) && scenes[sceneNumber].currentCheckpoint == scenes[sceneNumber].checkpoints.Count - 1)
        {
            Debug.Log("Next scene");
            gameManager.dontCancel = false;
            if (gameManager.highlightPerObject != null)
            {
                gameManager.highlightPerObject.HighLight(false);
            }
            checkpoint.GetComponent<LookInteractor>().enabled = false;
            gameManager.selected = false;
            sceneNumber++;
            scenes[sceneNumber].gameObject.SetActive(true);
            if (scenes[sceneNumber].gameObject.GetComponent<AudioSource>() == null)
                scenes[sceneNumber].checkpoints[0].GetComponent<LookInteractor>().enabled = true;
            else
            {
                startAudio = scenes[sceneNumber].gameObject.GetComponent<AudioSource>();
                StartCoroutine(WaitToStart(startAudio));
            }
            StartCoroutine(FadeEffect(true));
        }
    }

    IEnumerator WaitToStart(AudioSource startAudio)
    {
        if (startAudio.isPlaying)
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitToStart(startAudio));
        }
        else
            scenes[sceneNumber].checkpoints[0].GetComponent<LookInteractor>().enabled = true;
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
            scenes[sceneNumber - 1].gameObject.SetActive(false);
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
