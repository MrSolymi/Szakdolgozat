using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform _particleContainer;

    protected override void Awake()
    {
        base.Awake();
        
        _particleContainer = GameObject.FindWithTag("ParticleContainer").transform;
    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation) => Instantiate(particlePrefab, position, rotation, _particleContainer);
    
    public GameObject StartParticles(GameObject particlePrefab) => StartParticles(particlePrefab, transform.position, Quaternion.identity);

    public GameObject StartParticlesRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }
    //TODO: If I wanna have a function to spawn a particle at a specific position, I should add it here, now it's only spawning at the center of the player's position
}
