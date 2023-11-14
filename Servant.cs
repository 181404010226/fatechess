
using Godot;
using System;

public class Servant : Unit
{
	// Reference to the master
	public Master Master { get; set; }

	// Number of pressures the servant is under
	public int PressureCount { get; set; }

	public Servant(Board gameboard):base(gameboard)
	{
		PressureCount = 0;
	}

	// Override the method to apply pressure to the servant
	public override void ApplyPressure()
	{
		base.ApplyPressure();
		PressureCount++;
		if (PressureCount >= 3)
		{
			Kill();
		}
	}

	// Override the method to relieve pressure from the servant
	public override void RelievePressure()
	{
		base.RelievePressure();
		PressureCount = Math.Max(0, PressureCount - 1);
	}

	// Method to be called when the master uses a command spell
	public virtual void CommandSpellEffect()
	{
		// Implement the effect of the command spell here
	}

	// Override the method to move the servant
	public override void Move(Vector2 newPosition)
	{
		if (IsAlive && !IsUnderPressure)
		{
			base.Move(newPosition);
		}
	}

	// Override the method to attack another unit
	public override void Attack(Unit target)
	{
		if (IsAlive && !IsUnderPressure)
		{
			base.Attack(target);
		}
	}
}
