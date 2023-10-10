using Godot;

public partial class Menu : Node
{
	private Control ActivitiesMenu;

	public override void _Ready() => ActivitiesMenu = GetNode<Control>("ActivitiesMenu");

	private async void HideControlComponent(Control control_node, double side_speed)
	{
		for (float i = control_node.Modulate.A; i >= 0; i -= 0.01f)
		{
			control_node.Modulate = new Color(control_node.Modulate, i);
			GD.Print("Change canvas item modulate - " + control_node + ": " + control_node.Modulate.ToString());
			await ToSignal(GetTree().CreateTimer(side_speed), "timeout");
		}
		control_node.Hide();
	}

	private async void ShowControlComponent(Control control_node, double side_speed)
	{
		control_node.Show();
		for (float i = control_node.Modulate.A; i <= 1; i += 0.01f)
		{
			control_node.Modulate = new Color(control_node.Modulate, i);
			GD.Print("Change canvas item modulate - " + control_node + ": " + control_node.Modulate.ToString());
			await ToSignal(GetTree().CreateTimer(side_speed), "timeout");
		}
	}

	public async void StartGame()
	{
		HideControlComponent(ActivitiesMenu, 0.01f);
		await ToSignal(GetTree().CreateTimer(1), "timeout");	
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
	public void OnResetLevelsButtonPressed() => LevelControl.ResetCompleteLevels = true;

	public void OnStartButtonPressed() => StartGame();
}

