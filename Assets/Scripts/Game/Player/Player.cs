using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] Animator animator;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHorizontalLimit(float limit)
    {
        controller.SetHorizontalLimit(limit);
    }

    void OnCollisionEnter(Collision other)
    {
        controller.ShouldRun = false;
    }
}
