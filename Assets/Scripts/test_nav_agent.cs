using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_nav_agent : MonoBehaviour {
    //test
    public test_nav navMngr;
    public JumpLink jumplink;
    public int cntJN;

    public float minDistance = 0.05f;
    public bool isGrounded = true;
    public Vector2 currentJN;
    public Vector2 nextJN;
    public Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
    {
        cntJN = 0;
        jumplink = navMngr.result[0];
        isGrounded = false;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        nextJN = transform.position;
        currentJN = transform.position;
        //StartCoroutine(IterateNodeTimer(1f, jumplink.jumpNodes.Length));
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (!isGrounded)
        {
            //Debug.Log(((Vector2)transform.position - nextJN).magnitude);
            transform.position = Vector2.MoveTowards(transform.position, nextJN, 0.15f);
            if (((Vector2)transform.position - nextJN).magnitude <= minDistance)
            {
                IterateNode();
            }
        }
	}

    IEnumerator IterateNodeTimer(float time,int length)
    {
        rigidbody.isKinematic = true;
        transform.position = currentJN;
        cntJN = 0;
        while(cntJN < length)
        {
            print(cntJN);
            cntJN++;
            IterateNode();
            yield return new WaitForSeconds(time);
        }
        rigidbody.isKinematic = false;
        isGrounded = true;    
    }

    void IterateNode()
    {   
        if(cntJN < jumplink.jumpNodes.Length - 1)
        {
            currentJN = nextJN;
            nextJN = jumplink.jumpNodes[cntJN + 1];
            cntJN++;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(nextJN, 0.5f);
        /*
        foreach (Vector2 each in jumplink.jumpNodes)
            Gizmos.DrawWireSphere(each, 0.5f);
        */
    }
}
