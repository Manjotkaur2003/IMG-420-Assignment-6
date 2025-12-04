# Assignment 6 - Advanced Enemy AI with Behavior Trees

## Project Overview
This Godot 4.4 C# project implements an advanced enemy AI system using Behavior Trees. Enemies exhibit complex behaviors including patrolling, chasing, fleeing, attacking, and calling for allies.

## Setup Instructions

### Requirements
- **Godot 4.4 .NET Version** (NOT the standard version)
- .NET 8.0 SDK

### How to Run
1. Open Godot 4.4 .NET
2. Import/Open the `BT_Assignment_Full` folder
3. Build the C# solution (Project → Build or click hammer icon)
4. Press F5 or click Play

### Controls
- **WASD / Arrow Keys**: Move player
- **SPACE**: Attack nearby enemies (25 damage)
- Player is the blue square
- Normal Enemies are red squares
- Aggressive Enemy is purple square (EXTRA CREDIT)

## Behavior Tree Structure

```
Root: BTSelector
├── 1. EmergencyFlee (BTSequence) [Priority: Highest]
│   ├── IsHealthCritical (< 20%)
│   └── Flee
│
├── 2. CallForHelp (BTSequence)
│   ├── IsHealthLow (< 50%)
│   ├── AreAlliesAvailable
│   └── SummonAlly (with particle effects!)
│
├── 3. Combat (BTSequence)
│   ├── IsPlayerInRange (200 units)
│   └── CombatOptions (BTSelector)
│       ├── AttackSequence
│       │   ├── IsPlayerInAttackRange (50 units)
│       │   ├── CanAttack
│       │   └── Attack
│       └── ChaseSequence
│           └── ChasePlayer
│
└── 4. PatrolSequence (BTSequence) [Default Behavior]
    └── Patrol
```

## AI Behaviors Demonstrated

### 1. Patrol (Default)
- Enemies patrol between assigned waypoints when player is not detected
- Enemy1 patrols points P1-P4 (top-left area)
- Enemy2 patrols points P5-P8 (bottom-right area)

### 2. Chase
- When player enters detection range (200 units, yellow circle)
- Enemy moves toward player at MoveSpeed (100)

### 3. Attack
- When player enters attack range (50 units, red circle)
- Enemy attacks with cooldown system (1 second)

### 4. Flee
- When health drops below 20%
- Enemy runs directly away from player

### 5. Summon Allies
- When health is between 20-50%
- Spawns green ally with particle effect (EXTRA CREDIT)
- Maximum 3 allies at a time

## Enhancements Implemented (Part 4 - 2 points)

### 1. Visual Debug Indicators ✓
- **Yellow circle** (200 radius): Detection range
- **Red circle** (50 radius): Attack range
- Implemented in `Enemy._Draw()` method

### 2. Group Coordination ✓
- Summoned allies automatically move toward the player
- Allies are added to "Allies" group for tracking
- Implemented in `Ally.cs`

---

## EXTRA CREDIT FEATURES (5 points)

### 1. Line-of-Sight Checking (+2 points) ✓
- **File**: `Scripts/BehaviorTree/Conditions/BTCanSeePlayer.cs`
- Uses physics raycasting to check if enemy can actually see the player
- Enemies won't detect player through walls
- Checks collision layer 1 (walls) for obstructions

### 2. Particle Effects for Summoning (+1 point) ✓
- **File**: `Scripts/BehaviorTree/Actions/BTSummonAlly.cs`
- Green particle burst effect when ally is summoned
- Uses GPUParticles2D with custom ParticleProcessMaterial
- Particles explode outward and fade

### 3. Multiple Enemy Types (+2 points) ✓
- **File**: `Scripts/AggressiveEnemy.cs` + `AggressiveEnemy.tscn`
- **Purple Aggressive Enemy** with completely different behavior:
  - **Never flees** (fights to the death!)
  - **Faster movement** (150 vs 100)
  - **Faster attacks** (0.5s vs 1.0s cooldown)
  - **Larger detection range** (300 vs 200)
  - **Less health** (80 vs 100)
  - **States**: HUNT, RAGE, ATTACK!
  - Different colored debug circle (purple)

---

## Project Structure

```
BT_Assignment_Full/
├── Main.tscn              # Main game scene
├── Player.tscn            # Player character
├── Enemy.tscn             # Normal enemy with BehaviorTree
├── Ally.tscn              # Summoned ally
├── AggressiveEnemy.tscn   # EXTRA CREDIT: Aggressive enemy
├── project.godot
├── README.md
├── Screenshots/
└── Scripts/
    ├── Player.cs
    ├── Enemy.cs
    ├── Ally.cs
    ├── AggressiveEnemy.cs     # EXTRA CREDIT
    ├── HealthBar.cs
    └── BehaviorTree/
        ├── BTNode.cs
        ├── BTComposite.cs
        ├── BTSelector.cs
        ├── BTSequence.cs
        ├── Conditions/
        │   ├── BTIsHealthCritical.cs
        │   ├── BTIsHealthLow.cs
        │   ├── BTIsPlayerInRange.cs
        │   ├── BTCanAttack.cs
        │   ├── BTAreAlliesAvailable.cs
        │   └── BTCanSeePlayer.cs    # EXTRA CREDIT
        └── Actions/
            ├── BTFlee.cs
            ├── BTChasePlayer.cs
            ├── BTAttack.cs
            ├── BTPatrol.cs
            └── BTSummonAlly.cs      # Updated with particles

```

## Tuning Parameters

| Parameter | Normal Enemy | Aggressive Enemy |
|-----------|--------------|------------------|
| Detection Range | 200 | 300 |
| Attack Range | 50 | 50 |
| Attack Cooldown | 1.0s | 0.5s |
| Move Speed | 100 | 150 |
| Max Health | 100 | 80 |
| Flees at | < 20% HP | Never! |
| Summons Allies | Yes | No |

## Testing Checklist

- [x] Enemy patrols between waypoints
- [x] Enemy detects player in range
- [x] Enemy chases player
- [x] Enemy attacks when in attack range
- [x] Enemy flees when health critical
- [x] Enemy summons allies when health low
- [x] Allies move toward player
- [x] Debug circles visible around enemies
- [x] Player movement works (WASD)
- [x] Player attack works (SPACE)
- [x] Walls contain player and enemies
- [x] **EXTRA**: Line-of-sight condition available
- [x] **EXTRA**: Particle effects on ally summon
- [x] **EXTRA**: Aggressive enemy type (purple)

## Known Limitations

1. No sprite animations (uses colored polygons)
2. No sound effects implemented
3. Player has no health system (cannot be defeated)

## Screenshots

Save screenshots to the `Screenshots/` folder showing:
1. **Patrol**: Enemies moving between waypoints
2. **Chase**: Enemy following player
3. **Attack**: Enemy attacking player
4. **Flee**: Enemy running away (low HP)
5. **Summon**: Ally being spawned with particles
6. **Aggressive**: Purple enemy in RAGE mode
