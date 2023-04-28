using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform GrabPoint;
    [SerializeField] private Transform RayPoint;
    [SerializeField] private float RayDistance;

    public GameObject GrabbedObject;
    private int LayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        LayerIndex = LayerMask.NameToLayer("GrabObjects");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(RayPoint.position, transform.right, RayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == LayerIndex)
        {
            if (GrabbedObject == null && Input.GetKeyDown(KeyCode.E))
            {
                GrabbedObject = hitInfo.collider.gameObject;
                GrabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                GrabbedObject.GetComponent<CircleCollider2D>().enabled = false;
                GrabbedObject.transform.position = GrabPoint.position;
                GrabbedObject.transform.SetParent(transform);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                GrabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                GrabbedObject.GetComponent<CircleCollider2D>().enabled = true;
                GrabbedObject.transform.SetParent(null);
                GrabbedObject = null;
            }
        }
    }
}
