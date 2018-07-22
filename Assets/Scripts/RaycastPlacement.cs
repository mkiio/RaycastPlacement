using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlacement : MonoBehaviour {

    private const float RAYCAST_DISTANCE = 100f;

    public LayerMask layerMask;
    public Transform origin;

    [SerializeField]
    private GameObject target;
    private float offset;

    private void Start()
    {
        SetTarget(target);
    }

    // Update is called once per frame
    void Update ()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (target == null) return;

        RaycastHit hit;

        if (Physics.Raycast(origin.position, origin.forward, out hit, RAYCAST_DISTANCE, layerMask))
        {
            target.transform.rotation = Quaternion.LookRotation(hit.normal);
            target.transform.position = hit.point + (hit.normal * offset);
       
        }
    }

    public void SetTarget(GameObject _target = null)
    {
        Collider targetCollider = target.GetComponent<Collider>();

        if (targetCollider == null)
        {
            enabled = false;
            Debug.LogWarning("Target for raycast placement does not have a collider, placement disabled.");
            return;
        }

        target = _target;
        target.transform.rotation = Quaternion.identity; //Reset the rotation. Bounds are world axis aligned and are impacted by rotation.
        offset = targetCollider.bounds.extents.z;  //Note: using Renderer may be more flexible, in this scenario we're assuming we're going to have a collider.
    }

    public void SetOrigin(Transform _origin)
    {
        origin = _origin;
    }
}
