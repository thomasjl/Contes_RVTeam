using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public static class Utilities {

    public static void DestroyChidren(this Transform transform)
    {
        if (transform == null || transform.childCount < 1)
            return;
        for (int i = transform.childCount - 1; i >= 0; i--)
            UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
    }

    #region ProgressionAnim -------------------------------
    public static IEnumerator ProgressionAnim(this MonoBehaviour caller, float duration, System.Action<float> progressionHandler)
    {
        IEnumerator routine = ProgressionRoutine(duration, progressionHandler, delegate { });
        caller.StartCoroutine(routine);
        return routine;
    }
    public static IEnumerator ProgressionAnim(this MonoBehaviour caller, float duration, System.Action<float> progressionHandler, System.Action endActionHandler)
    {
        IEnumerator routine = ProgressionRoutine(duration, progressionHandler, endActionHandler);
        caller.StartCoroutine(routine);
        return routine;
    }

    static IEnumerator ProgressionRoutine(float duration, System.Action<float> progressionHandler, System.Action endActionHandler)
    {
        float progression = 0;
        while (progression <= 1)
        {
            progressionHandler(progression);
            progression += Time.deltaTime / duration;
            yield return null;
        }
        endActionHandler();
    }
    #endregion --------------------------------------------

    public static IEnumerator Timer(this MonoBehaviour caller, float duration, System.Action endActionHandler)
    {
        IEnumerator routine = TimerRoutine(duration, endActionHandler);
        caller.StartCoroutine(routine);
        return routine;
    }

    static IEnumerator TimerRoutine(float duration, System.Action endActionHandler)
    {
        yield return new WaitForSeconds(duration);
        endActionHandler();
    }


    #region Show/hide gameObjects ------------------
    /// <summary>
    /// Set all children active.
    /// </summary>
    /// <param name="go"></param>
    public static void Show(this GameObject go)
    {
        go.transform.SetChildrenActive(true);
    }
    /// <summary>
    /// Set all children inactive.
    /// </summary>
    /// <param name="go"></param>
    public static void Hide(this GameObject go)
    {
        go.transform.SetChildrenActive(false);
    }

    static void SetChildrenActive(this Transform transform, bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(state);
    }
    #endregion ------------------------------------

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        if (component.GetComponent<T>() != null)
            return component.GetComponent<T>();
        else
            return component.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Returns a random integer number between min [inclusive] and max [exclusive]. Never returns something equal to excluded.
    /// </summary>
    public static int ExclusiveRange(int min, int max, int excluded)
    {
        if (excluded < min || excluded >= max)
            return UnityEngine.Random.Range(min, max);
        int result = UnityEngine.Random.Range(min, max - 1);
        if (result >= excluded)
            result++;
        return result;
    }

    public static void LimitedIncrement(ref int index, int arrayLength)
    {
        index++;
        if (index >= arrayLength)
            index = 0;
    }
    public static void LimitedDecrement(ref int index, int arrayLength)
    {
        index--;
        if (index < 0)
            index = arrayLength - 1;
    }

    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    public static float RoundedDecimals(this float number, int decimalPlaces)
    {
        return (float)Math.Round(number, decimalPlaces);
    }

    #region Set Vector3 values ---------------------
    public static Vector3 SetX(this Vector3 vector3, float value)
    {
        return new Vector3(value, vector3.y, vector3.z);
    }
    public static Vector3 SetY(this Vector3 vector3, float value)
    {
        return new Vector3(vector3.x, value, vector3.z);
    }
    public static Vector3 SetZ(this Vector3 vector3, float value)
    {
        return new Vector3(vector3.x, vector3.y, value);
    }
    #endregion ------------------------------------------


    #region Duplicating a component ---------------------------
    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType())
            return null;
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { }
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }
    #endregion ------------------------------------------------------

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}