using UnityEngine;

public static class ExtTransforms
{
    public static void DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform) {
            MonoBehaviour.Destroy(child.gameObject);
        }
    }
}
