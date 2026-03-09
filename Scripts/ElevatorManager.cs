using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance;

    public List<Elevator> elevators;
    public Transform[] floorPositions;
    private HashSet<int> activeRequests = new HashSet<int>();
    Dictionary<int, FloorButton> floorButtons = new Dictionary<int, FloorButton>();
    void Awake()
    {
        Instance = this;
    }
    
    public void RegisterButton(int floor, FloorButton button)
    {
        floorButtons[floor] = button;
    }

    public void RequestElevator(int floor)
    {
        if (activeRequests.Contains(floor))
            return;

        activeRequests.Add(floor);

        Elevator bestElevator = null;
        float bestScore = float.MaxValue;

        foreach (Elevator elevator in elevators)
        {
            float distance = Mathf.Abs(
                elevator.transform.position.y -
                floorPositions[floor].position.y
            );

            float score = distance;

            if (elevator.direction == ElevatorDirection.Idle)
                score -= 3;

            if (elevator.direction == ElevatorDirection.Up && floor >= elevator.CurrentFloor)
                score -= 1;

            if (elevator.direction == ElevatorDirection.Down && floor <= elevator.CurrentFloor)
                score -= 1;

            if (score < bestScore)
            {
                bestScore = score;
                bestElevator = elevator;
            }
        }

        if (bestElevator != null)
            bestElevator.AddRequest(floor);
    }
    public void ClearRequest(int floor)
    {
        activeRequests.Remove(floor);
        if (floorButtons.ContainsKey(floor))
        {
            floorButtons[floor].ResetButton();
        }
    }
}