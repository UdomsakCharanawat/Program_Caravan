using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ar2 : MonoBehaviour, ISelectHandler
{
    public bool select;
    public Text _text;
    public Button _button;
    public bool lockValue, nullValue, inputValue, wrongValue, textValue;
    public Color _lock, _input, _wrong, _textVal;
    public Color[] _null;

    public ColorBlock cb;

    public bool easy, medium;


    void Start()
    {
        cb = _button.colors;

        if (lockValue)
        {
            //cb.normalColor = _lock;
            //_button.colors = cb;
            this.gameObject.GetComponent<Image>().color = _lock;
            _button.interactable = false;
        }
        else if (nullValue)
        {
            //var ran = Random.Range(0, _null.Length);
            //cb.normalColor = _null[ran];
            //_button.colors = cb;
            //_text.color = _textVal;

            if (easy)
            {
                if (this.gameObject.tag == "1" || this.gameObject.tag == "4" || this.gameObject.tag == "5")
                {
                    cb.normalColor = _null[0];
                    _button.colors = cb;
                    _text.color = _textVal;
                    //Debug.Log("tag : " + this.gameObject.tag);
                }
                else
                {
                    cb.normalColor = _null[1];
                    _button.colors = cb;
                    _text.color = _textVal;
                }
            }

            if (medium)
            {
                if (this.gameObject.tag == "1" || this.gameObject.tag == "3" || this.gameObject.tag == "5" || this.gameObject.tag == "7" || this.gameObject.tag == "9")
                {
                    cb.normalColor = _null[0];
                    _button.colors = cb;
                    _text.color = _textVal;
                    //Debug.Log("tag : " + this.gameObject.tag);
                }
                else
                {
                    cb.normalColor = _null[1];
                    _button.colors = cb;
                    _text.color = _textVal;
                }
            }
            
            //ฟังก์ชัน ISODD ส่งกลับค่า TRUE ถ้าจำนวนเป็นเลขคี่ หรือส่งกลับค่า FALSE
            //var mod = int.Parse(this.gameObject.tag)%2;
            //Debug.Log("tag : "+this.gameObject.tag + " : " + mod);
        }
    }

    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name);
        Array2DEditor.ar _ar = gameObject.GetComponentInParent<Array2DEditor.ar>();
        _ar.selectButton = this.gameObject.name;

    }

}

