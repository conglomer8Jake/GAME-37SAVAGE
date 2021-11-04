using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{

    public ParticleSystem[] ShotParticleSystems;
    public float SecondsBetweenShotSystemChanges = 5.0f;

    int CurrentShotParticleSystemIndex;
    float SecondsSinceLastSystemChange;

    // Use this for initialization
    void Start()
    {
        CurrentShotParticleSystemIndex = 0;
        SecondsSinceLastSystemChange = SecondsBetweenShotSystemChanges;

        ParticleSystem nextSystem = GetCurrentParticleSystem();
        if (nextSystem != null)
        {
            nextSystem.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SecondsSinceLastSystemChange -= Time.deltaTime;
        if (SecondsSinceLastSystemChange <= 0.0f)
        {
            SecondsSinceLastSystemChange = SecondsBetweenShotSystemChanges;

            ParticleSystem previousSystem = GetCurrentParticleSystem();
            if (previousSystem != null)
            {
                previousSystem.Stop();
            }

            CurrentShotParticleSystemIndex++;
            if (CurrentShotParticleSystemIndex >= ShotParticleSystems.Length)
            {
                CurrentShotParticleSystemIndex = 0;
            }

            ParticleSystem nextSystem = GetCurrentParticleSystem();
            if (nextSystem != null)
            {
                nextSystem.Play();
            }
        }
    }

    ParticleSystem GetCurrentParticleSystem()
    {
        if (CurrentShotParticleSystemIndex < ShotParticleSystems.Length)
        {
            return ShotParticleSystems[CurrentShotParticleSystemIndex];
        }

        return null;
    }
}
