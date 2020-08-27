using UnityEngine;

public class Toolbar : MonoBehaviour
{
    private bool smth_selected = false;

    public void Select_button(int number)
    {
        for (int i=0; i<7; i++)
        {
             transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Button>().enabled = (i != number);
        }
    }

    void Update()
    {
        if (!smth_selected)
        {
            smth_selected = true;
            Select_button(0);
            transform.parent.parent.gameObject.GetComponent<Menu_functions>().Set_records("all");
        }
    }
}
