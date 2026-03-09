using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public enum ElevatorDirection
{
    Idle, Up, Down
}
public class Elevator : MonoBehaviour
{
    public float speed = 2f;
    public int CurrentFloor = 0;

    public ElevatorDirection direction = ElevatorDirection.Idle;

    private List<int> requests = new List<int>();
    private bool isMoving = false;
    public TMP_Text floorText;
    public AudioSource dingAudio;


    public bool IsIdle()
    {
        return requests.Count == 0 && !isMoving;
    }

    public void AddRequest(int floor)
    {
        if (requests.Contains(floor))
            return;

        requests.Add(floor);
        requests.Sort();

        if (direction == ElevatorDirection.Idle)
        {
            if (floor > CurrentFloor)
                direction = ElevatorDirection.Up;
            else if (floor < CurrentFloor)
                direction = ElevatorDirection.Down;
        }
    }

    void Update()
    {

        if (!isMoving && requests.Count > 0)
        {
            int nextFloor = GetNextFloor();

            if (nextFloor != -1)
            {
                requests.Remove(nextFloor);
                StartCoroutine(MoveToFloor(nextFloor));
            }
        }
    }

    int GetNextFloor()
    {
        if (requests.Count == 0)
            return -1;

        if (direction == ElevatorDirection.Up)
        {
            foreach (int floor in requests)
            {
                if (floor >= CurrentFloor)
                    return floor;
            }

            direction = ElevatorDirection.Down;
        }

        if (direction == ElevatorDirection.Down)
        {
            for (int i = requests.Count - 1; i >= 0; i--)
            {
                if (requests[i] <= CurrentFloor)
                    return requests[i];
            }

            direction = ElevatorDirection.Up;
        }

        return requests[0];
    }

    IEnumerator MoveToFloor(int floor)
    {
        isMoving = true;
        ElevatorManager.Instance.ClearRequest(CurrentFloor);
        float targetY = ElevatorManager.Instance.floorPositions[floor].position.y;

        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            float newY = Mathf.MoveTowards(
                transform.position.y,
                targetY,
                speed * Time.deltaTime
            );

            transform.position = new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );

            yield return null;
        }

        CurrentFloor = floor;

        if (floorText != null)
        {
            floorText.text = "Floor: " + CurrentFloor;
        }
        if (dingAudio != null)
        {
            dingAudio.Play();
        }
        ElevatorManager.Instance.ClearRequest(floor);
        if (requests.Count == 0)
            direction = ElevatorDirection.Idle;

        isMoving = false;
    }
}