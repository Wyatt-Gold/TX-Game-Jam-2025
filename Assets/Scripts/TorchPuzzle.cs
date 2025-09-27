using System;
using System.ComponentModel;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TorchPuzzle : MonoBehaviour
{

    public GameObject[] toggles;
    public bool[] on;
    private bool editable = true;

    private Color transp = new Color(1f, 1f, 1f, 0f);
    private Color opaq = new Color(1f, 1f, 1f, 1f);

    void Start()
    {
        on = new bool[toggles.Length];
        // 5 torch puzzles will currently begin with none active. 
        if (toggles.Length == 3)
        {
            on[UnityEngine.Random.Range(0, 2)] = true;
        }

        RefreshToggles();
    }

    public void RecieveCommand(byte ID)
    {
        if (!editable) return;
        //Debug.Log("recieving command " + ID);

        switch (ID)
        {
            case 1: RotateOn(-1); break;
            case 2: RotateOn(1); break;
            case 3: on[1] = !on[1]; break;
            case 4: on[1] = !on[1]; on[2] = !on[2]; on[3] = !on[3]; break;
            case 5: on[1] = !on[1]; on[3] = !on[3]; break;
            default: throw new InvalidDataException(ID + " is not a valid command");
        }

        RefreshToggles();
        if (Solved())
        {
            editable = false;
            //Write what happens upon victory
            StartCoroutine(VictoryDance());
            
        }
    }


    // boolean rotation, either left one unit or right one unit.
    // Does not currently support more than 1 unit shifts.
    private void RotateOn(int dist)
    {
        bool[] old = new bool[on.Length];
        for (int i = 0; i < on.Length; i++)
        {
            old[i] = on[i];
        }

        bool[] fresh = new bool[on.Length];
        if (dist == -1)
        {
            fresh[fresh.Length - 1] = old[0];
            for (int i = 0; i < fresh.Length - 1; i++) fresh[i] = old[i + 1];
        }
        else
        {
            fresh[0] = old[old.Length - 1];
            for (int i = 1; i < fresh.Length; i++) fresh[i] = old[i - 1];
        }

        on = fresh;
    }

    /// <summary>
    /// Goes through the toggle array. 
    /// If a toggle is on, it deactivates its child called "Bowl"
    /// and activates its child called "Flame".
    /// If a toggle is off, do the opposite.
    /// </summary>
    void RefreshToggles()
    {
        //Debug.Log(on[0] + " " + on[1] + " " + on[2]);
        for (int i = 0; i < toggles.Length; i++)
        {
            //Debug.Log(toggles[i].GetComponentsInChildren<Transform>()[0]);
            if (on[i])
            {
                foreach (Transform child in toggles[i].GetComponentsInChildren<Transform>())
                {
                    if (child.name.Equals("Bowl")) child.GetComponent<SpriteRenderer>().color = transp;
                    if (child.name.Equals("Flame")) child.GetComponent<SpriteRenderer>().color = opaq;
                }
            } else
            {
                foreach (Transform child in toggles[i].GetComponentsInChildren<Transform>())
                {
                    if (child.name.Equals("Bowl")) child.GetComponent<SpriteRenderer>().color = opaq;
                    if (child.name.Equals("Flame")) child.GetComponent<SpriteRenderer>().color = transp;
                }
            }
        }
    }

    bool Solved()
    {
        bool win = true;
        foreach (bool check in on)
        {
            win &= check;
        }
        return win;
    }

    // Inspired by door opening sequences in Final Fantasy VI 
    System.Collections.IEnumerator VictoryDance()
    {
        float increment = 0.75f;
        float timeIncrement = 2;
        float cycles = 3;
        while (cycles-- > 0)
        {
            transform.position = transform.position + Vector3.up * increment;
            yield return new WaitForSeconds(timeIncrement);
        }
    }
}
