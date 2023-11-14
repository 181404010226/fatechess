
using Godot;
using System;
using System.Collections.Generic;

public class Cavalry : Servant
{
	public Cavalry(Board gameboard) : base(gameboard)
	{
		// Set the movement range and attack range according to the project description
		Movement = 1;
		AttackRange = 1;
	}

	// Override the method to move the cavalry
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Cavalry移动中");
		GD.Print("Current position: " + Position.x + ", " + Position.y);
		if (!IsUnderPressure)
		{
			// Get the cells in straight line within 1 range from the current position
			List<Unit> straightMoves = gameBoard.GetCellsInLine(this, 1, 1);

			List<Unit> possibleMoves = new List<Unit>();

			// For each cell in straight line, get the cells in diagonal within 1 range
			foreach (Unit cell in straightMoves)
			{
				List<Unit> diagonalMoves = gameBoard.GetCellsInDiagonal(cell, 1, 1);
				possibleMoves.AddRange(diagonalMoves);
			}
			possibleMoves.AddRange(straightMoves);

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
		// The cavalry can attack a unit that is one square diagonally away from it
		Vector2 diff = target.Position - Position;
		if (IsAlive && !IsUnderPressure && Math.Abs(diff.x) == 1 && Math.Abs(diff.y) == 1)
		{
			base.Attack(target);
		}
	}
}
