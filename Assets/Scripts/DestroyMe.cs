using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    private bool startedCleanup = false;
    private void OnTriggerEnter(Collider other)
    {
        if (this.name != "CleanupArea")
            Destroy(this.gameObject);
        else if (!startedCleanup)
        {
            startedCleanup = true;
            StartCoroutine(RemoveArea());
        }
    }

    private IEnumerator RemoveArea()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
