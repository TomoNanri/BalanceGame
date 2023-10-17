using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noumin : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 0.5f;
    private Vector3 _startPoint;
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
        int randomValue = Random.Range(0, 5);
        switch (randomValue)
        {
            case 0:
                transform.position = _startPoint;
                _targetPosition = _endPoint;
                break;

            case 1:
                transform.position = _endPoint;
                _targetPosition = _startPoint;
                break;

            case 2:
                transform.position = position;
                _targetPosition = _endPoint;
                break;

            default:
                transform.position = position;
                _targetPosition = _startPoint;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == _startPoint)
        {
            _targetPosition = _endPoint;
        }
        else if(transform.position == _endPoint)
        {
            _targetPosition = _startPoint;
        }
        float step = _velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }
    private void Walk()
    {
        
    }
}
