using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUpImage;

    public void ShowPopUp()
    {
        popUpImage.SetActive(true);
        print("Show");
    }

    public void HidePopUp()
    {
        popUpImage.SetActive(false);
        print("Hide");
    }
}
