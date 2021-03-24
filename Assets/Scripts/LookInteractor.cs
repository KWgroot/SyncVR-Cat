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

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Select()
    {
        cancelToken = new CancellationTokenSource();
        try
        {
            await gameManager.LookingAtInteractable(true, GameManager.Action.Select, cancelToken);
        }
        catch (OperationCanceledException exception)
        {
            Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
        }
        finally
        {
            cancelToken.Dispose();
        }
    }

    public async void WindTrigger()
    {
        cancelToken = new CancellationTokenSource();
        try
        {
            await gameManager.LookingAtInteractable(true, GameManager.Action.WindGust, cancelToken);
        }
        catch (OperationCanceledException exception)
        {
            Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
        }
        finally
        {
            cancelToken.Dispose();
        }
    }    

    public async void StoppedLooking()
    {
        cancelToken = new CancellationTokenSource();
        try
        {
            await gameManager.LookingAtInteractable(false, GameManager.Action.NoAction, cancelToken);
        }
        catch (OperationCanceledException exception)
        {
            Debug.Log($"{nameof(OperationCanceledException)} thrown with message: {exception.Message}");
        }
        finally
        {
            cancelToken.Dispose();
        }
    }
}
