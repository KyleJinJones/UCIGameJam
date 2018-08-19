using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLink
{
    public Vector2 position;
    public Vector2 direction;
    public bool isLeft;
    public int agentType;
    public Vector2[] jumpNodes;
    public JumpLink(Vector2 pos, Vector2 dir, bool isLeft, int agent)
    {
        position = pos;
        direction = dir;
        this.isLeft = isLeft;
        agentType = agent;
    }
}
public class test_nav : MonoBehaviour
{
    public List<BoxCollider2D> colliders = new List<BoxCollider2D>(); 
    //public List<Vector3> jumpPoints = new List<Vector3>();
    public List<Vector3> leftJumpPoints = new List<Vector3>();
    public List<Vector3> rightJumpPoints = new List<Vector3>();
    public List<Vector3> test_points = new List<Vector3>();
    public List<Vector3> test_dirs = new List<Vector3>();
    public Vector3 hitpoint;

    //agent params
    public Vector3 jumpVector;
    public float jumpSpeedMax = 1f;
    public float jumpSpeedMin = 0.1f;
    public float speedMax = 1f;
    public float speedMin = 0.1f;

    //simulation
    public float jumpOffset = 0.5f;
    public float jumpOffsetVerti = 0.1f;
    public float searchOffset = 1f;
    public float distanceDelta = 0.1f;
    public float interval = 0.1f;
    public int maxIteration = 10;
    public List<JumpLink> result = new List<JumpLink>();

    //result
    public List<Vector2> navPoints = new List<Vector2>();


	// Use this for initialization
	void Awake () {
        foreach(Collider2D collider in colliders)
        {
            float x = collider.bounds.extents.x;
            float y = collider.bounds.extents.y;
            float z = collider.bounds.extents.z;
            leftJumpPoints.Add(collider.bounds.center + new Vector3(x - jumpOffset, y + jumpOffsetVerti, z));
            rightJumpPoints.Add(collider.bounds.center + new Vector3(-x + jumpOffset, y + jumpOffsetVerti, z));
        }
        foreach (Vector3 point in leftJumpPoints)
        {
            CalcuJumpLink(point, jumpVector, true);
        }
        foreach (Vector3 point in rightJumpPoints)
        {
            CalcuJumpLink(point, jumpVector, false);
        }
    }

    void CalcuJumpLink(Vector3 offPoint,Vector3 startSpeed,bool isLeft)
    {
        List<Vector2> jumpNodes = new List<Vector2>();

        Vector2 tempPoint = (Vector2)offPoint;
        Vector2 speed = (Vector2)startSpeed;
        if (!isLeft)
            speed = new Vector2(-speed.x,speed.y);
        int cnt = maxIteration;
        while (cnt > 0)
        {
            jumpNodes.Add(tempPoint);
            test_points.Add((Vector3)tempPoint);
            test_dirs.Add((Vector3)speed * interval);

            RaycastHit2D hit;
            hit = Physics2D.Raycast(tempPoint, speed * interval, speed.magnitude * interval);
            if (hit)
            {
                jumpNodes.Add(tempPoint);
                test_points.Add((Vector3)tempPoint);
                test_dirs.Add((Vector3)speed * interval);
                if (hit.normal == Vector2.up)
                {
                    result.Add(new JumpLink(offPoint, startSpeed, isLeft,0));
                    print("goal");
                } 
                else
                    print("side");
                hitpoint = (Vector3)hit.point;
                break; 
            }
            tempPoint += speed * interval;
            speed += Physics2D.gravity * interval;

            RaycastHit2D hitVerti;
            hitVerti = Physics2D.Raycast(tempPoint + speed * interval, Vector2.down, jumpOffsetVerti);
            if (hitVerti)
            {
                jumpNodes.Add(tempPoint);
                test_points.Add((Vector3)tempPoint);
                JumpLink jumplink = new JumpLink(offPoint, startSpeed, isLeft, 0);
                jumplink.jumpNodes = jumpNodes.ToArray();
                result.Add(jumplink);
                print("goal_verti");
                hitpoint = (Vector3)hit.point;
                
                break;
            }
            cnt--;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (test_points.Count <= 0)
            return;
        for (int i = 0; i < test_dirs.Count; i++)
        {
            Gizmos.DrawSphere(test_points[i], 0.1f);
            Gizmos.DrawRay(test_points[i], test_dirs[i]);
        }
        //Gizmos.DrawRay(jumpPoints[0], jumpVector);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitpoint, 0.1f);
        Gizmos.color = Color.cyan;
        foreach (JumpLink jp in result)
        {
            Gizmos.DrawSphere(jp.position, 0.1f);
        }
    }
}
