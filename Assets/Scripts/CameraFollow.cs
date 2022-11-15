using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject _followObject;

    [SerializeField] private Vector3 _startOffset;

    private void Start() => _startOffset = _followObject.transform.position - transform.position;

    private void Update() => KeepDistance();

    private void KeepDistance() => transform.position = _followObject.transform.position - _startOffset;


}
