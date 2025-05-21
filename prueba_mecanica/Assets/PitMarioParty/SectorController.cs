using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SectorController : MonoBehaviour
{

    public float repeatTime;
    public AnimatorController animatorController;
    public AnimatorController animatorController2;
    public bool chooseSector;

    public static int pairIndex;

    public static bool selectionActive;
    public static List<Transform> sectors = new List<Transform>();
    private Color originalColor;


    public Rigidbody Player1;
    public Rigidbody Player2;
    public Rigidbody Enemy1;
    public Rigidbody Enemy2;
    public Rigidbody Enemy3;     
    public Rigidbody Enemy4;
    public Rigidbody Enemy5;
    public Player ScriptPlayer1;
    public Player ScriptPlayer2;
    public MovNPC ScriptNPC;
    public MovNPC ScriptNPC1;
    public MovNPC ScriptNPC2;
    public MovNPC ScriptNPC3;
    public MovNPC ScriptNPC4;


    void Start()
    {
        selectionActive = true;
        foreach(Transform child in transform)
        {
            sectors.Add(child.GetChild(0));
        }

        originalColor = sectors[0].GetComponent<MeshRenderer>().material.color;
        StartCoroutine(WaitAndChange());

    }

    void Update()
    {
        if(chooseSector)
        {
            chooseSector = false;
            selectionActive = false;

            int first = pairIndex * 2;
            int second = first + 1;

            sectors[first].GetComponent<Animator>().runtimeAnimatorController = animatorController;
            sectors[first].GetComponent<Animator>().SetTrigger("Move");

            sectors[second].GetComponent<Animator>().runtimeAnimatorController = animatorController2;
            sectors[second].GetComponent<Animator>().SetTrigger("Move");

            StartCoroutine(ActDescGravedad());
        }
    }

    public static void RemoveAnimator()
    {
        selectionActive = true;
    }

    /*private void ChangeIndex()
    {
        index++;
        if (index == sectors.Count)
            index = 0;
    }*/

    private void ChangePairIndex()
    {
        pairIndex++;
        if (pairIndex >= sectors.Count / 2)
            pairIndex = 0;
    }


    private void SetColors()
    {
        for (int i = 0; i < sectors.Count; i++)
        {
            if (i == pairIndex * 2 || i == pairIndex * 2 + 1)
                sectors[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
            else
                sectors[i].GetComponent<MeshRenderer>().material.color = originalColor;
        }
    }

    private IEnumerator WaitAndChange()
    {
        while(true)
        {
            if(selectionActive)
            {
                ChangePairIndex();
                SetColors();
            }
            yield return new WaitForSeconds(repeatTime);
        }
    }

    IEnumerator ActDescGravedad()
    {
        ScriptPlayer1.Speed = 0f;
        ScriptPlayer2.Speed = 0f;
        ScriptNPC.speed = 0f;
        ScriptNPC1.speed = 0f;
        ScriptNPC2.speed = 0f;
        ScriptNPC3.speed = 0f;
        ScriptNPC4.speed = 0f;

        Player1.useGravity = false;
        Player2.useGravity = false;
        Enemy1.useGravity = false;
        Enemy2.useGravity = false;
        Enemy3.useGravity = false;
        Enemy4.useGravity = false;
        Enemy5.useGravity = false;

        yield return new WaitForSeconds(1f);

        ScriptPlayer1.Speed = 5f;
        ScriptPlayer2.Speed = 5f;
        ScriptNPC.speed = 5f;
        ScriptNPC1.speed = 5f;
        ScriptNPC2.speed = 5f;
        ScriptNPC3.speed = 5f;
        ScriptNPC4.speed = 5f;

        Player1.useGravity = true;
        Player2.useGravity = true;
        Enemy1.useGravity = true;
        Enemy2.useGravity = true;
        Enemy3.useGravity = true;
        Enemy4.useGravity = true;
        Enemy5.useGravity = true;
    }
}
