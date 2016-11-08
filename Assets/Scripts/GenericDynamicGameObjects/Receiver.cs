using UnityEngine;
using System.Collections;

public class Receiver : MonoBehaviour {

    Rigidbody rb;

    public Vector3[] positions;
    public Vector3[] rotations;
    public float speed;
    public bool loop;

    int index = 0;

	// Use this for initialization
	void Start () {
        positions = append(transform.position, positions);
        rotations = append(transform.rotation.eulerAngles, rotations);

        rb = GetComponent<Rigidbody>();
	}

    Vector3[] append(Vector3 toApp, Vector3[] arr) {
        Vector3[] output = new Vector3[arr.Length + 1];
        output[0] = toApp;
        for(int i = 0; i < arr.Length; i++) {
            output[i + 1] = arr[i];
        }
        return output;
    }
	
	// Update is called once per frame
	void Update () {
        if (rb) {
            rb.MovePosition(Vector3.Lerp(transform.position, positions[index], speed));
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(positions[index]), speed));
        } else {
            transform.position = Vector3.Lerp(transform.position, positions[index], speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(positions[index]), speed);
        }
    }

    public void Receive() {
        index++;
        if(index > positions.Length) {
            index = (loop) ? 0 : positions.Length - 1;
        }
    }
}
