
using Godot;
using System;
using System.Collections.Generic;

public class Swordsman : Servant
{
	public Swordsman(Board gameboard) : base(gameboard)
	{
		// Swordsman can move one square in a straight line and can move twice
		Movement = 1;

		// Swordsman's attack range is one square in all four directions
		AttackRange = 1;
	}

	// Override the method to move the Swordsman
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Swordsman移动中");
		GD.Print("Current position: " + Position.x + ", " + Position.y);
		if (!IsUnderPressure)
		{
			List<Unit> possibleMoves = gameBoard.GetCellsInStraightLineWithTurns(this, 0, 2);
  
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
		// Check if the target is within the attack range
		if (IsAlive && !IsUnderPressure && (target.Position - Position).Length() <= AttackRange)
		{
			base.Attack(target);
		}
	}
}
