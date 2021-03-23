using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteractor : MonoBehaviour
{
    private GameManager GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookedAt()
    {
        Debug.Log("I'VE BEEN SPOTTED");
        GameManager.LookingAtInteractable(true);
    }

    public void StoppedLooking()
    {
        Debug.Log("BYYYYYYYE");
        GameManager.LookingAtInteractable(false);
    }
}
