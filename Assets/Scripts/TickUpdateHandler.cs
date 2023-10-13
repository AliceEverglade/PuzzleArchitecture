using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickUpdateHandler : MonoBehaviour
{
    [SerializeField] private bool active;
    private bool backendActive;

    [Range(0.1f,100f)]
    [SerializeField] private float TickSpeed;

    public static event Action TickPing;
    // Start is called before the first frame update
    void Start()
    {
        backendActive = true;
        StartCoroutine(TickUpdater());
    }

    IEnumerator TickUpdater()
    {
        while (backendActive)
        {
            if (active)
            {
                TickPing?.Invoke();
            }
            yield return new WaitForSeconds(1 / TickSpeed);
        }
    }

    public float GetTickSpeed()
    {
        return TickSpeed;
    }
}
