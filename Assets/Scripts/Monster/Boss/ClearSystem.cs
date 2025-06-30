using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSystem : MonoBehaviour
{
    [SerializeField] MonsterStats bossHealth;
    bool isDead = false;
    void Update()
    {
        if (bossHealth.currentHp <=0 ) 
        {
            if (!isDead)
            {
                isDead = true;
                SceneManager.LoadScene("Clear");
            }
        }
    }
}
