using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
//using UnityEditor;
//[CustomEditor(typeof(ComboMeter))]
public class ComboMeter :  NetworkBehaviour {
    public float MaxComboCounterLifeTime,timer,AddtoTimer;
    public int ComboCounter;
    public bool ComboCounterActivated;
    public Text ComboCounterText,DescribeComboCounter;
    public Image ComboTimerBar;
	// Use this for initialization
    
	void Start () 
    {
        if (!isLocalPlayer)
            return;
        ComboTimerBar.fillAmount = 0;
        ComboCounter=0;
	}
	
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();
    //    ComboMeter comboMeter = (ComboMeter)target;
    //    if(GUILayout.Button("Generate String"))
    //    {
            
    //    }
    //}   
	// Update is called once per frame
    public void AddToComboMeter(int comboHits)
    {
        if (!isLocalPlayer)
            return;

        ComboCounter += comboHits;
        if(timer>=AddtoTimer)
        timer -= AddtoTimer;
        ComboCounterText.text = ComboCounter.ToString();
        if (!ComboCounterActivated)
            ComboCounterActivated = true;
        
        //if (ComboCounter >= 15)
        //{
        //    DescribeComboCounter.text = "Amazing!";
        //}
        //else if (ComboCounter >= 10)
        //{
        //    DescribeComboCounter.text = "IMMACULATE!!!";
        //}
        //else if (ComboCounter >= 5)
        //{
        //    DescribeComboCounter.text = "WOMBO COMBO THAT AINT FALCO";
        //}
        //else
        //{
        //    DescribeComboCounter.text = "";
        //}
    }

	void Update () {
        
        if(!isLocalPlayer)
            return;

        if(ComboCounterActivated)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer / MaxComboCounterLifeTime);
            ComboTimerBar.fillAmount = (1-(timer / MaxComboCounterLifeTime));
            if(timer>=MaxComboCounterLifeTime)
            {
                ComboCounter = 0; 
                ComboCounterText.text = " ";
                ComboCounterActivated = false;
                timer = 0;
                //DescribeComboCounter.text = " ";
               
            }
            //if(ComboCounter >=15)
            //{
            //    DescribeComboCounter.text = "Amazing!";
            //}
            //else if(ComboCounter>=10)
            //{
            //    DescribeComboCounter.text = "IMMACULATE!!!";
            //}
            //else if(ComboCounter >=5)
            //{
            //    DescribeComboCounter.text = "WOMBO COMBO THAT AINT FALCO";
            //}
            //else
            //{
            //    DescribeComboCounter.text = "";
            //}
        }

	}
}
