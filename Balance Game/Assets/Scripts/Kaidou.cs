using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Kaidou : MonoBehaviour
{
    public enum Kaidoumei { Kitakaidou = 0, Higasikaidou = 1, Minamikaidou = 2, Nisikaidou = 3 };
    [SerializeField]
    private GameObject _otonosama;
    [SerializeField]
    private GameObject _responseNoumin;

    private Oshiro _oshiro;

    private bool isShisatsuStart = false;
    private List<Vector3> _otonosamaPosition = new List<Vector3>();
    private List<Mura> _responseMuraList = new List<Mura>();

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        float width = GetComponent<SpriteRenderer>().bounds.size.x;

        _oshiro = FindAnyObjectByType<Oshiro>();
        _responseMuraList.Clear();


        switch (name)
        {
            case "Kitakaidou":
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y - height / 3, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y + height / 3, pos.z));
                _responseMuraList.Add(GameObject.Find("/UshitoraMura").GetComponent<Mura>());
                _responseMuraList.Add(GameObject.Find("/InuiMura").GetComponent<Mura>());
                Debug.Log($"[{name}.START] _resposeMuraList[0]={_responseMuraList[0]}");

                break;

            case "Minamikaidou":
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y + height / 3, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y - height / 3, pos.z));
                _responseMuraList.Add(GameObject.Find("/TatsumiMura").GetComponent<Mura>());
                _responseMuraList.Add(GameObject.Find("/HitsujisaruMura").GetComponent<Mura>());
                Debug.Log($"[{name}.START] _resposeMuraList[0]={_responseMuraList[0]}");

                break;

            case "Higasikaidou":
                _otonosamaPosition.Add(new Vector3(pos.x - width / 3, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x + width / 3, pos.y, pos.z));
                _responseMuraList.Add(GameObject.Find("/InuiMura").GetComponent<Mura>());
                _responseMuraList.Add(GameObject.Find("/HitsujisaruMura").GetComponent<Mura>());
                Debug.Log($"[{name}.START] _resposeMuraList[0]={_responseMuraList[0]}");
                
                break;

            case "Nisikaidou":
                _otonosamaPosition.Add(new Vector3(pos.x + width / 3, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x, pos.y, pos.z));
                _otonosamaPosition.Add(new Vector3(pos.x - width / 3, pos.y, pos.z));
                _responseMuraList.Add(GameObject.Find("/TatsumiMura").GetComponent<Mura>());
                _responseMuraList.Add(GameObject.Find("/UshitoraMura").GetComponent<Mura>());
                Debug.Log($"[{name}.START] _resposeMuraList[0]={_responseMuraList[0]}");
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShisatsuStart)
        {
            isShisatsuStart = false;
            StartCoroutine(Kaiwa(3f, 0.5f, 1f));
        }
    }
    IEnumerator Kaiwa(float keepSec,float appearInterval, float rug)
    {
        Debug.Log($"[{name}] _resposeMuraList = {_responseMuraList}");
        for(int i = 0; i< _otonosamaPosition.Count; i++)
        {
            var pos = _otonosamaPosition[i];
            yield return new WaitForSeconds(appearInterval);
            var otonosama = Instantiate(_otonosama, pos, Quaternion.identity);
            var otonosamaSerifu = otonosama.transform.Find("Canvas/Panel/Serifu").GetComponent<TextMeshProUGUI>();

            if (i != 0)
            {
                otonosamaSerifu.SetText("景気は如何でござるか？");
                foreach(Mura m in _responseMuraList)
                {
                    Debug.Log($"[{name}] m={m} m.name={m.name}");
                    var mpos = m.transform.position;
                    var responseMessage = m.Response();
                    yield return new WaitForSeconds(rug);

                    var noumin = Instantiate(_responseNoumin, mpos, Quaternion.identity);
                    var serifu = noumin.transform.Find("Canvas/Panel/Serifu").GetComponent<TextMeshProUGUI>();
                    serifu.SetText(responseMessage);
                    m.Speak(responseMessage);
                    yield return new WaitForSeconds(keepSec);
                    Destroy(noumin);
                }
            }
            else
            {
                otonosamaSerifu.SetText("<color=#0000ff>おしのびでござる</color>");
                yield return new WaitForSeconds(keepSec);
            }

            Destroy(otonosama);
        }
        _oshiro.RaiseShisakuEnd(this, EventArgs.Empty);
    }
    public void StartShisatsu()
    {
        isShisatsuStart = true;
    }
}
