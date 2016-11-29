// Author: Pietro Polsinelli - http://designAGame.eu
// Twitter https://twitter.com/ppolsinelli
// All free as in free beer :-)

using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    public Transform EastBoundary;
    private Renderer ebR;
    public Transform WestBoundary;
    private Renderer wbR;
    public Transform NorthBoundary;
    private Renderer nbR;
    public Transform SouthBoundary;
    private Renderer sbR;


    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        ebR = EastBoundary.GetComponent<Renderer>();
        wbR = WestBoundary.GetComponent<Renderer>();
        nbR = NorthBoundary.GetComponent<Renderer>();
        sbR = SouthBoundary.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            Vector3 tpo = targetPos + offset;

            if (ebR.isVisible && tpo.x > transform.position.x)
                tpo = new Vector3(transform.position.x, tpo.y, tpo.z);
            else if (wbR.isVisible && tpo.x < transform.position.x)
                tpo = new Vector3(transform.position.x, tpo.y, tpo.z);

            if (nbR.isVisible && tpo.y > transform.position.y)
                tpo = new Vector3(tpo.x, transform.position.y, tpo.z);
            else if (sbR.isVisible && tpo.y < transform.position.y)
                tpo = new Vector3(tpo.x, transform.position.y, tpo.z);

            transform.position = Vector3.Lerp(transform.position, tpo, 0.25f);
        }
    }
}
