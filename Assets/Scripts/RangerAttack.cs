using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    // private float time = 0;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private float rotationTime = 1;
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Attack()
    {
        GameObject instanciatedArrow;
        RotateTo(40);
        instanciatedArrow = Instantiate(arrow, 
            exit.transform.position,
            Quaternion.Euler(
                new Vector3(
                    gameObject.transform.localRotation.eulerAngles.x,
                    gameObject.transform.localRotation.eulerAngles.y - 40,
                    gameObject.transform.localRotation.eulerAngles.z
                )));
        instanciatedArrow.transform.localScale
            = new Vector3(  gameObject.transform.localScale.x * 
                                instanciatedArrow.transform.localScale.x,
                            gameObject.transform.localScale.y * 
                                instanciatedArrow.transform.localScale.y,
                            gameObject.transform.localScale.z * 
                                instanciatedArrow.transform.localScale.z);
        instanciatedArrow.tag = gameObject.tag + "Projectile";
    }

    void RotateTo(float yAngle)
    {
        Debug.Log("Rotate: " + yAngle);
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles +
            new Vector3(transform.rotation.x, transform.rotation.y + yAngle,
                transform.rotation.z));
        for (var t = 0f; t < rotationTime; t += Time.deltaTime / 1f)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
        }
    }
}
