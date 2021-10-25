using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] float jumpSpeed;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        //StartCoroutine(ShowTime());

    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Saltar();
        Falling();
        Fire();

    }

    IEnumerator ShowTime()
    {
        int count = 0;
        while(true)
        {
            yield return new WaitForSeconds(1f);
            count++;
            Debug.Log("Tiempo: "+count);
        }
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
            myAnimator.SetLayerWeight(1, 0);

    }

    void Mover()
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

    void Saltar()
    {
        if (isGrounded() && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", false);
            myAnimator.SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            }
        }
    }

    void Falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("jumping"))
            myAnimator.SetBool("falling", true);
    }

    bool isGrounded()
    {
        //return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f,LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.2f) * -1), Color.red);
        return myRaycast.collider != null;
    }

    void AfterTakeOfEvent()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("falling", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
