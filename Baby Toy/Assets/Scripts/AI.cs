using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Animator anim;
    public Transform player;
    public Transform ppointLeft;
    public Transform ppointRight;
    public GameObject npc;
    State currentState;

    public Transform hold;

    public GameObject[] dadTalk;
    public GameObject dad;
    public GameObject speechContainer;
    private bool flipped;
    private int random;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = new Patrol(this.gameObject, anim, player, ppointLeft, ppointRight);
        StartCoroutine(DadTalk());
    }

    private IEnumerator DadTalk()
    {
        while (currentState.ToString() == "Patrol")
        {
            if (currentState.ToString() == "Patrol")
            {
                random = Random.Range(0, 4);
                dadTalk[random].SetActive(true);
            }

            yield return new WaitForSeconds(2);

            dadTalk[random].SetActive(false);

            yield return new WaitForSeconds(3);
        }
    }

    public void SeeBaby()
    {
        currentState.found = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();

        if (dad.transform.rotation.y < 0 && flipped)
        {
            speechContainer.transform.Rotate(0, 180, 0);
            flipped = false;
        }

        if (dad.transform.rotation.y > 0 && !flipped)
        {
            speechContainer.transform.Rotate(0, 180, 0);
            flipped = true;
        }
    }
}
