using UnityEngine;
using System.Collections;
public class AccessSystem : MonoBehaviour
{
    bool isActive = false;
    WaitForSeconds cache;
    float delay = 0.01f;
    [SerializeField] Transform block;
    [SerializeField] Transform BossDoor;
    [SerializeField] Transform BossMonster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isActive)
            {
                StartSystem();
            }            
        }
    }

    private void Awake()
    {
        cache = new WaitForSeconds(delay);
    }
    void StartSystem()
    {
        isActive = true;
        StartCoroutine(BossSystem());
    }
    IEnumerator BossSystem()
    {
        Vector3 move = new Vector3(0, -0.1f);
        while (block.transform.position.y > 15f)
        {
            block.position += move;
            yield return cache;
        }

        while (BossDoor.transform.position.y < 55f)
        {
            BossDoor.position -= move;
            yield return cache;
        }


        Vector3 monsterMove = new Vector3(-0.1f, 0);
        while (BossMonster.transform.position.x > 145f)
        {
            BossMonster.position += monsterMove;
            yield return cache;
        }
    }
}