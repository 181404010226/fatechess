
using Godot;
using System;

public class Master : Unit
{
	// The servant this master controls
	public Servant Servant { get; set; }

	// Whether the master has used their command spell
	public bool HasUsedCommandSpell { get; set; }

	public Master(Board gameboard) : base(gameboard)
	{
		Movement = 1;
		AttackRange = 0; // Masters cannot attack
		HasUsedCommandSpell = false;
	}

	// Method to use the command spell
	public void UseCommandSpell()
	{
		if (!HasUsedCommandSpell)
		{
			// Implement command spell logic here
			HasUsedCommandSpell = true;
		}
	}

	// Override the move method to implement the master's special movement rules
	public override void Move(Vector2 newPosition)
	{
		
		GD.Print(" Master移动中");
	}

	// Override the attack method to prevent the master from attacking
	public override void Attack(Unit target)
	{
		// Masters cannot attack
	}

	// Method to summon the servant
	public void SummonServant()
	{
		if (Servant != null && Servant.IsAlive)
		{
			// Implement summoning logic here
		}
	}
}
