using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    private float time = 0;
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private GameObject exit;
    private Animator animator;
    int i = 3;
    int lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // animator.SetTrigger("Death");
        if((int) time % i == 0 && (int) time > lastTime)
        {
            lastTime = (int)time;
            animator.SetTrigger("Attack");      
        }
    }

    void Attack()
    {
        GameObject instanciatedMagic;
        instanciatedMagic = Instantiate(magic,
         exit.transform.position,
         gameObject.transform.rotation);
        instanciatedMagic.AddComponent<projectileMove>();
    }
}
