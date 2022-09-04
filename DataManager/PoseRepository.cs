using System;
using UnityEngine;
using System.IO;
using System.Xml;

public interface IPoseRepository : IRepository<Poses>
{
    Vector3 GetPose(Poses.PoseBts obj);
    Vector2 GetPoseHand(Poses.PoseHand obj);

    bool PoseFound();
    void SaveToTxt(String data, String fileName);
    void SaveToXml(String data, String fileName);
    void LoadTxt(String fileName);
    void LoadXml(String fileName);
}

public class PoseRepository : Repository<Poses>, IPoseRepository
{
    // Get pose from optical device
    public Vector3 GetPose(Poses.PoseBts obj)
    {
        if (InputSelector.inputFactory.GetInput("Mediapipe").IsReady())
        {
            return MediapipeManager.GetPoint(MediapipeManager.ConvertKinectMapToMediaPipe(obj));
        }
        else if (InputSelector.inputFactory.GetInput("Kinect").IsReady())
        {
            return KinectManager.GetRawSkeletonJointPos(KinectManager.GetPlayer1ID(), (int)obj);
        }
        else
        {
            return Vector3.zero;
        }
    }

    //Mediapipe only
    public Vector2 GetPoseHand(Poses.PoseHand obj)
    {
        if (MediapipeManager.VectorHand.Count > 0)
        {
            return new Vector2(
                MediapipeManager.VectorHand[(int)obj].X,
                MediapipeManager.VectorHand[(int)obj].Y
            );
        }
        else
        {
            return Vector2.zero;
        }
    }

    // If PoseFound return true
    public bool PoseFound()
    {
        if (InputSelector.inputFactory.GetInput("Mediapipe").IsReady())
        {
            return MediapipeManager.HasPose();
        }
        else if (InputSelector.inputFactory.GetInput("Kinect").IsReady())
        {
            return KinectManager.Player1Calibrated;
        }
        else
        {
            return false;
        }
    }

    // Save to txt file
    public void SaveToTxt(String data, String fileName)
    {
        string path = Application.dataPath + "/" + fileName + ".txt";
        File.AppendAllText(path, data + "\n\n");
    }

    // Save to xml file
    public void SaveToXml(String data, String fileName)
    {
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", fileName);

        // Begin node
        XmlElement pose = xmlDocument.CreateElement("Pose");
        pose.InnerText = data;
        root.AppendChild(pose);
        // End node

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/" + fileName + ".xml");
    }

    // Load txt file
    public void LoadTxt(String fileName)
    {
        string path = Application.dataPath + "/" + fileName + ".txt";
        string textFromFile = File.ReadAllText(path);
        Debug.Log(textFromFile);
    }

    // Load xml file
    public void LoadXml(String fileName)
    {
        string path = Application.dataPath + "/" + fileName + ".xml";
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(path);

        XmlNodeList pose = xmlDocument.GetElementsByTagName("Pose");
        Debug.Log(pose[0].InnerText);
    }
}