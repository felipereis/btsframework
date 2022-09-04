using UnityEngine;

//A data transfer object (DTO) is an object that carries data
//You can use this technique to facilitate communication between two apps
[System.Serializable]
public class RotationDTO
{
    public string name;
    public QuaternionDTO rotation;

    public RotationDTO(string name, QuaternionDTO rotation)
    {
        this.name = name;
        this.rotation = rotation;
    }
}

[System.Serializable]
public class QuaternionDTO
{
    public float x, y, z, w;

    public QuaternionDTO(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
}

public class Dto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}