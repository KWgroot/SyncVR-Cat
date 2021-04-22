using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteractor : MonoBehaviour
{
    private GameManager gameManager;
    private CancellationTokenSource cancelToken;
    private List<GameManager.Action> actions;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (GetComponent<AudioSource>() != null)
            audioSource = GetComponent<AudioSource>();
    }

    public void StartInteraction()
    {
        if (enabled)
        {
            cancelToken = new CancellationTokenSource();
            actions = new List<GameManager.Action>();
        }
    }

    public void Select()
    {
        if (enabled)
        {
            actions.Add(GameManager.Action.Select);
        }
    }

    public void WindTrigger()
    {
        if (enabled)
        {
            actions.Add(GameManager.Action.WindGust);
        }
    }

    public void CheckpointTrigger()
    {
        if (enabled)
        {
            actions.Add(GameManager.Action.CheckConditionMet);
        }
    }

    public void PlayAudio()
    {
        if (enabled)
            actions.Add(GameManager.Action.PlayAudio);
    }

    public async void SendActions()
    {
        if (enabled && !gameManager.dontCancel)
        {
            try
            {
                await gameManager.LookingAtInteractable(true, actions, cancelToken, this.gameObject, audioSource);
            }
            catch (OperationCanceledException exception)
            {
                //Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
            }
            finally
            {
                cancelToken.Dispose();
            }
        }
    }

    public async void StoppedLooking()
    {
        if (enabled)
        {
            try
            {
                await gameManager.LookingAtInteractable(false, actions, cancelToken, null, null);
            }
            catch (OperationCanceledException exception)
            {
                //Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
            }
        }
    }
}
