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
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public async void StoppedLooking()
    {
        if (enabled)
        {
            cancelToken = new CancellationTokenSource();
            try
            {
                await gameManager.LookingAtInteractable(false, GameManager.Action.NoAction, cancelToken, null);
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

    public async void Select()
    {
        if (enabled)
        {
            cancelToken = new CancellationTokenSource();
            try
            {
                await gameManager.LookingAtInteractable(true, GameManager.Action.Select, cancelToken, this.gameObject);
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

    public async void WindTrigger()
    {
        if (enabled)
        {
            cancelToken = new CancellationTokenSource();
            try
            {
                await gameManager.LookingAtInteractable(true, GameManager.Action.WindGust, cancelToken, this.gameObject);
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

    public async void CheckpointTrigger()
    {
        if (enabled)
        {
            cancelToken = new CancellationTokenSource();
            try
            {
                await gameManager.LookingAtInteractable(true, GameManager.Action.CheckConditionMet, cancelToken, this.gameObject);
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
}
