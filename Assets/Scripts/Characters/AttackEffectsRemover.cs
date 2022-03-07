using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AttackEffectsRemover : MonoBehaviour
{
    private float _delay;

    private void Start()
    {
        _delay = GetComponent<ParticleSystem>().main.duration;
        Destroy(this.gameObject, _delay);
    }
}
