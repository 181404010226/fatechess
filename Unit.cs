using Godot;
using System;

public class Unit : TextureRect
{
	protected Board gameBoard;
	// Position on the board
	public Vector2 Position { get; set; }

	// Movement range
	public int Movement { get; set; }

	// Attack range
	public int AttackRange { get; set; }

	// Whether the unit is under pressure
	public bool IsUnderPressure { get; set; }

	// Whether the unit is alive
	public bool IsAlive { get; set; }

	public Unit(Board gameboard)
	{
		this.gameBoard = gameboard;
		IsAlive = true;
	}

	// Method to move the unit
	public virtual void Move(Vector2 newPosition)
	{
		GD.Print("移动中");
		GD.Print("Current position: " + Position.x + ", " + Position.y);
		if (IsAlive)
		{
		}
	}

	// Method to attack another unit
	public virtual void Attack(Unit target)
	{
		if (IsAlive)
		{
			// Implement attack logic here
		}
	}

	// Method to apply pressure to the unit
	public virtual void ApplyPressure()
	{
		IsUnderPressure = true;
	}

	// Method to relieve pressure from the unit
	public virtual void RelievePressure()
	{
		IsUnderPressure = false;
	}
	// Method to move the unit when clicked
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			// 获取UI在屏幕当中的位置
			Vector2 unitPositionInScreen = new Rect2(GetGlobalRect().Position, RectSize).Position;
			if (new Rect2(unitPositionInScreen, RectSize).HasPoint(mousePosition))
			{

				// Check if the clicked unit is a green cell
				if (gameBoard.selectedUnit!=null && 
					this.Modulate == new Color(0.5f, 1, 0.5f))
				{
					// Print the positions before swapping
					GD.Print("Before swapping: ");
					GD.Print("Current unit position: " + Position.x + ", " + Position.y);
					GD.Print("Selected unit position: " + gameBoard.selectedUnit.Position.x + ", " + gameBoard.selectedUnit.Position.y);

					// Move the current unit to the clicked position
					gameBoard.MoveUnit(this, gameBoard.selectedUnit);
					// Reset the selected unit
					gameBoard.selectedUnit = null;
					
					gameBoard.ResetUnitColors();
				}
				else
				{
					gameBoard.ResetUnitColors();
					Move(new Vector2(0,0));
					gameBoard.selectedUnit = this;
				}
			}
		}
	}

	// Method to kill the unit
	public void Kill()
	{
		IsAlive = false;
	}
}
