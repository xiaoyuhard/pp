using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSprite : MonoBehaviour
{
    public List<Sprite> sprites;
    
    public int Number;
    
    List<GameObject> frameObjects;
    
    public GameObject framePrefab;
    
    private int numberAmount;

    // Start is called before the first frame update
    void Start()
    {
        //CreateNumber(Number);
    }
    public void CreateNumber(int number)
    {
        frameObjects = new List<GameObject>();
        int numAmount = (int) Mathf.Log10(number) + 1;
        this.numberAmount = numAmount;
        //Debug.Log(numAmount);
        for (int i = numAmount; i >= 1; i--)
        {
            int temp = FindNum(number, i);
            CreateNumberSprite(temp);
        }
        CreateNumberSprite(10);
    }
    public void UpdateNumber(int number)
    {
        if (frameObjects != null)
        {
            for (int i = 0; i < frameObjects.Count; i++)
            {
                GameObject.Destroy(frameObjects[i]);
            }
        }
        CreateNumber(number);
    }


    public int FindNum(int num, int n)
    {
        //int power = 1;
        //for (int i = 0; i < n; i++)
        //{
        //    power *= 10;
        //}
        int power = (int) Mathf.Pow(10, n);
        return (num - num / power * power) * 10 / power;
    }
    public void CreateNumberSprite(int number)
    {
        GameObject frameObject = Instantiate(framePrefab, transform);
        Image image = frameObject.GetComponent<Image>();
        image.sprite = sprites[number];
        frameObjects.Add(frameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
