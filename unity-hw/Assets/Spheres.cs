using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Spheres
{
    private const float radius = 5;
    private const float smallRadius = 0.5f;
    private const int radialCount = 40;
    private const int vecticalCount = 40;
    static Spheres()
    {
        if (GameObject.Find("Spheres") != null) return;
        
        GameObject parent = new GameObject("Spheres");

        for (int i = 0; i < radialCount; i++)
        {
            var radialAngle = Mathf.PI * 2 * i / radialCount;
            for (int j = 0; j < vecticalCount; j++)
            {
                var verticalAngle = Mathf.PI * 2 * j / vecticalCount;
                
                float x = radius * Mathf.Sin(verticalAngle) * Mathf.Cos(radialAngle);
                float y = radius * Mathf.Cos(verticalAngle);
                float z = radius * Mathf.Sin(verticalAngle) * Mathf.Sin(radialAngle);

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(x, y, z);
                sphere.transform.localScale = Vector3.one * smallRadius;
                sphere.transform.parent = parent.transform;
            }
        }
    }
}
