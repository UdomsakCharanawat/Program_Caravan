using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Array2DEditor
{
    public class ar : MonoBehaviour
    {
        [SerializeField]
        private int posx, posy;

        [SerializeField]
        private Array2DString arrayTag = null;

        [SerializeField]
        private Array2DBool shape = null;

        [SerializeField]
        private Array2DString arrayString = null;

        [SerializeField]
        private GameObject prefabToInstantiate = null;

        private int xset, yset;
        int ts = 0;
        public string selectButton;
        public GameObject board;
        GameObject prefabGO;
        public GameObject lockBoard;
        public int countSumSize, countSumMax;
        public GameObject panelComplete;
        public bool complete;

        public bool easy, medium;


        void Start()
        {
            gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
            gameObject.GetComponent<Canvas>().planeDistance = 50;

            xset = shape.GridSize.x;
            yset = shape.GridSize.y;
            mock();

            
        }

        public void finish()
        {
            if (!complete) StartCoroutine(completed());
        }

        IEnumerator completed()
        {
            complete = true;
            yield return new WaitForSeconds(1f);
            panelComplete.SetActive(true);
        }

        public void mock()
        {
            var cells = shape.GetCells();
            var aay = arrayString.GetCells();
            var atag = arrayTag.GetCells();

            GameObject piece = new GameObject("Piece");
            piece.transform.parent = board.transform;
            piece.transform.localScale = new Vector3(1f, 1f, 1f);


            for (var y = 0; y < shape.GridSize.y; y++)
            {
                for (var x = 0; x < shape.GridSize.x; x++)
                {
                    if (easy)
                    {
                        if (x == xset / 2)
                        {
                            if (y != 2 && y != 4)
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y, 0), Quaternion.identity);
                            else
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y - 0.03f, 0), Quaternion.identity);
                        }
                        else if (x != xset / 2)
                        {
                            if (y != 2 && y != 4)
                            {
                                if (x != 4)
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y, 0), Quaternion.identity);
                                else
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.02f, -y, 0), Quaternion.identity);
                            }
                            else
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y - 0.03f, 0), Quaternion.identity);
                        }
                    }

                    if (medium)
                    {
                        if (x == 3 || x == 6)
                        {
                            if (y == 3 || y == 6)
                            {
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.05f, -y - 0.05f, 0), Quaternion.identity);
                            }
                            else if (y == 4 || y == 7)
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y - 0.03f, 0), Quaternion.identity);
                            else
                            {
                                prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.05f, -y, 0), Quaternion.identity);
                            }
                        }
                        else
                        {
                            if (x == 4 || x == 7)
                            {
                                if (y == 3 || y == 6)
                                {
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y - 0.05f, 0), Quaternion.identity);
                                }
                                else if (y == 4 || y == 7)
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y - 0.03f, 0), Quaternion.identity);
                                else
                                {
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x + 0.03f, -y, 0), Quaternion.identity);
                                }
                            }
                            else
                            {
                                if (y == 3 || y == 6)
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y - 0.05f, 0), Quaternion.identity);
                                else if (y == 4 || y == 7)
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y - 0.03f, 0), Quaternion.identity);
                                else
                                    prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y, 0), Quaternion.identity);
                            }
                           
                        }
                    }


                    //prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y, 0), Quaternion.identity);
                    //prefabGO.name = $"({x}, {y})";

                    prefabGO.name = $"{x},{y}";
                    prefabGO.transform.parent = piece.transform;
                    prefabGO.transform.localScale = new Vector3(1, 1, 1);

                    if (cells[y, x])
                    {
                        var str = aay[y, x].ToString();
                        prefabGO.GetComponent<ar2>()._text.text = str;
                        prefabGO.GetComponent<ar2>().lockValue = true;
                        countSumSize++;
                    }
                    else
                    {
                        prefabGO.GetComponent<ar2>()._text.text = "";
                        prefabGO.GetComponent<ar2>().nullValue = true;
                    }

                    var tag = atag[y, x].ToString();
                    prefabGO.tag = tag;
                    prefabGO.GetComponent<ar2>().easy = easy;
                    prefabGO.GetComponent<ar2>().medium = medium;

                    countSumMax++;

                }
            }


            piece.transform.localPosition = new Vector3(posx, posy, 0);
            piece.transform.localScale = new Vector3(0.845f, 0.845f, 0.845f);

        }

        public void example()
        {
            if (shape == null || prefabToInstantiate == null)
            {
                Debug.LogError("Fill in all the fields in order to start this example.");
                return;
            }

            var cells = shape.GetCells();

            var piece = new GameObject("Piece");

            for (var y = 0; y < shape.GridSize.y; y++)
            {
                for (var x = 0; x < shape.GridSize.x; x++)
                {
                    if (cells[y, x])
                    {
                        var prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, -y, 0), Quaternion.identity, piece.transform);
                        Debug.Log(x + " : " + y);
                        prefabGO.name = $"({x}, {y})";
                        prefabGO.transform.parent = this.transform;
                        prefabGO.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
        }
    }
}

