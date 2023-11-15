using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new();

    public static WaitForSeconds BetterWaitForSeconds(float seconds)
    {
        // Check if WaitForSeconds instance for the specified seconds exists in the dictionary
        if (!WaitDictionary.TryGetValue(seconds, out var wait))
        {
            wait = new WaitForSeconds(seconds);
            WaitDictionary.Add(seconds, wait);
        }

        return wait;
    }

    public static Vector3 GenerateRandomVector3(Vector3 min, Vector3 max)
    {
        var randomX = Random.Range(min.x, max.x);
        var randomY = Random.Range(min.y, max.y);
        var randomZ = Random.Range(min.z, max.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public static Vector3 GetWorldPositionOfRectTransform(RectTransform rt)
    {
        // Getting world position from rect transform wont work without Z value = 0f !!
        // So adjust camera Z offset as positive value. For example: -9f
        var cameraZOffset = 9f;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(rt.position.x,
            rt.position.y, cameraZOffset));

        return worldPos;
    }

    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static void SetChildrenActiveState(this Transform t, bool state)
    {
        foreach (Transform child in t) child.gameObject.SetActive(state);
    }

    public static int LayerToInt(this LayerMask mask)
    {
        return Mathf.RoundToInt(Mathf.Log(mask.value, 2));
    }

    public static int Sqr(this int value)
    {
        return value * value;
    }

    public static float Sqr(this float value)
    {
        return value * value;
    }
}