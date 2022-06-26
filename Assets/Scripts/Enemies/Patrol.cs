using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header ("Granice poruszania")]
    [SerializeField] private Transform lewo;
    [SerializeField] private Transform prawo;

    [Header ("Zachowanie przeciwnika")]
    [SerializeField] private Transform przeciwnik;

    [Header ("Parametry poruszania")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool wlewo;

    [Header("Postoj")]
    [SerializeField] private float postoj;
    private float czaspostoju;

    [Header("Animacja")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = przeciwnik.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (wlewo)
        {
            if (przeciwnik.position.x >= lewo.position.x)
                MoveInDirection(-1);
            else
                zmianastrony();
        }
        else
        {
            if (przeciwnik.position.x <= prawo.position.x)
                MoveInDirection(1);
            else
                zmianastrony();
        }
    }
    private void zmianastrony()
    {
        anim.SetBool("moving", false);

        czaspostoju += Time.deltaTime;

        if(czaspostoju > postoj)
            wlewo = !wlewo;
    }
    private void MoveInDirection(int _direction)
    {
        czaspostoju = 0;
        anim.SetBool("moving", true);

        przeciwnik.localScale = new Vector3(Mathf.Abs(initScale.x) * -_direction, initScale.y, initScale.z);

        przeciwnik.position = new Vector3(przeciwnik.position.x + Time.deltaTime * _direction * speed, przeciwnik.position.y, przeciwnik.position.z);
    }
}
