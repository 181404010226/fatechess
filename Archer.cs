using Godot;
using System;
using System.Collections.Generic;

public class Archer : Servant
{
	public Archer(Board gameboard) : base(gameboard)
	{
		// Set the movement range and attack range according to the project description
		Movement = 1;
		AttackRange = 8;
	}

	// Override the method to move the Archer
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Archer移动中");
		GD.Print("Current position: " + Position.x + ", " + Position.y);
		if (!IsUnderPressure)
		{
			// Get the cells in straight line within 1~2 range from the current position
			List<Unit> possibleMoves = gameBoard.GetCellsInLine(this, 1, 2);

			// Change the color of the possible move cells to green
			foreach (Unit cell in possibleMoves)
			{
				cell.Modulate = new Color(0.5f, 1, 0.5f);
			}
		}
		else
		{
			List<Unit> adjacentCells = gameBoard.GetAdjacentCells(this);

			 // Change the color of the adjacent cells to green
			foreach (Unit cell in adjacentCells)
			{
				cell.Modulate = new Color(0.5f, 1, 0.5f);
			}
		}
	}

	// Override the method to attack another unit
	public override void Attack(Unit target)
	{
		// The Archer's attack range is the same as the Chinese chess horse's movement range
		if (IsAlive && !IsUnderPressure)
		{
			// Implement attack logic here
		}
	}

	// Method to check if a position is within the Archer's attack range
	public bool IsWithinAttackRange(Vector2 position)
	{
		// The Archer's attack range is the same as the Chinese chess horse's movement range
		// Implement the logic to check if the position is within the Archer's attack range
		return false;
	}
}
