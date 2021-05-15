using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    private float time = 0;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject exit;
    private Animator animator;
    int i = 3;
    int lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // animator.SetTrigger("Death");
        if((int) time % i == 0 && (int) time > lastTime)
        {
            time += Time.deltaTime;
            lastTime = (int) time;
            animator.SetTrigger("Attack");      
        }
    }

    void Attack()
    {
        
        GameObject instanciatedArrow;

        instanciatedArrow = Instantiate(arrow, 
            exit.transform.position,
            Quaternion.Euler(
                new Vector3(
                    gameObject.transform.localRotation.eulerAngles.x,
                    gameObject.transform.localRotation.eulerAngles.y - 40,
                    gameObject.transform.localRotation.eulerAngles.z
                )));
        instanciatedArrow.transform.localScale
                = new Vector3(0.1f, 0.1f, 0.1f);
        instanciatedArrow.tag = gameObject.tag + "Projectile";
    }

    IEnumerator Rotate()
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles +
            new Vector3(0, 40, 0));
        for (var t = 0f; t < 1; t += Time.deltaTime / 1f)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
