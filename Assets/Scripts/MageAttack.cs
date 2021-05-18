using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private GameObject exit;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Attack()
    {
        GameObject instanciatedMagic;
        instanciatedMagic = Instantiate(magic,
         exit.transform.position,
         gameObject.transform.rotation);
        instanciatedMagic.transform.localScale
            = new Vector3(gameObject.transform.localScale.x *
                            instanciatedMagic.transform.localScale.x,
                        gameObject.transform.localScale.y *
                            instanciatedMagic.transform.localScale.y,
                        gameObject.transform.localScale.z *
                            instanciatedMagic.transform.localScale.z);
        instanciatedMagic.tag = gameObject.tag + "Projectile";
    }
}
