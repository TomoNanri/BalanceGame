using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class BadgeScript : MonoBehaviour
{
    [SerializeField]
    private float _alphaLow = 0.7f; 
    private GameManager _gm;
    private Tokuten _tokutenObject;
    private GameObject _title;
    private GameObject _choiText;
    private Image _image;
    private Color _originColor;
    private Color _inactiveColor;
    private int _thresholdValue;
    private bool _isActive;

    // Start is called before the first frame update
    void Start()
    {
        _gm = FindAnyObjectByType<GameManager>();
        _gm.InitializeHandler += ResetBadge;

        // バッジの表示閾値をバッジ名から求める
        _thresholdValue = 10000 * int.Parse(Regex.Replace(name, @"[^0-9]", ""));
        Debug.Log($"[{name}] _thresholdValue={_thresholdValue}");

        // 石高変動イベントの Listen 開始
        _tokutenObject = GameObject.Find("Tokuten").GetComponent<Tokuten>();
        _tokutenObject.KokudakaEvent += EventHandler;

        // バッジのコンポーネントを取得しておく
        _image = GetComponent<Image>();
        _title = transform.Find("Title").gameObject;
        _choiText = transform.Find("ChoiText").gameObject;

        // 元の色とインアクティブ時の色を保存
        _originColor = _image.color;
        _inactiveColor = _originColor - new Color(0, 0, 0, _alphaLow);

        // 開始時は全バッジインアクティブ
        Inactivate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ResetBadge()
    {
        Inactivate();
    }
    private void EventHandler(KokudakaEventArgs arg)
    {
        if (!_isActive)
        {
            if (arg.Kokudaka >= _thresholdValue)
                Activate();
        }
    }
    public void Inactivate()
    {
        _isActive = false;
        _image.color = _inactiveColor;
        _choiText.SetActive(false);
    }
    public void Activate()
    {
        _isActive = true;
        StartCoroutine(BlinkingOn(0.5f, 4));
    }
    private IEnumerator BlinkingOn(float sec,int count)
    {
        for(int i=0; i<count; i++)
        {
            _image.color = i%2==0?_originColor:_inactiveColor;
            yield return new WaitForSeconds(sec);
        }
        _image.color = _originColor;
        yield return new WaitForSeconds(sec);
        _choiText.SetActive(true);
    }
}
