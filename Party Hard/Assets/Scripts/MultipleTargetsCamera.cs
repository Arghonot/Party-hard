using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetsCamera : MonoBehaviour
{
    public List<Transform> Targets;
    public Vector3 offset;
    public float smoothTime;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float ZoomLimiter = 50f;

    Camera cam;
    Vector3 velocity;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (Targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();

    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / ZoomLimiter);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(Targets[0].position, Vector3.zero);

        for (int i = 0; i < Targets[i].childCount; i++)
        {
            bounds.Encapsulate(Targets[i].position);

        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (Targets.Count == 1)
        {
            return Targets[0].position;
        }

        var bounds = new Bounds(Targets[0].position, Vector3.zero);

        for (int i = 0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }

        return bounds.center;
    }
}
