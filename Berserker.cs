
using Godot;
using System;
using System.Collections.Generic;

public class Berserker : Servant
{
	public Berserker(Board gameboard) : base(gameboard)
	{
		Movement = 2; // Berserker can move 1 to 2 squares diagonally
		AttackRange = 1; // Berserker's attack range is all surrounding squares
	}

	// Override the method to move the Berserker
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Berserker移动中");
		if (!IsUnderPressure)
		{
			// Get the cells in diagonal within 1 range from the current position
			List<Unit> diagonalMoves = gameBoard.GetCellsInDiagonal(this, 1, 1);

			// Change the color of the possible move cells to green
			foreach (Unit cell in diagonalMoves)
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
		// Berserker can attack all surrounding squares
		if (IsAlive && !IsUnderPressure && Math.Abs(target.Position.x - Position.x) <= AttackRange && Math.Abs(target.Position.y - Position.y) <= AttackRange)
		{
			base.Attack(target);
		}
	}

	// Override the method to apply pressure to the Berserker
	public override void ApplyPressure()
	{
		base.ApplyPressure();
		// Berserker is not affected by repeated pressure
		if (IsUnderPressure)
		{
			PressureCount = 1;
		}
	}
}
