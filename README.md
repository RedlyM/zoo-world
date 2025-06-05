# 🦁 Zoo World

**Zoo World** is a 3D simulation prototype built in Unity. Animals spawn periodically and interact with each other based on simple predator-prey rules.

## 🎥 Gameplay

![Zoo World Gameplay](Readme%20Files/gameplay.gif)

## 📋 Task Summary

- Top-down camera
- A new animal appears every 1–2 seconds
- Animals move randomly with physics-based collisions
- Prey bounce off each other or disappear when touched by a predator
- Predators eat prey, and may also eat each other
- A "Tasty!" label appears under the predator when it eats
- UI displays counters for dead prey and predators (top-right corner)

## Architecture

The project is implemented using the **MVP (Model–View–Presenter)** pattern:

- **Model** stores an animal’s data and lifetime in seconds
- **View** handles physics, rendering, and collision triggers
- **Presenter** connects the model and view, and manages interaction logic

### Predator-vs-Predator Logic

When two predators collide:
- The **younger** predator (shorter lifetime) survives
- If both have equal lifetimes, the winner is chosen randomly

## Plugins Used

- **VContainer** — Dependency injection
- **UniRx** — Reactive properties and state handling
- **UniTask** — Lightweight async operations
- **DoTween** — UI/animation transitions
- **Odin Inspector** — Editor improvements
