using Assets.Code.UserStudy;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LogTrackingData : MonoBehaviour
{

    public bool doRecord = false;
    public const float logInterval = 0.1f;
    float logTimer = logInterval;

    public StringBuilder sb;
    public string filePath;

    public int TaskNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitLogging(int personID)
    {
        logTimer = logInterval;
        filePath = TrackingDataLogger.retrieveFilePath(this.gameObject, personID);
        sb = new StringBuilder();
    }

    public void StartLogging(int taskNo)
    {
        doRecord = true;
        TaskNo = taskNo;
    }


    public void StopLogging()
    {
        doRecord = false;
        logTimer = logInterval;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (doRecord)
        {
            logTimer -= Time.deltaTime;
            if (logTimer<=0)
            {
                logTimer = logInterval;

                float[] output = TrackingDataLogger.retrieveCommonTrackingData(TaskNo, this.gameObject);
                sb = new StringBuilder();
                sb = TrackingDataLogger.AddDataToLine(sb, output);

                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, sb.ToString());
                }
                else
                {
                    File.AppendAllText(filePath, sb.ToString());
                }

            }
        }
    }
}