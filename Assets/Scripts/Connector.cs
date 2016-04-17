using UnityEngine;
using System.Collections;

public class Connector : MonoBehaviour {
    public GameObject connector;
    public Transform target;
    public bool connected;	

    void Update () {
        if (connected) {
            if (!connector.GetComponent<MeshRenderer>().enabled) {
                connector.GetComponent<MeshRenderer>().enabled = true;
            }
            connector.transform.position = (transform.position + target.position) * 0.5f;
            connector.transform.LookAt(transform);
            connector.transform.localScale = new Vector3(1, 1, Mathf.Abs((target.position - transform.position).magnitude));
        } else if (connector.GetComponent<MeshRenderer>().enabled) {
            connector.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
