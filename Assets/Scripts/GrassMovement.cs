using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GrassMovement : MonoBehaviour
{
    public Material grass;
    private bool inEffect = false;

    private void Update()
    {
        if (Input.GetKeyDown("r"))
            SwayGrass(true, 6f, 4f);

        if (Input.GetKeyDown("t"))
            SwayGrass(false, 6f, 4f);
    }

    /// <summary>
    /// This method sways the grass left or right to accommodate events in the world.
    /// </summary>
    /// <param name="direction">True for left, false for right sway.</param>
    /// <param name="windForce">Simulates amount of force wind applies.</param>
    /// <param name="duration">Sets the length that the gust of wind is present for.</param>
    public void SwayGrass(bool direction, float windForce, float duration)
    {
        StartCoroutine(swayGrass(direction, windForce, duration));
    }

    private IEnumerator swayGrass(bool direction, float windForce, float duration)
    {
        if (direction && !inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-1, windForce), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration/0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
        else if (!inEffect)
        {
            inEffect = true;
            Tween startMove = grass.DOVector(new Vector2(-windForce, 1), "_WindDirection", duration);
            yield return startMove.WaitForCompletion();
            Tween endMove = grass.DOVector(new Vector2(-1, 1), "_WindDirection", duration / 0.5f);
            yield return endMove.WaitForCompletion();
            inEffect = false;
        }
    }
}
