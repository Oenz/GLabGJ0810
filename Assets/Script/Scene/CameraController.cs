using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    List<Transform> _targets;
    [SerializeField]Vector3 _offset;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _targets =  FindObjectsOfType<Player>().Select(x => x.transform).ToList();
    }

    void Update()
    {
        Vector3 targetPos = Vector3.zero;
        foreach(Transform t in _targets)
        {
            targetPos += t.position;
        }
        targetPos /= _targets.Count();
        transform.position = targetPos + _offset;
    }
}
