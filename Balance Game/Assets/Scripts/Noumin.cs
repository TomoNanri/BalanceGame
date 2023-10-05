using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noumin : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 0.5f;
    [SerializeField]
    private Vector3 _startPoint;
    [SerializeField]
    private Vector3 _endPoint;
    private Vector3 _targetPosition;
    private GameObject _mura; 

    // Start is called before the first frame update
    void Start()
    {
        _mura = transform.parent.gameObject;
        var width = _mura.GetComponent<SpriteRenderer>().bounds.size.x;
        var position = _mura.transform.position;
        _startPoint = new Vector3(position.x - width / 3, position.y, position.z);
        _endPoint = new Vector3(position.x + width / 3, position.y, position.z);
        transform.position = _startPoint;
        _targetPosition = _endPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == _startPoint)
        {
            _targetPosition = _endPoint;
            //transform.LookAt(_targetPosition);
        }
        else if(transform.position == _endPoint)
        {
            _targetPosition = _startPoint;
            //transform.LookAt(_targetPosition);
        }
        float step = _velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }
    private void Walk()
    {
        
    }
}
