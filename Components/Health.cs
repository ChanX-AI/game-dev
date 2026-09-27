using static System.Math;
using Platformer2D.Core;

namespace Platformer2D.Components;

public class Health : Component {
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }

    public Health(int maxHealth) {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public bool TakeDamage(int amount) {
        if (amount <= 0) return false;

        int oldHealth = CurrentHealth;
        CurrentHealth = Max(0, CurrentHealth - amount);

        return oldHealth != CurrentHealth;
    }

    public bool Heal(int amount) {
        if (amount <= 0) return false;

        int oldHealth = CurrentHealth;
        CurrentHealth = Min(MaxHealth, CurrentHealth + amount);

        return oldHealth != CurrentHealth;
    }

}