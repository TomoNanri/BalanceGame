using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuukei : MonoBehaviour
{
    public List<Sprite> Sprites = new();
    public enum Kisetu { }
    private Image _fukei;

    // Start is called before the first frame update
    void Start()
    {
        _fukei = transform.Find("Canvas/Panel").GetComponent<Image>();
        SetFuukei(MonthStateController.Kisetsu.Fuyu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFuukei(MonthStateController.Kisetsu season)
    {
        _fukei.sprite = Sprites[(int)season];
    }
}
