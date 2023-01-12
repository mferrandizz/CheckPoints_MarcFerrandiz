using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointController : MonoBehaviour
{
    public Text positionText;
    
    public float x, y, z;


    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }



    public void Save()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        PlayerPrefs.SetFloat("x" ,x);
        PlayerPrefs.SetFloat("y" ,y);
        PlayerPrefs.SetFloat("z" ,z);

        positionText.text = "CheckPoint: " +PlayerPrefs.GetFloat("x").ToString() + "x " + PlayerPrefs.GetFloat("y").ToString() + "y " + PlayerPrefs.GetFloat("z").ToString() +"z ";
    }

    public void LoadData()
    {
        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        z = PlayerPrefs.GetFloat("z");

        Vector3 LoadPosition = new Vector3(x,y,z);
        transform.position = LoadPosition;
    }

    public void DelateData()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.DeleteKey("z");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("CheckPoint");
            Save();
        }
    }

}
