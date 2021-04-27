using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public WindMovement wind;
    public Image selectionBar;

    [Header("UI Settings")]
    public float imageFillSpeed;

    [Header("Wind settings")]
    public bool direction;
    public float windForce, duration;

    [Header("Checkpoint system")]
    public CheckpointManager checkpointManager;

    [Header("Selected objects")]
    public GameObject selectedObject;
    public GameObject currentlyLookingAt;

    [Header("Player")]
    public GameObject player;

    [HideInInspector]
    public bool selectingInteractable = false, selected = false, dontCancel = false;
    private const int MAX_FILL = 1, MIN_FILL = 0, TASK_DELAY = 50;

    private CancellationTokenSource cancelToken;

    [HideInInspector]
    public HighlightScriptPerObject highlightPerObject;

    public enum Action : int
    {
        Select = 0,
        WindGust = 1,
        CheckConditionMet = 2,
        PlayAudio = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (selectingInteractable && selectionBar.fillAmount < MAX_FILL)
        {
            selectionBar.fillAmount += imageFillSpeed * Time.deltaTime;
        }
        else if (!selectingInteractable && selectionBar.fillAmount > MIN_FILL)
        {
            selectionBar.fillAmount = MIN_FILL;
            try
            {
                if (!dontCancel)
                    cancelToken.Cancel();
            }
            catch (ObjectDisposedException exception)
            {
                //Debug.Log($"{nameof(ObjectDisposedException)} thrown with message: {exception.Message}");
            }
        }
        else if (selectionBar.fillAmount >= MAX_FILL)
        {
            selectingInteractable = false;
            selectionBar.fillAmount = MIN_FILL;
            //SELECTED OBJECT
            selected = true;
            selectedObject = currentlyLookingAt;
        }
    }
    /// <summary>
    /// When called checks if player is looking at interactable and what action to perform.
    /// </summary>
    /// <param name="looking">True if currently looking at interactable.</param>
    /// <param name="actions">List of actions to perform.</param>
    /// <param name="token">Cancel task if looking away or finished unexpected.</param>
    /// <param name="gameObject">Object currently being looked at.</param>
    /// <returns>Task if completed succesfully.</returns>
    public async Task LookingAtInteractable(bool looking, List<Action> actions, CancellationTokenSource token, GameObject gameObject, AudioSource audioSource)
    {     
        if (looking)
        {
            cancelToken = token;
            currentlyLookingAt = gameObject;

            if (gameObject.GetComponent<HighlightScriptPerObject>() != null)
            {
                highlightPerObject = gameObject.GetComponent<HighlightScriptPerObject>();
                highlightPerObject.HighLight(true);
            }

            foreach (Action action in actions)
            {
                if (!cancelToken.IsCancellationRequested)
                {
                    switch (action)
                    {
                        case Action.Select:
                            selectingInteractable = true;
                            while (!selected && selectingInteractable)
                                await Task.Delay(TASK_DELAY, cancelToken.Token);
                            break;

                        case Action.WindGust:
                            WindGust(direction, windForce, duration);
                            break;

                        case Action.CheckConditionMet:
                            SendCheckpointUpdate();
                            break;

                        case Action.PlayAudio:
                            audioSource.Play();
                            dontCancel = true;
                            while (audioSource.isPlaying)
                                await Task.Delay(TASK_DELAY, cancelToken.Token);
                            break;
                    }
                }
            }
        }
        else
        {
            if (!dontCancel)
            {
                try
                {
                    cancelToken.Cancel();
                }
                catch (ObjectDisposedException exception)
                {
                    //Debug.Log($"{nameof(ObjectDisposedException)} thrown with message: {exception.Message}");
                }
                selectingInteractable = false;
                selected = false;
                currentlyLookingAt = null;

                if (highlightPerObject != null)
                    highlightPerObject.HighLight(false);
            }            
        }
    }
    /// <summary>
    /// This method sways the grass left or right to accommodate events in the world.
    /// </summary>
    /// <param name="direction">True for left, false for right sway.</param>
    /// <param name="windForce">Simulates amount of force wind applies.</param>
    /// <param name="duration">Sets the length that the gust of wind is present for.</param>
    private void WindGust(bool direction, float windPower, float duration)
    {
        wind.Sway(direction, windPower, duration);
        //Move trees
        //Move bushes
    }

    private void SendCheckpointUpdate()
    {
        if (selectedObject != null && selected)
            checkpointManager.ConditionMet(selectedObject);
        else if (currentlyLookingAt != null)
            checkpointManager.ConditionMet(currentlyLookingAt);
    }
}
