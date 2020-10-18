using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Returns normalized direction 
    /// </summary>
    public static Vector3 GetDirection(Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        return dir.normalized;
    }
    public static Vector3 GetDirection(GameObject from, GameObject to) => GetDirection(from.transform.position, to.transform.position);

    /// <summary>
    /// Adds two vectors together axis wise  (x+x, y+y, z+z)
    /// </summary>
    public static Vector3 AddVectors(Vector3 lhs, Vector3 rhs) => new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);

    public static Vector3 GetMouseWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}