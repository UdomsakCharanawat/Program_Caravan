using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveItem : MonoBehaviour
{
    /*
     * Drag object
     */
    float deltaX, deltaY;
    public bool locked;
    public Vector2 initialPosition;
    Vector2 mousePosition;
    public Transform point;
    public float scaleXclick, scaleYclick, scaleXdown, scaleYdown;
    public GameObject[] openPanelPig, openPanelChicken, openPanelCrown;

    game01Control _game01Control;
    score _score;

    bool hideItem;
    public SpriteRenderer _sp;
    public Transform _trans;
    public Color[] co;

    private void Start()
    {
        _game01Control = GameObject.Find("scriptControl").GetComponent<game01Control>();
        _score = GameObject.Find("scriptControl").GetComponent<score>();
        _sp = gameObject.GetComponent<SpriteRenderer>();
        _trans = gameObject.GetComponent<Transform>();
    }

    private void OnMouseDown()
    {
        initialPosition = this.transform.localPosition;
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.localPosition.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.localPosition.y;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.2f);
            transform.localScale = new Vector2(transform.localScale.x + scaleXclick, transform.localScale.y + scaleYclick);
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(mousePosition.x - deltaX, mousePosition.y - deltaY, transform.localPosition.z);
        
        }

        //Debug.Log("transform : " + transform.localPosition.x);
        //Debug.Log("point : " + point.localPosition.x);
        if (Mathf.Abs(transform.localPosition.x - point.localPosition.x) <= 200f &&
                Mathf.Abs(transform.localPosition.y - point.localPosition.y) <= 200f)
        {
            Debug.Log("in area");
        }
    }

    private void OnMouseUp()
    {
        if (!locked)
        {
            if (Mathf.Abs(transform.localPosition.x - point.localPosition.x) <= 200f &&
                Mathf.Abs(transform.localPosition.y - point.localPosition.y) <= 200f)
            {
                ///transform.localPosition = new Vector3(point.localPosition.x, point.localPosition.y, -0.1f);
                //Destroy(this.gameObject);
                ///transform.localScale = new Vector2(transform.localScale.x - scaleXdown, transform.localScale.y - scaleYdown);
                ///locked = true;
                ///
                if (_game01Control.animal == 0 && this.gameObject.tag == "Pig")
                {
                    //_score.calcu();
                    //this.gameObject.SetActive(false);
                    hideItem = true;
                    for (int i = 0; i < openPanelPig.Length; i++) openPanelPig[i].SetActive(true);
                    
                }
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y - 0.1f);
                transform.localScale = new Vector2(transform.localScale.x - scaleXclick, transform.localScale.y - scaleYclick);

                if (_game01Control.animal == 1 && this.gameObject.tag == "Chicken")
                {
                    //_score.calcu();
                    //this.gameObject.SetActive(false);
                    hideItem = true;
                    for (int i = 0; i < openPanelChicken.Length; i++) openPanelChicken[i].SetActive(true);
                }
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y - 0.1f);
                transform.localScale = new Vector2(transform.localScale.x - scaleXclick, transform.localScale.y - scaleYclick);

                if (_game01Control.animal == 2 && this.gameObject.tag == "Crown")
                {
                    //_score.calcu();
                    //this.gameObject.SetActive(false);
                    hideItem = true;
                    for (int i = 0; i < openPanelCrown.Length; i++) openPanelCrown[i].SetActive(true);
                }

                if (hideItem)
                {
                    _score.calcu();
                    this.gameObject.SetActive(false);
                }
                else
                {
                    transform.localPosition = new Vector3(initialPosition.x, initialPosition.y - 0.1f);
                    transform.localScale = new Vector2(transform.localScale.x - scaleXclick, transform.localScale.y - scaleYclick);

                    StartCoroutine(wrongAnim());
                    StartCoroutine(wrongShake());
                }
                //else
                //{
                //    transform.localPosition = new Vector3(initialPosition.x, initialPosition.y - 0.1f);
                //    transform.localScale = new Vector2(transform.localScale.x - scaleXclick, transform.localScale.y - scaleYclick);

                //    try
                //    {
                //        StartCoroutine(wrongAnim());
                //        StartCoroutine(wrongShake());
                //    }
                //    catch
                //    {
                //        Debug.Log("item true");
                //    }
                //}
            }
            else
            {
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y - 0.1f);
                transform.localScale = new Vector2(transform.localScale.x - scaleXclick, transform.localScale.y - scaleYclick);

                //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 150, 150, 255);
            }
            
        }
        
    }

    
    IEnumerator wrongAnim()
    {
        for (int i = 0; i <= 3; i++)
        {
            _sp.color = co[1];
            yield return new WaitForSeconds(0.05f);
            _sp.color = co[0];
            yield return new WaitForSeconds(0.05f);
            Debug.Log("change color" + i);
        }
    }

    IEnumerator wrongShake()
    {
        for (int i = 0; i <= 3; i++)
        {
            _trans.eulerAngles = new Vector3(0, 0, 2);
            yield return new WaitForSeconds(0.05f);
            _trans.eulerAngles = new Vector3(0, 0, -2);
            yield return new WaitForSeconds(0.05f);
        }
        _trans.eulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0f);
    }
}
