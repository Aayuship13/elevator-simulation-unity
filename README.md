# Elevator Simulation (Unity)

A 2D elevator simulation built using Unity.

## Features
- 3 elevators
- 4 floors
- Direction-based scheduling
- Request queue per elevator
- Nearest elevator dispatch
- Elevator ding sound on arrival
- Button highlight when pressed
- UI floor indicators

## How It Works
Floor buttons send requests to the ElevatorManager.
The manager assigns the request to the most suitable elevator based on:

- idle elevators
- direction compatibility
- distance

Each elevator maintains its own request queue.

## Unity Version
Unity 6.x

## How to Run
Open the project in Unity and run the main scene.

## Demo
Video / build link below.
