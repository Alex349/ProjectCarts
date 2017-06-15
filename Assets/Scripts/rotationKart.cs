using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rotationKart : MonoBehaviour {

    public Transform target;
    public Terrain terrain;
    public Terrain CP1, CP2, CP3, CP4, CP5, CP6, CP7, CP8, CP8_1, CP9, CP10, CP11;
    private NavMeshAgent agent;
    private Quaternion lookRotation;
    public float deltaRotation;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();  //< cache NavMeshAgent component
        agent.updateRotation = false;          //< let us control the rotation explicitly
        lookRotation = new Quaternion (0,0,0,0);     //< set original rotation
    }

    Vector3 GetTerrainNormal()
    {
        Vector3 terrainLocalPos = transform.position - terrain.transform.position;
        Vector2 normalizedPos = new Vector2(terrainLocalPos.x / terrain.terrainData.size.x,
                                            terrainLocalPos.z / terrain.terrainData.size.z);
        return terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);
    }
    void Update()
    {

        Vector3 normal = GetTerrainNormal();
        Vector3 direction = agent.steeringTarget - transform.position;
        direction.y = 0.0f;

        if (direction.magnitude > 0.1f || normal.magnitude > 0.1f)
        {
            Quaternion qLook = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion qNorm = Quaternion.FromToRotation(Vector3.up, normal );
            lookRotation = qNorm * qLook ;
        }
        //soften the orientation
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * deltaRotation);

    }

    void OnTriggerStay (Collider theTerrainCol)
    {
        if (theTerrainCol.gameObject.name == "CP1")
        {
            terrain = CP1;
        }
        if (theTerrainCol.gameObject.name == "CP2")
        {
            terrain = CP2;
        }
        if (theTerrainCol.gameObject.name == "CP3")
        {
            terrain = CP3;
        }
        if (theTerrainCol.gameObject.name == "CP4")
        {
            terrain = CP4;
        }
        if (theTerrainCol.gameObject.name == "CP5")
        {
            terrain = CP5;
        }
        if (theTerrainCol.gameObject.name == "CP6")
        {
            terrain = CP6;
        }
        if (theTerrainCol.gameObject.name == "CP7")
        {
            terrain = CP7;
        }
        if (theTerrainCol.gameObject.name == "CP8")
        {
            terrain = CP8;
        }
        if (theTerrainCol.gameObject.name == "CP8.1")
        {
            terrain = CP8_1;
        }
        if (theTerrainCol.gameObject.name == "CP9")
        {
            terrain = CP9;
        }
        if (theTerrainCol.gameObject.name == "CP10")
        {
            terrain = CP10;
        }
        if (theTerrainCol.gameObject.name == "CP11")
        {
            terrain = CP11;
        }

    }

}


























//   public NavMeshAI navmeshAI;

//   public Transform backLeft;
//   public Transform backRight;
//   public Transform frontLeft;
//   public Transform frontRight;

//   public RaycastHit hit;

//   public RaycastHit lr;
//   public RaycastHit rr;
//   public RaycastHit lf;
//   public RaycastHit rf;

//  public Vector3 a ;
//  public Vector3 b ;
//  public Vector3 c ;
//  public Vector3 d ;

//   void Start ()
//   {
//}

//void Update ()
//   {
//       Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
//       //Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
//       //Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
//       //Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);


//       // Get the vectors that connect the raycast hit points

//       a = rr.point - lr.point;
//       b = rf.point - rr.point;
//       c = lf.point - rf.point;
//       d = rr.point - lf.point;


//       // Get the normal at each corner

//       //Vector3 crossBA = Vector3.Cross(b, a);
//       //Vector3 crossCB = Vector3.Cross(c, b);
//       //Vector3 crossDC = Vector3.Cross(d, c);
//       //Vector3 crossAD = Vector3.Cross(a, d);

//       // Calculate composite normal

//       //transform.up = (crossBA + crossCB + crossDC + crossAD).normalized;

//       transform.up -= (transform.up - lr.normal) * .1f;

//       var rotation = Quaternion.LookRotation(navmeshAI.points[navmeshAI.destPoint].position - transform.position);
//       transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
//   }
