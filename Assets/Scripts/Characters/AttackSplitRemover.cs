using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AttackSplitRemover : MonoBehaviour
{
    private float _delay;

    void Start()
    {
        _delay = GetComponent<ParticleSystem>().main.duration;
        Destroy(this.gameObject, _delay);
    }
}
