<p align="center">
  <img src="https://github.com/user-attachments/assets/5a4e3f76-ef8c-4c99-a396-ccb6576cacf5" alt="animated" />
</p>

# Unity Tower Defense Game

A modular and extensible tower defense game built with Unity, featuring a robust architecture with state machines, object pooling, command pattern, and a flexible stat system.

## 🎮 Game Overview

This is a classic tower defense game where players strategically place defensive units on a grid-based board to prevent enemy units from reaching the player's base. The game features:

- Grid-based board system
- Multiple defensive and enemy unit types
- Real-time combat with projectile system
- Level progression system
- Status effect system for buffs/debuffs
- Save/load functionality

## 🏗️ Architecture

### Core Systems

#### **State Machine System**
- Generic state machine implementation for managing game states, unit behaviors, and UI panels
- Used for enemy AI (Movement, Attack, Dead states)
- Used for defense units (Attack, Dead states)
- Used for UI panel management (Gameplay, Win, Lose states)

#### **Command Pattern**
- Command execution handler for sequential level initialization
- Commands include:
  - Board building with animations
  - Player line positioning
  - Enemy spawner setup
  - Defense UI building

#### **Service Locator Pattern**
- Centralized service management system
- Easy dependency injection
- Services include ProjectilePool, LevelManager, etc.

#### **Object Pooling**
- Efficient projectile management
- Reduces garbage collection overhead
- Configurable pool sizes per projectile type

### Game Systems

#### **Stat System**
The game features a sophisticated stat system with:
- **Base Stats**: Level-based stat progression
- **Stat Modifiers**: Flat, percentage add, and percentage multiplier types
- **Dynamic Updates**: Real-time stat recalculation
- **Listener Pattern**: Components can subscribe to stat changes

```
Stat Calculation Order:
1. Base Value
2. Flat Modifiers
3. Percentage Add Modifiers (summed)
4. Percentage Multiplier Modifiers (stacked)
```

#### **Damage System**
- Type-based damage with resistance calculations
- Resistable and non-resistable damage types
- Damage source tracking
- Integration with stat system for dynamic damage values

#### **Health System**
- Generic health component usable for any entity
- Percentage-based damage/healing
- God mode and untouchable states
- Death and revival mechanics
- Health bar visualization

#### **Status Effect System**
- Stackable and non-stackable effects
- Level-based effect data
- Temporary and permanent effects
- Priority system for conflicting effects
- Automatic cleanup on unit death

#### **Team System**
- GUID-based team identification
- Team-based targeting and collision detection
- Prevents friendly fire

### Board & Level System

#### **Board Builder**
- Dynamic grid generation based on level data
- Animated tile spawning with DOTween
- Configurable board sizes
- Tile placement validation

#### **Level System**
- Scriptable Object-based level data
- Configurable defense units and enemy waves
- Board size per level
- Level progression with save system

### Unit System

#### **Base Unit Class**
All units inherit from a base `Unit` class providing:
- Health management
- Stat container
- Status effect manager
- Team assignment
- Damage calculation
- Event system (OnDead, OnTakeDamage, OnHeal, OnRevive)

#### **Defense Units**
- Stationary defensive towers
- Attack interval-based firing
- Range-based targeting
- Directional projectile launching (Forward, Back, Left, Right)
- Health bar display

#### **Enemy Units**
- Movement along grid
- Target detection
- Melee attack system
- Death animations with sinking effect
- Move speed with animator integration

### Projectile System

#### **Linear Projectiles**
- Direction-based movement
- Max distance travel
- Team-based collision detection
- Trail and particle effects
- Scale animation
- Object pooling integration

#### **Projectile Launcher**
- Configurable launch points
- Support for multiple fire directions
- Particle system integration
- Damage data passing

### UI System

#### **Panel State Machine**
Manages game UI panels:
- Gameplay panel
- Win panel
- Lose panel

