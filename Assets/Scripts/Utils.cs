using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Color SetA(this Color color, float value)
    {
        color.a = value;
        return color;
    }

    public static Color SetR(this Color color, float value)
    {
        color.r = value;
        return color;
    }

    public static Color SetG(this Color color, float value)
    {
        color.g = value;
        return color;
    }

    public static Color SetB(this Color color, float value)
    {
        color.b = value;
        return color;
    }

    public static Vector3 SetX(this Vector3 vector, float value)
    {
        vector.x = value;
        return vector;
    }

    public static Vector3 SetY(this Vector3 vector, float value)
    {
        vector.y = value;
        return vector;
    }

    public static Vector3 SetZ(this Vector3 vector, float value)
    {
        vector.z = value;
        return vector;
    }

    public static Vector2 SqueezeY(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static Vector2Int SqueezeY(this Vector3Int vector)
    {
        return new Vector2Int(vector.x, vector.z);
    }

    public static Vector3 UnsqueezeY(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }

    public static Vector3Int UnsqueezeY(this Vector2Int vector)
    {
        return new Vector3Int(vector.x, 0, vector.y);
    }

    public static Vector2 SetX(this Vector2 vector, float value)
    {
        vector.x = value;
        return vector;
    }

    public static Vector2 SetY(this Vector2 vector, float value)
    {
        vector.y = value;
        return vector;
    }

    public static void Wait(this MonoBehaviour monoBehaviour, object instruction, Action action)
    {
        monoBehaviour.StartCoroutine(DoWait(instruction, action));
    }

    private static IEnumerator DoWait(object instruction, Action action)
    {
        yield return instruction;
        action.Invoke();
    }

    public static void WaitTime(this MonoBehaviour monoBehaviour, float delay, Action action)
    {
        monoBehaviour.StartCoroutine(DoWait(new WaitForSeconds(delay), action));
    }

    public static T Pop<T>(this List<T> list)
    {
        T res = list[^1];
        list.RemoveAt(list.Count - 1);
        return res;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n); // n은 포함, n은 제외됨
            n--;
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public static IEnumerable<T> RotatedView<T>(this IList<T> list)
    {
        int pivot = UnityEngine.Random.Range(0, list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            yield return list[(pivot + i) % list.Count];
        }
    }

    public static IEnumerable<T> RotatedView<T>(this T[] arr)
    {
        int pivot = UnityEngine.Random.Range(0, arr.Length);
        for (int i = 0; i < arr.Length; i++)
        {
            yield return arr[(pivot + i) % arr.Length];
        }
    }

    public static readonly Vector3Int[] directions3 = new Vector3Int[]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right,
        Vector3Int.forward,
        Vector3Int.back,
    };

    /// <summary>
    /// 시계방향으로 2차원 Vector2Int를 나열한 배열
    /// </summary>
    public static readonly Vector2Int[] directions2 = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public static Vector2 Perp(this Vector2 b, Vector2 a)
    {
        Vector2 aNormalized = a.normalized;
        Vector2 bParallel = Vector2.Dot(b, aNormalized) * aNormalized;
        return b - bParallel;
    }

    public static Vector2 Rotate90(this Vector2 v)
    {
        return new Vector2(-v.y, v.x);
    }

    public static Vector2Int Rotate90(this Vector2Int v)
    {
        return new Vector2Int(-v.y, v.x);
    }
}
