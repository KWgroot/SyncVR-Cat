using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    public int currentCheckpoint = 0;
    private AudioSource startingAudio;

    public void FillCheckpoints(CheckpointManager manager)
    {
        foreach (Transform child in transform)
        {
            checkpoints.Add(child.gameObject);
            if (child.gameObject.GetComponent<LookInteractor>() != null)
                child.gameObject.GetComponent<LookInteractor>().enabled = false;
        }

        if (manager.sceneNumber == manager.scenes.IndexOf(this) && this.gameObject.GetComponent<AudioSource>() == null)
            checkpoints[0].GetComponent<LookInteractor>().enabled = true;
        else if (this.gameObject.GetComponent<AudioSource>() != null && this.transform.GetSiblingIndex() == 0)
        {
            startingAudio = this.gameObject.GetComponent<AudioSource>();
            StartCoroutine(WaitToStart(startingAudio));
        }
    }

    IEnumerator WaitToStart(AudioSource startAudio)
    {
        if (startingAudio.isPlaying)
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(WaitToStart(startAudio));
        }
        else
            checkpoints[0].GetComponent<LookInteractor>().enabled = true;
    }
}