#### **Defense Unit Selection**
- Click-to-place tower system
- Unit count tracking
- Visual feedback for placement validity
- Event-driven communication

## 📁 Project Structure

```
_Project/
├── _Scripts/
│   ├── Board/              # Board and tile management
│   ├── Cores/
│   │   ├── Damages/        # Damage system
│   │   ├── Events/         # Event channels (ScriptableObjects)
│   │   ├── Health/         # Health system
│   │   ├── Stats/          # Stat system
│   │   ├── StatusEffects/  # Status effect system
│   │   ├── Teams/          # Team system
│   │   └── Units/          # Unit base classes and interfaces
│   ├── Defences/           # Defense unit implementation
│   ├── Enemies/            # Enemy unit implementation
│   ├── Initilazer/         # Level initialization commands
│   ├── Levels/             # Level data and management
│   ├── Managers/           # Game managers (Level, Save, Scene)
│   ├── Player/             # Player interaction systems
│   ├── Projectiles/        # Projectile system
│   ├── UI/                 # UI components
│   └── Utilities/          # Utility classes
│       ├── Commands/       # Command pattern implementation
│       ├── Services/       # Service locator
│       └── StateMachines/  # State machine implementation
```

## 🔧 Key Features

### Event-Driven Architecture
The game uses ScriptableObject-based event channels for decoupled communication:
- `VoidChannelSO`: Simple events with no parameters
- `UnitEventChannelSO`: Events that pass Unit references

### Extensibility
- Easy to add new unit types by inheriting from `Unit`
- New stat types through ScriptableObjects
- Custom status effects by extending `StatusEffect`
- Modular damage types with configurable resistances

### Animation Integration
- DOTween for smooth animations
- Animator integration for unit states
- Particle systems for visual effects

## 🎯 Gameplay Flow

1. **Level Initialization**
   - Board is built with animated tiles
   - Player line is positioned
   - Enemy spawner is configured
   - Defense UI is built

2. **Gameplay Loop**
   - Player selects and places defense units
   - Enemies spawn at intervals
   - Defense units attack enemies in range
   - Projectiles deal damage on collision
   - Game checks win/lose conditions

3. **Win Condition**
   - All enemies defeated

4. **Lose Condition**
   - Enemy reaches player line

## 🛠️ Technical Dependencies

- **Unity Engine** (version not specified in files)
- **DOTween**: Animation library
- **UniTask**: Async/await support
- **GuidSystem**: GUID-based identification
- **SaveSystem**: Save/load functionality
- **SceneLoadSystem**: Scene management

## 💾 Save System

The game implements a save system for:
- Current level progress
- Can be extended for unit upgrades, player resources, etc.

## 🎨 Design Patterns Used

1. **State Machine Pattern**: Unit behaviors and UI management
2. **Command Pattern**: Sequential command execution
3. **Object Pool Pattern**: Projectile management
4. **Service Locator Pattern**: Dependency management
5. **Observer Pattern**: Event channels and stat listeners
6. **Strategy Pattern**: Different attack behaviors
7. **Factory Pattern**: Unit spawning

## 📝 Code Examples

### Creating a New Defense Unit

```csharp
public class CustomDefenseUnit : DefenceUnit
{
    public override void Initialize()
    {
        base.Initialize();
        // Custom initialization
    }
}
```

### Adding a New Status Effect

```csharp
[CreateAssetMenu(fileName = "New Status Effect", menuName = "StatusEffects/New Effect")]
public class CustomStatusEffectData : StatusEffectData
{
    // Custom effect data
}

public class CustomStatusEffect : StatusEffect
{
    public override void ApplyEffect(Unit target)
    {
        base.ApplyEffect(target);
        // Custom effect logic
    }
}
```

### Creating a Level

Create a new `LevelSO` ScriptableObject and configure:
- Board size
- Defense units (unit type + count)
- Enemy units (unit type + count)
 
