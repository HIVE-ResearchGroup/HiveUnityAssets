using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.UserStudy
{
    public static class TrackingDataLogger
    {
        public static string filePathPrefix = "C:\\_tracking_data\\";
        public static string delimiter = ";";


        public static string retrieveFilePath(GameObject o, int personID)
        {
            if (!Directory.Exists(filePathPrefix + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Directory.CreateDirectory(filePathPrefix + DateTime.Now.ToString("yyyy-MM-dd"));
            }

            string filePath = filePathPrefix + DateTime.Now.ToString("yyyy-MM-dd") + "\\" +
                              personID +  "_" + o.name + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return filePath;

        }

        public static float[] retrieveCommonTrackingData(int taskNo, GameObject o)
        {
            Vector3 trackerPosition = o.transform.position;
            Vector3 trackerLocalPosition = o.transform.localPosition;
            Vector3 trackerOrientation = o.transform.rotation.eulerAngles;
            Quaternion trackerLocalOrientation = o.transform.localRotation;

            float[] output = new float[]
            {
                taskNo,
                trackerPosition.x,
                trackerPosition.y,
                trackerPosition.z,
                /*trackerLocalPosition.x,
                trackerLocalPosition.y,
                trackerLocalPosition.z,*/
                trackerOrientation.x,
                trackerOrientation.y,
                trackerOrientation.z
                /*trackerLocalOrientation.w,
                trackerLocalOrientation.x,
                trackerLocalOrientation.y,
                trackerLocalOrientation.z*/
            };

            return output;

        }

        public static StringBuilder AddDataToLine(StringBuilder sb, float[] trackingData)
        {
            int length = trackingData.Length;

            if (sb == null)
            {
                sb = new StringBuilder();
            }

            for (int index = 0; index < length; index++)
            {
                sb.Append(trackingData[index] + delimiter);
            }

            sb.AppendLine();

            return sb;

        }

    }
}