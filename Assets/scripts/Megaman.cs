using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject bullet;

    Animator myAnimator;
    Rigidbody2D myBody;
    Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Fall();
        Fire();

    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            //shoots!
            newBullet.GetComponent<Bullet>().Fire(transform.localScale.x > 0 ? 1 : -1);
        }
    }

    void Move()
    {
        float mov = Input.GetAxis("Horizontal");
        if (mov != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            myAnimator.SetBool("running", true);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));

        }
        else
            myAnimator.SetBool("running", false);
    }
    void Fall()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("jumping"))
            myAnimator.SetBool("falling", true);

    }

    void Jump()
    {
        if (isGrounded() && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", false);
            myAnimator.SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);

            }
        }
            
        
    }

    public void AfterTakeOf()
    {
        myAnimator.SetBool("falling", true);
        myAnimator.SetBool("jumping", false);
        Debug.Log("after takeof");
    }

    private bool isGrounded()
    {
        //return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D ray = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + 0.2f), Color.green);
        return (ray.collider != null);
    }

}
