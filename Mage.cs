
using Godot;
using System;
using System.Collections.Generic;

public class Mage : Servant
{
	public Mage(Board gameboard) : base(gameboard)
	{
		// Set the movement range and attack range according to the project description
		Movement = 2;
		AttackRange = 8;
	}

	// Override the method to move the mage
	public override void Move(Vector2 newPosition)
	{
		
		GD.Print("Mage移动中");
		if (!IsUnderPressure)
		{
			// Get the cells in straight line and diagonal within 1 range from the current position
			List<Unit> straightMoves = gameBoard.GetCellsInLine(this, 1, 1);
			List<Unit> diagonalMoves = gameBoard.GetCellsInDiagonal(this, 1, 1);

			List<Unit> possibleMoves = new List<Unit>();
			possibleMoves.AddRange(straightMoves);
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
		// The mage can attack units at a straight or diagonal distance of 2
		if (IsAlive && !IsUnderPressure)
		{
			Vector2 distance = target.Position - Position;
			if (Math.Abs(distance.x) <= 2 && Math.Abs(distance.y) <= 2)
			{
				base.Attack(target);
			}
		}
	}

	// Method to be called when the master uses a command spell
	public override void CommandSpellEffect()
	{
		// The mage's movement ignores pressure for the current turn
		IsUnderPressure = false;
	}
}
