using System;
using System.Collections.Generic;
using SecondHomework;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    private GameObject satelitePrefab;
    [SerializeField]
    [Range(1.6f, 30f)]
    private float obritRadius;
    [Tooltip("In radians per second")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    [Range(0, 15)]
    private int sateliteCount;
    [SerializeField]
    private SecondHomework.RotationDirection rotationDirection;
    [SerializeField]
    private SecondHomework.DispersionType dispersionType;
    [SerializeField]
    [Tooltip("Only for close dispersion type, in radians")]
    [Range(0f, Mathf.PI)]
    private float inbetweenDistance;

    private float angle = 0f;
    private float direction = 1;

    private List<GameObject> satelites = new();

    private void Awake()
    {
        RecreateSatelites();
        direction = rotationDirection == RotationDirection.Clockwise ? -1 : 1;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        angle += rotationSpeed * deltaTime;
        for(int i = 0; i < satelites.Count; i++)
        {
            var angle_offset = 0f;
            switch (dispersionType)
            {
                case DispersionType.Even:
                    angle_offset = GetOffsetWithEvenDispersion(i);
                    break;
                case DispersionType.Close:
                    angle_offset = GetOffsetWithCloseDispersion(i);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var pos = new Vector3(0, 0, 0);
            pos.x = Mathf.Cos(angle + angle_offset) * obritRadius;
            pos.z = Mathf.Sin(angle + angle_offset) * obritRadius;
            satelites[i].transform.localPosition = pos;
        }
    }

    private float GetOffsetWithEvenDispersion(int i)
    {
        return MathF.PI * 2 / sateliteCount * i;
    }

    private float GetOffsetWithCloseDispersion(int i)
    {
        return inbetweenDistance * i;
    }

    //I was going to make the configuration live-updatable
    //but turned out that unity don't have built-in property changed attribute/event
    //that works with editor, and i'm not gonna copy other's code just for that
    private void RecreateSatelites()
    {
        foreach (var satelite in satelites) 
            Destroy(satelite);
        for (int i = 0; i < sateliteCount; i++)
        {
            var satelite  = Instantiate(satelitePrefab, transform);
            satelites.Add(satelite);
        }
    }
}
