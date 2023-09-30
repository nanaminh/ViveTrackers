using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class DataLogger : MonoBehaviour
{
    public string participant_ID;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            WritePosition("--- Calibration ---");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WritePosition("--- Start " + i.ToString() + " ---");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            WritePosition("--- End " + i.ToString() + " ---");
            i++;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            WritePosition("--- Reset ---");
            i = 1;
        }

    }

    // FixedUpdate is called once per 0.1 s
    void FixedUpdate()
    {
        float px = this.gameObject.transform.position.x;
        float pz = this.gameObject.transform.position.z;
        string timestamp = System.DateTime.Now.ToString("hh:mm:ss:ff");
        string txt = timestamp + " " + px.ToString() + ", " + pz.ToString();
        WritePosition(txt);
        
    }

    void WritePosition(string text)
    {
        string trackername;
        if(this.gameObject.name == "LHR-3EBE6F7B")
        {
            trackername = "Spot";
        } else
        {
            trackername = "Human";
        }
        string filename = participant_ID + trackername + "_position.txt";
        string path = "Assets/TrackerData/" + filename;
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

    }
}
