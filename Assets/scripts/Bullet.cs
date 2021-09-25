using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Animator myAnimator;
    Rigidbody2D myBody;
    CircleCollider2D myCollider;
    // Start is called before the first frame update
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImpactEnds()
    {
        Destroy(gameObject);
    }

    public void Fire(int dir)
    {
        myBody.velocity = new Vector3(speed * Time.deltaTime * dir, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
        {
            myBody.velocity = Vector3.zero;
            myCollider.enabled = false;
            myAnimator.SetTrigger("impact");
        }
    }
}
