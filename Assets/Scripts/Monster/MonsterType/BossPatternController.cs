using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController : MonoBehaviour
{
    [SerializeField]
    GameObject leftArm;
    [SerializeField]
    GameObject rightArm;
    [SerializeField]
    GameObject missileLauncher;

    public float patternCooldown = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        //StartLeftArmAttack();
        Invoke(nameof(StartPattern), 2f);
    }
    void StartPattern()
    {
        StartCoroutine(RandomPatternLoop());
    }
    void StartLeftArmAttack()
    {
        leftArm.GetComponent<LeftArmAttack>().FireBullets();
    }
    void StartRightArmAttack()
    {
        rightArm.GetComponent<RightArmAttack>().FireBullets();
    }
    // Update is called once per frame
    IEnumerator RandomPatternLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(patternCooldown);

            int rnd = Random.Range(0, 3); // 0:왼손, 1:오른손, 2:미사일
            switch (rnd)
            {
                case 0:
                    StartLeftArmAttack();
                    break;
                case 1:
                    StartRightArmAttack();
                    break;
                    /*
                case 2:
                    missileLauncherAttack.FireMissiles();
                    break;*/
            }
        }
    }
}
