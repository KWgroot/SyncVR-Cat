using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GrassMovement : MonoBehaviour
{
    public Material grass;
    private bool inEffect = false;

    public void SwayGrass(bool direction, float windForce, float duration)
    {
        StartCoroutine(swayGrass(direction, windForce, duration));
    }

    private IEnumerator swayGrass(bool direction, float windForce, float duration)
    {
        if (direction && !inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-1, windForce*2), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration/0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
        else if (!inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-windForce*2, 1), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration / 0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
    }

    private void OnDestroy()
    {
        grass.SetVector("_WindDirection", new Vector2(-1, 1));
    }
}
