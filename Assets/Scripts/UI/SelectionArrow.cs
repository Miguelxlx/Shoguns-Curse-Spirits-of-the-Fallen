using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.Return))
            Interact();
    }

    private void ChangePosition(int change)
    {
        currentPosition += change;

        //if(change != 0)
        //    //Play Sound

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;
        //Assign the y position of the option to show to the arrow (to move it up and down)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        //Play interact sound
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

}
