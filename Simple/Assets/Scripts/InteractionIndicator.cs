using UnityEngine;
using System.Collections;

public class InteractionIndicator : MonoBehaviour {

    public Collider2D collider;
    public Collider2D player;
    public GameObject Esprite;

    void Start()
    {
        //collider = this.GetComponent<Collider2D>();
    }

    //void Update()
    //{
    //    if (collider.IsTouching(player))
    //    {
    //        Debug.Log("Press E");
    //    }
    //}

    void OnTriggerEnter2D(Collider2D enteringObject)
    {
        Esprite.SetActive(true);
        StartCoroutine("CheckForE");
    }

    void OnTriggerExit2D(Collider2D leavingObject)
    {
        Esprite.SetActive(false);
        StopCoroutine("CheckForE");
    }

    IEnumerator CheckForE()
    {
        int count = 0;
        while(true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                count++;
                Debug.Log("E " + count);
            }

            yield return 0;
        }
    }
}
