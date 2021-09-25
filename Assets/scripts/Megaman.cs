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

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Saltar();

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
        bool isGrounded = pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGrounded)
        {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            }
        }
        else
            myAnimator.SetBool("falling", true);
        
    }

}
