Behavior Tree Enemy AI - Script Pack
------------------------------------

This zip contains C# scripts for a Godot 4 (.NET) project implementing
a behavior-tree-driven Enemy with:

- Emergency flee when health is critical
- Call-for-help when low health and allies are available
- Combat (attack when in range and cooldown ready, otherwise chase)
- Patrol as fallback behavior
- Simple Ally that moves toward the Player
- Debug visualization circles around Enemy (extra credit visual indicator)

How to integrate into your project:

1. Create folders:
   - res://Scripts/BehaviorTree/Conditions
   - res://Scripts/BehaviorTree/Actions
   - res://Scripts

2. Copy these scripts into the matching folders.

3. Enemy.tscn:
   - Root node: Enemy (CharacterBody2D) with Enemy.cs attached.
   - Add child node: BehaviorTree (Node), attach BTSelector.cs to it.
   - Under BehaviorTree, build this tree:

     BehaviorTree (BTSelector)
       EmergencyFlee (BTSequence)
         IsHealthCritical (BTIsHealthCritical)
         Flee (BTFlee)
       CallForHelp (BTSequence)
         IsHealthLow (BTIsHealthLow)
         AreAlliesAvailable (BTAreAlliesAvailable)
         SummonAlly (BTSummonAlly)
       Combat (BTSequence)
         IsPlayerInRange (BTIsPlayerInRange) [Range ~ 200]
         CombatOptions (BTSelector)
           AttackSequence (BTSequence)
             IsPlayerInAttackRange (BTIsPlayerInRange) [Range ~ 50]
             CanAttack (BTCanAttack)
             Attack (BTAttack)
           ChaseSequence (BTSequence)
             ChasePlayer (BTChasePlayer)
       PatrolSequence (BTSequence)
         Patrol (BTPatrol) [assign PatrolPointsPaths to patrol nodes]

4. Main.tscn:
   - Root: Main (Node2D)
   - Add Player (CharacterBody2D, Player.cs), path must be "Main/Player".
   - Instance 2+ Enemy scenes.
   - Add Node2D children as patrol points; assign them in BTPatrol.PatrolPointsPaths.

5. Ally.tscn:
   - Root: Ally (CharacterBody2D, Ally.cs).
   - Simple sprite/collision; it will automatically join group "Allies".

6. Project Settings → Input Map:
   - ui_left: A, Left
   - ui_right: D, Right
   - ui_up: W, Up
   - ui_down: S, Down

7. Build C# project, then run.

You can now capture screenshots for each behavior:
- Enemy patrolling
- Enemy chasing player
- Enemy attacking
- Enemy summoning ally (and allies converging on player)
- Enemy fleeing at low health

Enhancements / Extra Credit implemented:
- Visual debug rings in Enemy._Draw()
- Ally group coordination (allies converge on player)
- Modular BT scripts that match assignment requirements.
