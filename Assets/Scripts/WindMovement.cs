using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WindMovement : MonoBehaviour
{
    public Material grass;
    public Material leaves;
    private bool inEffect = false;

    public void Sway(bool direction, float windForce, float duration)
    {
        StartCoroutine(swayFoliage(direction, windForce, duration));
    }

    private IEnumerator swayFoliage(bool direction, float windForce, float duration)
    {
        if (direction && !inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-1, windForce*2), "_WindDirection", duration);
            leaves.DOVector(new Vector2(-1, windForce), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration/0.5f);
            leaves.DOVector(new Vector2(-1, 1), "_WindDirection", duration / 0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
        else if (!inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-windForce*2, 1), "_WindDirection", duration);
            leaves.DOVector(new Vector2(-windForce, 1), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration / 0.5f);
            leaves.DOVector(new Vector2(-1, 1), "_WindDirection", duration / 0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
    }

    private void OnDestroy()
    {
        grass.SetVector("_WindDirection", new Vector2(-1, 1));
        leaves.SetVector("_WindDirection", new Vector2(-1, 1));
    }
}
