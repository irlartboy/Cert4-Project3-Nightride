using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public float x, y, z;

    public void SaveFunction()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        Save.SaveData(this); 
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
