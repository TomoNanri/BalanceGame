using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeScript : MonoBehaviour
{
    private GameObject _ChoiText;
    private Image _image;
    private Color _originColor;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _originColor = _image.color;
        _ChoiText = transform.Find("ChoiText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActive(bool isActive)
    {
        if (_ChoiText == null)
        {
            _ChoiText = transform.Find("ChoiText").gameObject;
        }
        if (isActive)
        {

        }
        else
        {

        }
        _ChoiText.SetActive(isActive);
    }
}
