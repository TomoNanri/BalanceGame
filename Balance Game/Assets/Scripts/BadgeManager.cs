using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    [SerializeField]
    private bool _isActive100Man = false;
    [SerializeField]
    private bool _isActive70Man = false;
    [SerializeField]
    private bool _isActive50Man = false;
    [SerializeField]
    private bool _isActive30Man = false;
    [SerializeField]
    private bool _isActive20Man = false;
    [SerializeField]
    private bool _isActive10Man = false;
    [SerializeField]
    private bool _isActive5Man = false;
    [SerializeField]
    private bool _isActive1Man = false;

    private GameObject _Panel100Man;
    private GameObject _Panel70Man;
    private GameObject _Panel50Man;
    private GameObject _Panel30Man;
    private GameObject _Panel20Man;
    private GameObject _Panel10Man;
    private GameObject _Panel5Man;
    private GameObject _Panel1Man;
    private Color _originColor;

    // Start is called before the first frame update
    void Start()
    {
        _Panel100Man = transform.Find("Canvas/Panel100Man").gameObject;
        SetActive(_Panel100Man, _isActive100Man);
        _Panel70Man = transform.Find("Canvas/Panel70Man").gameObject;
        SetActive(_Panel70Man, _isActive70Man);
        _Panel50Man = transform.Find("Canvas/Panel50Man").gameObject;
        SetActive(_Panel50Man, _isActive50Man);
        _Panel30Man = transform.Find("Canvas/Panel30Man").gameObject;
        SetActive(_Panel30Man, _isActive30Man);
        _Panel20Man = transform.Find("Canvas/Panel20Man").gameObject;
        SetActive(_Panel20Man, _isActive20Man);
        _Panel10Man = transform.Find("Canvas/Panel10Man").gameObject;
        SetActive(_Panel10Man, _isActive10Man);
        _Panel5Man = transform.Find("Canvas/Panel5Man").gameObject;
        SetActive(_Panel5Man, _isActive5Man);
        _Panel1Man = transform.Find("Canvas/Panel1Man").gameObject;
        SetActive(_Panel1Man, _isActive1Man);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetActive(GameObject obj, bool isActive)
    {
        BadgeScript badge = obj.GetComponent<BadgeScript>();
        badge.SetActive(isActive);
    }
}
