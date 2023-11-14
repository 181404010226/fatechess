using Godot;
using System;
using System.Collections.Generic;

public class Assassin : Servant
{
	public Assassin(Board gameboard) : base(gameboard)
	{
		// Set the movement range and attack range according to the project description
		Movement = 1;
		AttackRange = 1;
	}

	// Override the method to move the assassin
	public override void Move(Vector2 newPosition)
	{
		GD.Print("Assassin移动中");
		if (!IsUnderPressure)
		{
			// Get the cells in diagonal within 1 range from the current position
			List<Unit> diagonalMoves = gameBoard.GetCellsInDiagonal(this, 1, 1);

			List<Unit> possibleMoves = new List<Unit>();

			// For each cell in diagonal, get the cells in straight line within 1 range
			foreach (Unit cell in diagonalMoves)
			{
				List<Unit> straightMoves = gameBoard.GetCellsInLine(cell, 1, 1);
				possibleMoves.AddRange(straightMoves);
			}
			possibleMoves.AddRange(diagonalMoves);

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
		// The assassin's attack range is diagonal
		if (IsAlive && !IsUnderPressure && Math.Abs(Position.x - target.Position.x) == Math.Abs(Position.y - target.Position.y))
		{
			base.Attack(target);
		}
	}

	// Method to be called when the master uses a command spell
	public override void CommandSpellEffect()
	{
		// The assassin's movement ignores pressure for the turn when the command spell is used
		IsUnderPressure = false;
		Move(Position);
		IsUnderPressure = true;
	}
}
