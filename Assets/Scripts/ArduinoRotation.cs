using Assets.Scripts;

using UnityEngine;

public class ArduinoRotation : MonoBehaviour
{
    public string Port = "COM8";

    public int BaudRate = 9600;

    private Quaternion _desiredQuaternion = Quaternion.identity;

    private ArduinoJob job;

	void Start () 
    {
        job = new ArduinoJob(Port, BaudRate);
        job.Start();
    }
	
	void Update ()
	{
	    if (job != null)
	    {
            if (job.Update())
            {
                _desiredQuaternion = job.Data;
                // job = null;
            }
	    }
	    else
	    {
	        Debug.Log("Job is null");
	    }

	    //var euler = Quaternion.Slerp(gameObject.transform.parent.transform.rotation, _desiredQuaternion, 0.2f).eulerAngles;
	    var euler = _desiredQuaternion.eulerAngles;

        euler = new Vector3(euler.y * -1, euler.x * -1, euler.z + 90);
        //euler = new Vector3(euler.y, euler.x, euler.z);

        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(euler), 0.2f);
	}
}
