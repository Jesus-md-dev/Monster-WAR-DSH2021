using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttack : MonoBehaviour
{
    public float speed = 1;
    private Animator animator;
    private Quaternion startAngle;
    private Vector3 startPosition;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        startAngle = gameObject.transform.rotation;
        startPosition = gameObject.transform.position;
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
