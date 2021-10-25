using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector2.Distance(player.transform.position, transform.position) <= range)
            Debug.Log("Persigalo!");*/

        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
            Debug.Log("persigalo");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, range);
        //Gizmos.DrawLine(transform.position, player.transform.position);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
