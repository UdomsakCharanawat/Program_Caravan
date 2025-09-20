using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyboardKey : MonoBehaviour
{
    //public bool easy, medium;
    public int sizeEasy, sizeMedium;
    private int sizeTemp;
    Array2DEditor.ar _ar;
    ar2 _ar2;
    public int col, row;
    public string tagSelect;
    GameObject[] ertag;
    public GameObject[] rowError, colError, tagError;

    private void Start()
    {
        _ar = gameObject.GetComponentInParent<Array2DEditor.ar>();

        resetError();
    }

    public void input(int value)
    {
        _ar2 = GameObject.Find(_ar.selectButton).GetComponent<ar2>();
        tagSelect = _ar2.GetComponent<ar2>().tag;

        if (_ar.easy) sizeTemp = sizeEasy;
        else if (_ar.medium) sizeTemp = sizeMedium;


        if (value != 0)
        {
            if (_ar2._text.text == "")
                _ar.countSumSize++;

            _ar2._text.text = value.ToString();

            ColorBlock cb = _ar2._button.colors;
            cb.normalColor = Color.white;
            _ar2._button.colors = cb;

            string sel = _ar.selectButton;
            string[] spl = sel.Split(char.Parse(","));
            col = int.Parse(spl[0]);
            row = int.Parse(spl[1]);

            ertag = GameObject.FindGameObjectsWithTag(tagSelect);
            foreach (GameObject er in ertag)
            {
                if (er.GetComponent<ar2>()._text.text == value.ToString())
                    if (er.GetComponent<ar2>().name != sel)
                    {
                        tagError[int.Parse(tagSelect) - 1].SetActive(true);
                        _ar.lockBoard.SetActive(true);
                    }
            }

            
            for (int i = 0; i < sizeTemp; i++)
            {
                if (GameObject.Find(col + "," + i.ToString()).GetComponent<ar2>()._text.text == value.ToString())
                    if (i != row)
                        if (GameObject.Find(col + "," + i.ToString()).GetComponent<ar2>().tag != tagSelect)
                        {
                            colError[col].SetActive(true);
                            _ar.lockBoard.SetActive(true);
                        }

                if (GameObject.Find(i.ToString() + "," + row).GetComponent<ar2>()._text.text == value.ToString())
                    if (i != col)
                        if (GameObject.Find(i.ToString() + "," + row).GetComponent<ar2>().tag != tagSelect)
                        {
                            rowError[row].SetActive(true);
                            _ar.lockBoard.SetActive(true);
                        }
            }
            
           
        }

        if(value == 0)
        {
            if (_ar2._text.text != "")
                _ar.countSumSize--;

            _ar2._text.text = "";

            _ar2._button.colors = _ar2.cb;

            resetError();
        }

        if (_ar.countSumSize == _ar.countSumMax)  _ar.finish();
    }

    public void resetError()
    {
        for (int i = 0; i < rowError.Length; i++)
        {
            rowError[i].SetActive(false);
            colError[i].SetActive(false);
            tagError[i].SetActive(false);
        }

        _ar.lockBoard.SetActive(false);
    }
}