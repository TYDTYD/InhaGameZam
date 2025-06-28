using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem bulletTrail;

    public static void PlayParticle(ParticleSystem particlePrefab, Vector2 position, Quaternion rotation)
    {
        ParticleSystem ps = Instantiate(particlePrefab, position, rotation);
        ps.Play();
        Destroy(ps.gameObject,ps.main.duration+ps.main.startLifetime.constantMax);
    }
}
