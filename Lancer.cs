
using Godot;
using System;
using System.Collections.Generic;

public class Lancer : Servant
{
	public Lancer(Board gameboard) : base(gameboard)
	{
		// Lancer can move in a straight line from 1 to 3 squares
		Movement = 3;
		// Lancer's attack range is the four squares directly adjacent to it
		AttackRange = 1;
	}

	// Override the method to move the Lancer
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Lancer移动中");
		GD.Print("Current position: " + Position.x + ", " + Position.y);
		if (!IsUnderPressure)
		{
			// Get the cells in straight line within 1~2 range from the current position
			List<Unit> possibleMoves = gameBoard.GetCellsInLine(this, 1, 3);

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
		// Check if the target is within attack range
		if (IsAlive && !IsUnderPressure && (target.Position - Position).Length() <= AttackRange)
		{
			base.Attack(target);
		}
	}
}
