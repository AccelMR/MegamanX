using UnityEngine;

public static class UnityExtensions
{
  public static float DistanceSqrd(this Vector2 v, Vector2 other)
  {
    return (v - other).sqrMagnitude;
  }

  public static Vector2 Rotate(this Vector2 v, Quaternion q)
  {
    return q * (Vector3)v;
  }

  /// <summary>
  /// Project vector a onto b
  /// </summary>
  public static float Project(this Vector2 a, Vector2 b)
  {
    return Vector2.Dot(a, b.normalized);
  }

  /// <summary>
  /// Get Vector from projecting vector a onto b
  /// </summary>
  public static Vector2 VProject(this Vector2 a, Vector2 b)
  {
    b = b.normalized;
    float dp = Vector2.Dot(a, b);
    return new Vector2(dp * b.x, dp * b.y);
  }

  /// <summary>
  /// Creates a copy of given component inside given gameObject
  /// </summary>
  public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
  {
    System.Type type = original.GetType();
    var dst = destination.GetComponent(type) as T;
    if (!dst) dst = destination.AddComponent(type) as T;
    var fields = type.GetFields();
    foreach (var field in fields)
    {
      if (field.IsStatic) continue;
      field.SetValue(dst, field.GetValue(original));
    }
    var props = type.GetProperties();
    foreach (var prop in props)
    {
      if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
      prop.SetValue(dst, prop.GetValue(original, null), null);
    }
    return dst as T;
  }

  /// <summary>
  /// Finds Child GameObject with given name
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public static GameObject GetChildWithName(this GameObject obj, string name)
  {
    Transform trans = obj.transform;
    Transform childTrans = trans.Find(name);
    if (childTrans != null)
    {
      return childTrans.gameObject;
    }
    else
    {
      return null;
    }
  }
}
