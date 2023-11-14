
using Godot;
using System;
using System.Collections.Generic;

public class Board : Node2D
{ 
	// The currently selected unit
	public Unit selectedUnit=null;
	// The size of the board
	private const int BoardWidth = 7;
	private const int BoardHeight = 9;

	// The board represented as a 2D array of units
	private Unit[,] board;

	// The list of masters and servants
	private List<Master> masters;
	private List<Servant> servants;

	public Board()
	{
		board = new Unit[BoardWidth, BoardHeight];
		masters = new List<Master>();
		servants = new List<Servant>();
	}
	// Method to reset the color of all units to white
	public void ResetUnitColors()
	{
		foreach (Unit unit in board)
		{
			if (unit != null)
			{
				unit.Modulate = Colors.White;
			}
		}
	}

	// Method to add a unit to the board
	public void AddUnit(Unit unit, Vector2 position)
	{
		if (position.x >= 0 && position.x < BoardWidth && position.y >= 0 && position.y < BoardHeight)
		{
			board[(int)position.x, (int)position.y] = unit;
			unit.Position = position;

			if (unit is Master master)
			{
				masters.Add(master);
			}
			else if (unit is Servant servant)
			{
				servants.Add(servant);
			}
		}
	}

	// Method to remove a unit from the board
	public void RemoveUnit(Unit unit)
	{
		Vector2 position = unit.Position;
		if (position.x >= 0 && position.x < BoardWidth && position.y >= 0 && position.y < BoardHeight)
		{
			board[(int)position.x, (int)position.y] = null;

			if (unit is Master master)
			{
				masters.Remove(master);
			}
			else if (unit is Servant servant)
			{
				servants.Remove(servant);
			}
		}
	}

	// Method to get the unit at a specific position
	public Unit GetUnitAtPosition(Vector2 position)
	{
		if (position.x >= 0 && position.x < BoardWidth && position.y >= 0 && position.y < BoardHeight)
		{
			return board[(int)position.x, (int)position.y];
		}
		return null;
	}

	// Method to move a unit to a new position
	public void MoveUnit(Unit unit, Unit clickedUnit)
	{
		int unitIndex = unit.GetIndex();
		int clickedUnitIndex = clickedUnit.GetIndex();

		unit.GetParent().MoveChild(unit, clickedUnitIndex);
		clickedUnit.GetParent().MoveChild(clickedUnit, unitIndex);
	// Update the board information after swapping the units
		Vector2 tempPosition = unit.Position;
		unit.Position = clickedUnit.Position;
		clickedUnit.Position = tempPosition;
		board[(int)unit.Position.x, (int)unit.Position.y] = unit;
		board[(int)clickedUnit.Position.x, (int)clickedUnit.Position.y] = clickedUnit;
	}

	// Method to get cells in a straight line from a unit's position
	public List<Unit> GetCellsInLine(Unit unit, int minRange, int maxRange)
	{
		List<Unit> cellsInLine = new List<Unit>();
		Vector2 position = unit.Position;

		// Check cells in a straight line in all four directions
		Vector2[] directions = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
		foreach (Vector2 direction in directions)
		{
			for (int range = minRange; range <= maxRange; range++)
			{
				Vector2 cellPosition = position + direction * range;
				if (IsPositionWithinBoard(cellPosition))
				{
					if (GetUnitAtPosition(cellPosition).GetType() 
					!= typeof(Unit))
					{
						break;
					}
					cellsInLine.Add(GetUnitAtPosition(cellPosition));
				}
			}
		}
		return cellsInLine;
	}

	public List<Unit> GetCellsInStraightLineWithTurns(Unit unit, int currentDepth, int maxDepth)
	{
		List<Unit> cells = new List<Unit>();
		if (currentDepth >= maxDepth)
		{
			return cells;
		}

		// Get the cells in the four directions
		List<Unit> directions = new List<Unit>
		{
			GetUnitAtPosition(unit.Position + new Vector2(1, 0)),
			GetUnitAtPosition(unit.Position + new Vector2(-1, 0)),
			GetUnitAtPosition(unit.Position + new Vector2(0, 1)),
			GetUnitAtPosition(unit.Position + new Vector2(0, -1))
		};

		foreach (Unit direction in directions)
		{
			if (direction != null && !cells.Contains(direction)
			&& direction.GetType()== typeof(Unit))
			{
				cells.Add(direction);
				cells.AddRange(GetCellsInStraightLineWithTurns(direction, currentDepth + 1, maxDepth));
			}
		}

		return cells;
	}

	public List<Unit> GetCellsInDiagonal(Unit unit, int minRange, int maxRange)
	{
		List<Unit> cellsInDiagonal = new List<Unit>();
		Vector2 position = unit.Position;

		// Check the four diagonal directions
		Vector2[] offsets = { new Vector2(-1, -1), new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1) };
		foreach (Vector2 offset in offsets)
		{
			for (int range = minRange; range <= maxRange; range++)
			{
				Vector2 diagonalPosition = position + offset * range;
				if (IsPositionWithinBoard(diagonalPosition))
				{
					Unit cell = GetUnitAtPosition(diagonalPosition);
					if (cell != null && cell.GetType() == typeof(Unit))
					{
						cellsInDiagonal.Add(cell);
					}
				}
			}
		}
		return cellsInDiagonal;
	}

	public List<Unit> GetAdjacentCells(Unit unit)
	{
		List<Unit> adjacentCells = new List<Unit>();
		Vector2 position = unit.Position;

		// Check the four adjacent cells
		Vector2[] offsets = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
		foreach (Vector2 offset in offsets)
		{
			Vector2 adjacentPosition = position + offset;
			if (IsPositionWithinBoard(adjacentPosition))
			{
				adjacentCells.Add(GetUnitAtPosition(adjacentPosition));
			}
		}
		foreach (Unit cell in adjacentCells)
		{
			GD.Print("Unit position: " + cell.Position.x + ", " + cell.Position.y);
		}
		return adjacentCells;
	}

	// Method to check if a position is within the board
	public bool IsPositionWithinBoard(Vector2 position)
	{
		return position.x >= 0 && position.x < BoardWidth && position.y >= 0 && position.y < BoardHeight;
	}

	// Method to get the list of masters
	public List<Master> GetMasters()
	{
		return masters;
	}

	// Method to get the list of servants
	public List<Servant> GetServants()
	{
		return servants;
	}
}
