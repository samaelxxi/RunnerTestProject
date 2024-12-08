using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class TransformPoolable : Poolable<TransformPoolable>
{
    public Transform Transform { get; private set; }

    void Awake()
    {
        Transform = transform;
    }
}
