using System.Collections;
using System.Threading;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Zycie")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Klatki")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Komponenty")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invunerability());
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");

                foreach (Behaviour component in components)
                    component.enabled = false;
                
                dead = true;

                transform.position = GameObject.FindWithTag("StartPos").transform.position;

                foreach (Behaviour component in components)
                        component.enabled = true;

                anim.SetTrigger("walk");
                currentHealth = 3;
                dead = false;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
