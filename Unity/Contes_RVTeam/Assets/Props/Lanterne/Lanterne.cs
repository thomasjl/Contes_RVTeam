using System;
using System.Collections;
using UnityEngine;

public class Lanterne : MonoBehaviour {

    LanterneFlame flame;
    public Vector3 FlamePosition{ get{ return flame.transform.position; } }

    public static Lanterne instance;
    private void Awake()
    {
        instance = this;
        flame = GetComponentInChildren<LanterneFlame>();
    }

   
    internal void SetFlameColor(object color)
    {
        throw new NotImplementedException();
    }

    public void SetFlameColor(Color color)
    {
        StopAllCoroutines();
        flame.SetColor(color);
    }

    public void PlayColorAnim(float duration, Color color)
    {
        StartCoroutine(AnimateFlameColor(duration, color));
    }

    IEnumerator AnimateFlameColor(float duration, Color targetColor)
    {
        Color startColor = flame.Color;
        float progression = 0;
        while (progression < 1)
        {
            flame.SetColor(Color.Lerp(targetColor, startColor, progression));
            progression += Time.deltaTime / duration;
            yield return null;
        }
    }
}
