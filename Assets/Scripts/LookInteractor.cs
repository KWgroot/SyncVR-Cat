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
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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

    public async void SendActions()
    {
        if (enabled)
        {
            try
            {
                await gameManager.LookingAtInteractable(true, actions, cancelToken, this.gameObject);
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
                await gameManager.LookingAtInteractable(false, actions, cancelToken, null);
            }
            catch (OperationCanceledException exception)
            {
                //Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
            }
        }
    }
}
