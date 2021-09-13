using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mov = Input.GetAxis("Horizontal");
        if(mov != 0)
        {
            myAnimator.SetBool("running", true);

        }
        else
            myAnimator.SetBool("running", false);

        transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));

    }
}
