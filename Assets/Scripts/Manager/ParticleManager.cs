using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static void PlayParticle(GameObject particleObj, Vector2 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(particleObj, position, rotation);        
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(obj, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}