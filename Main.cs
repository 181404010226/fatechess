using Godot;
using System;

public class Main : Node
{
	// The game board
	private Board board;
	// Instance of the game controller
	private GameController gameController;
	// Instance of the grid container
	private GridContainer grid;
	// Size of each cell
	private Vector2 cellSize;
	// 顶部栏大小
	const int topBar=50;
	// Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		board = new Board();
		gameController = new GameController();

		grid = new GridContainer();
		grid.MarginTop = topBar;
		grid.Columns = 9;
		string[] unittypes = { "caster", "assassin", "rider", "berserker", "lancer", "saber", "archer" };
		for (int i = 0; i < 63; i++)
		{
			Unit cell= new Unit(board);
			// 生成棋子
			if (i % 9 == 0 )
			{
				cell = generaterUnit(i/9 % 7);
				cell.Texture = (Texture)GD.Load($"res://picture/{unittypes[i/9 % 7]}.png");
			}
			else if (i % 9 == 8)
			{		
				cell = generaterUnit(6-i/9 % 7);
				cell.Texture = (Texture)GD.Load($"res://picture/{unittypes[6-i/9 % 7]}master.png");
			}
			else
			{
				cell.Texture = (Texture)GD.Load("res://icon.png");
			}
			cell.Position = new Vector2(i / 9,i % 9);
			board.AddUnit(cell, cell.Position);
		

			 // Scale the texture to match the cell size
			cell.StretchMode = TextureRect.StretchModeEnum.Scale;
			cell.Expand = true; 
			cell.MarginTop = 0;
			cell.MarginRight = 0;
			cell.MarginBottom = 0;
			cell.MarginLeft = 0;
			grid.AddChild(cell);
		}

		AddChild(grid);
	}

	private Unit generaterUnit(int type)
	{
		Unit unit;
		switch (type % 7)
		{
			case 0:
				unit = new Mage(board);
				break;
			case 1:
				unit = new Assassin(board);
				break;
			case 2:
				unit = new Cavalry(board);
				break;
			case 3:
				unit = new Berserker(board);
				break;
			case 4:
				unit = new Lancer(board);
				break;
			case 5:
				unit = new Swordsman(board);
				break;
			case 6:
				unit = new Archer(board);
				break;
			default:
				unit = new Unit(board);
				break;
		}
		return unit;
	}

	// Called every frame
	public override void _Process(float delta)
	{
		// Start the game if it hasn't started yet
		// gameController.StartGame();

		// 获取视窗的大小
		Vector2 windowSize = GetViewport().Size;

		// 计算每个格子的大小
		cellSize = new Vector2(
			windowSize.x / grid.Columns-5, 
			(windowSize.y-topBar) *9 / grid.Columns/7-5);

		// 更新每个格子的大小
		foreach (TextureRect cell in grid.GetChildren())
		{
			cell.RectMinSize = cellSize;
		}
	}
}
