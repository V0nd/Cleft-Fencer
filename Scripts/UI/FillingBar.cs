using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillingBar : MonoBehaviour
{
    public FloatValue value;
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBar();
    }

    void ChangeBar()
    {
        bar.fillAmount = value.currentValue/10;
    }
}
