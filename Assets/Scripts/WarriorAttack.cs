using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttack : MonoBehaviour
{
    public float speed = 1;
    private float time = 0;
    private Animator animator;
    private Quaternion startAngle;
    private Vector3 startPosition;
    int i = 3;
    int lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        startAngle = gameObject.transform.rotation;
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // animator.SetTrigger("Death");
        if ((int)time % i == 0 && (int)time > lastTime)
        {
            lastTime = (int)time;
            animator.SetTrigger("Attack");
        }
    }

    void AttackRotation() { StartCoroutine(Rotate(startAngle)); }

    IEnumerator Rotate(Quaternion angle)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(startAngle.eulerAngles);
        transform.position = Vector3.MoveTowards(transform.position,
                startPosition, speed / Time.deltaTime);
        for (var t = 0f; t < 1; t += Time.deltaTime / 1f)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            
            yield return null;
        }
    }
}
