using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField] private Vector3 offset;
    private float speed = 5f;
    private Transform cameraTransform;
	private void Start()
	{
		cameraTransform = transform;
	}

	void LateUpdate()
    {
        if(target != null)
		{
            Vector3 destination = target.position + offset;
			cameraTransform.position = Vector3.Lerp(transform.position, destination + offset, Time.deltaTime * speed);
		}
    }

    public void SetTarget(Transform obj)
	{
        target = obj;
	}
}
