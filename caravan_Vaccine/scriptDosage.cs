using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scriptDosage : MonoBehaviour
{
    public GameObject[] show;
    public GameObject[] hide;

    public float time, currentTime;
    public bool run;
    public Scrollbar _scrollbar;
    public Image _fillVaccine, _lineSlide;

    public int selectRat;
    public Text selectText;

    public float[] statusRat;
    public float[] dosageRat;

    public Color _colorMe, _colorIdle, _colorComplate, _colorFailed;
    public Material _matRat;
    public bool changeColor, resetColor;
    public float speedChangeColor;

    public GameObject[] panelStatus;
    public GameObject[] cardioStatus;
    public int statusDosage = 3;


    void Start()
    {
        try
        {
            resetHide();
            run = true;

            ColorUtility.TryParseHtmlString("#8820FF", out _colorMe);
            _matRat.SetColor("_Color", _colorMe);
        }
        catch
        {
            Debug.Log("first run");
        }
    }


    void Update()
    {
        if (run)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                bthomeGRat();
                currentTime = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentTime = 0;
            }

            _lineSlide.fillAmount = _scrollbar.value;
            _fillVaccine.fillAmount = _scrollbar.value;
            dosageRat[selectRat] = _scrollbar.value;

            if (changeColor)
            {
                if (Mathf.Abs(dosageRat[selectRat] - statusRat[selectRat]) <= 0.1f)
                {
                    statusDosage = 0;
                    colorComplate();
                    //panelStatus[statusDosage].SetActive(true);
                }
                else
                {
                    if (dosageRat[selectRat] > statusRat[selectRat]) statusDosage = 1;
                    else statusDosage = 2;
                    colorFailed();
                    //panelStatus[statusDosage].SetActive(true);
                }
                panelStatus[statusDosage].SetActive(true);
                for(int i = 0; i < cardioStatus.Length; i++) cardioStatus[i].SetActive(false);
                cardioStatus[statusDosage].SetActive(true);
            }

            if (resetColor)
            {
                StartCoroutine(resetCol());
            }
        }
    }

    private void colorComplate()
    {
        _matRat.SetColor("_TintColor", _matRat.color = Color.Lerp(_matRat.color, _colorComplate, Time.deltaTime * speedChangeColor));
        ColorUtility.TryParseHtmlString("#2F83FF", out _colorMe);
        if (_matRat.color == _colorMe) changeColor = false;
    }
    private void colorFailed()
    {
        _matRat.SetColor("_TintColor", _matRat.color = Color.Lerp(_matRat.color, _colorFailed, Time.deltaTime * speedChangeColor));
        ColorUtility.TryParseHtmlString("#FF36E4", out _colorMe);
        if (_matRat.color == _colorMe) changeColor = false;
    }
    IEnumerator resetCol()
    {
        yield return new WaitForSeconds(0f);

        _matRat.SetColor("_TintColor", _matRat.color = Color.Lerp(_matRat.color, _colorIdle, Time.deltaTime * speedChangeColor));
        ColorUtility.TryParseHtmlString("#8820FF", out _colorMe);
        if (_matRat.color == _colorMe) resetColor = false;
    }

    public void bthomeGRat()
    {
        SceneManager.LoadScene(0);
    }

    public void btSelect(int a)
    {
        int b = a + 1;
        selectText.text = b.ToString();
        selectRat = a;
        _lineSlide.fillAmount = 0;
        _fillVaccine.fillAmount = 0;
        _scrollbar.value = 0;
    }

    public void resetHide()
    {
        for (int i = 0; i < hide.Length; i++) hide[i].SetActive(false);
        for (int i = 0; i < show.Length; i++) show[i].SetActive(true);
    }

    public void btok()
    {
        if (!changeColor && statusDosage == 3)
        {
            dosageRat[selectRat] = _scrollbar.value;
            changeColor = true;
        }
    }

    public void btbackRat()
    {
        resetColor = true;
    }

    public void btnewplay()
    {
        statusDosage = 3;
        for (int i = 0; i < cardioStatus.Length; i++) cardioStatus[i].SetActive(false);
        cardioStatus[0].SetActive(true);
        StartCoroutine(downLineDosage());
    }

    IEnumerator downLineDosage()
    {
        while (_lineSlide.fillAmount > 0)
        {
            _lineSlide.fillAmount -= 0.03f;
            _fillVaccine.fillAmount -= 0.03f;
            _scrollbar.value -= 0.03f;
            yield return new WaitForSeconds(0.005f);
        }
    }
}
