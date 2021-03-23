using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image SelectionBar;
    public float ImageFillSpeed;

    private bool HoveringInteractable = false;
    private const int MAX_FILL = 1, MIN_FILL = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HoveringInteractable && SelectionBar.fillAmount < MAX_FILL)
        {
            SelectionBar.fillAmount += ImageFillSpeed * Time.deltaTime;
        }
        else if (!HoveringInteractable && SelectionBar.fillAmount > MIN_FILL)
        {
            SelectionBar.fillAmount = MIN_FILL;
        }
        else if (SelectionBar.fillAmount >= MAX_FILL)
        {
            HoveringInteractable = false;
            SelectionBar.fillAmount = MIN_FILL;
        }
    }

    public void LookingAtInteractable(bool looking)
    {
        if (looking)
            HoveringInteractable = true;
        else
            HoveringInteractable = false;
    }
}
