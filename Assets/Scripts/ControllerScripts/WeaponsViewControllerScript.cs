﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsViewControllerScript : MonoBehaviour {

    public List<GameObject> Elements = new List<GameObject>();

    public GameObject ExampleElement;
    public GameObject PaddingContent;

    private int row = 1;
    private int collum = 1;

    private float widthOfAElement = 75f; //this should maybe be felxible

	void Start () {
        Elements.Clear();

        //Test---------------------V
        //GameObject goTemp = Instantiate(ExampleElement, PaddingContent.transform);

        ////goTemp.transform.position = new Vector2(-PaddingContent.transform.GetComponent<RectTransform>().sizeDelta.x / 2 + widthOfAElement / 2, -15);
        //goTemp.GetComponent<RectTransform>().anchoredPosition = new Vector2(-180, -15);
        //goTemp.SetActive(true);
        //Elements.Add(goTemp);


        
        int elementsPerRow = Mathf.RoundToInt(PaddingContent.transform.GetComponent<RectTransform>().sizeDelta.x / widthOfAElement); //is 6 now, with the width of 445
        int collumPosCounter = -elementsPerRow / 2;
        int rowPosCounter = 0;
        
        foreach (GameObject WepElement in ObjectHolder._PlayerWeapons) {
            if (WepElement != null) {
                GameObject goTemp = Instantiate(ExampleElement, PaddingContent.transform);
                goTemp.GetComponent<Image>().sprite = WepElement.GetComponent<WeaponBehaviourScript>().WeaponImage;
                
                //Debug.Log(widthOfAElement * collumPosCounter + widthOfAElement / 2);

                goTemp.GetComponent<RectTransform>().anchoredPosition = new Vector2(widthOfAElement * collumPosCounter + widthOfAElement / 2, -(widthOfAElement * rowPosCounter + widthOfAElement / 2));

                goTemp.SetActive(true);
                Elements.Add(goTemp);
                collumPosCounter++;
                //if (rowPosCounter == 0) rowPosCounter++;
                if (collumPosCounter == elementsPerRow / 2) {
                    collumPosCounter = -elementsPerRow / 2;
                    rowPosCounter++;
                }
                //if (rowPosCounter == elementsPerRow) rowPosCounter = 0;
            }
        }
        
    }

    void Update () {
        //adjusts the size of the padding content to be the same as the normal contents - the padding
        PaddingContent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(PaddingContent.transform.GetComponent<RectTransform>().sizeDelta.x, PaddingContent.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 2*PaddingContent.transform.GetComponent<RectTransform>().offsetMax.y);
        //PaddingContent.transform.GetComponent<RectTransform>().sizeDelta
    }
}
