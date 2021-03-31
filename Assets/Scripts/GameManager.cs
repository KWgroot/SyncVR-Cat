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
    public GrassMovement grass;
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
    public bool selectingInteractable = false, selected = false;
    private const int MAX_FILL = 1, MIN_FILL = 0, TASK_DELAY = 50;

    private CancellationTokenSource cancelToken;

    public enum Action : int
    {
        NoAction = 0,
        Select = 1,
        WindGust = 2,
        CheckConditionMet = 3
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
    /// <param name="action">Action to perform, can be chained after another.</param>
    /// <param name="token">Cancel task if looking away or finished unexpected.</param>
    /// <returns>Task if completed succesfully.</returns>
    public async Task LookingAtInteractable(bool looking, Action action, CancellationTokenSource token, GameObject gameObject)
    {
        cancelToken = token;
        currentlyLookingAt = gameObject;
        if (looking)
        {
            switch (action)
            {
                case Action.NoAction:
                    Debug.Log("No action set");
                    break;

                case Action.Select:
                    selectingInteractable = true;
                    while (!selected && selectingInteractable)
                        await Task.Delay(TASK_DELAY, cancelToken.Token);
                    break;

                case Action.WindGust:
                    while (!selected && selectingInteractable)
                        await Task.Delay(TASK_DELAY, cancelToken.Token);
                    WindGust(direction, windForce, duration);
                    break;

                case Action.CheckConditionMet:
                    while (!selected && selectingInteractable)
                        await Task.Delay(TASK_DELAY, cancelToken.Token);
                    SendCheckpointUpdate();
                    break;
            }
        }
        else
        {
            selectingInteractable = false;
            selected = false;
            currentlyLookingAt = null;
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
        grass.SwayGrass(direction, windPower, duration);
        //Move trees
        //Move bushes
    }

    private void SendCheckpointUpdate()
    {
        if (selectedObject != null && selected)
            checkpointManager.ConditionMet(selectedObject);
    }
}
