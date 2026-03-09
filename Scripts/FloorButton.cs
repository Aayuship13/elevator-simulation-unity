using UnityEngine;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour
{
    public int floorNumber;

    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color activeColor = Color.yellow;

    bool isActive = false;

    void Start()
    {
        ElevatorManager.Instance.RegisterButton(floorNumber, this);
    }
    public void CallElevator()
    {
        if (isActive) return;

        isActive = true;
        buttonImage.color = activeColor;

        ElevatorManager.Instance.RequestElevator(floorNumber);
    }

    public void ResetButton()
    {
        isActive = false;
        buttonImage.color = normalColor;
    }
}