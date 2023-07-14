using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text coinCount;
    public Text coinsText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCount.text = count.ToString();
        coinsText.text = "coins :" + count.ToString();   
    }
    public void AddCount()
    {
        count++;
    }

}
