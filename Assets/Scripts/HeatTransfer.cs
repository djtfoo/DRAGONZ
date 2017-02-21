using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeatTransfer : MonoBehaviour {
    public float rateOfTransfer,MaxDistanceForTransfer, MinDistanceForTransfer;
    public static List<GameObject> m_GoList = new List<GameObject>() ;
	// Use this for initialization
	void Start () {
        rateOfTransfer *=Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject go in m_GoList)
        {
            if (!go.activeSelf)
                continue;

            //Debug.Log("YEA");
            foreach (GameObject go2 in m_GoList)
            {
                //Debug.Log("YEA");
                if (!go2.activeSelf)
                    continue;
                if (go == go2)
                    continue;
                float length = (go.transform.position - go2.transform.position).magnitude;
                Debug.Log(length);
                if (length <= MaxDistanceForTransfer && length >= MinDistanceForTransfer)
                {
                    Temperature go1Temp = go.GetComponent<Temperature>();
                    Temperature go2Temp = go2.GetComponent<Temperature>();


                    float HeatTransferDegrees;

                    if (go1Temp.currentTemperature > go2Temp.currentTemperature)
                    {
                        HeatTransferDegrees = (rateOfTransfer * (go1Temp.currentTemperature - go2Temp.currentTemperature)) / ((go1Temp.RValue - go2Temp.RValue)+length);
                        go1Temp.currentTemperature -= HeatTransferDegrees;
                        go2Temp.currentTemperature += HeatTransferDegrees;
                        Debug.Log(HeatTransferDegrees);
                    }
                    else
                    {
                        HeatTransferDegrees = (rateOfTransfer * (go2Temp.currentTemperature - go1Temp.currentTemperature)) / ((go2Temp.RValue - go1Temp.RValue) + length);
                        go1Temp.currentTemperature += HeatTransferDegrees;
                        go2Temp.currentTemperature -= HeatTransferDegrees;
                        Debug.Log(HeatTransferDegrees);
                    }
                }

            }
        }

	}
    public  void AddToGoList(GameObject Go)
    {
        m_GoList.Add(Go);
    }
    public  void DeleteToGoList(GameObject other)
    {
        foreach (GameObject go in m_GoList)
        {
            if (go == other)
            {
                m_GoList.Remove(other);
            }
        }
    }
}
