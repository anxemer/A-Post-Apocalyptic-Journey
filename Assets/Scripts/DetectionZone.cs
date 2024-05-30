using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedCollider = new List<Collider2D>();
    public List<Collider2D> attackZone = new List<Collider2D>();
    private string tagTarget = "Player";
 



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == tagTarget)
        {
            detectedCollider.Add(collision);

        }
        //attackZone.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == tagTarget)
        {
            detectedCollider.Remove(collision);

        }
        //attackZone.Remove(collision);

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    attackZone.Add(collision.collider);
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    attackZone.Remove(collision.collider);
    //}
}
