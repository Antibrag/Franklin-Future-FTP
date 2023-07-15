using Godot;
using System;

public partial class HUD : Control
{
	public override void _Process(double delta)
	{
		GetNode<Label>("StepsCount").Text = "Steps:\n" + Convert.ToString(LevelControl.CurrentSteps);
	}

	private async void ShowLabel(Label obj, float showSpeed) {
		obj.Show();
		for (float i = 0; i <= 1; i += showSpeed)
        {
            obj.Modulate = new Color(obj.Modulate.R, obj.Modulate.G, obj.Modulate.B, i);
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
        }
	}

	private async void HideLabel(Label obj, float showSpeed) {
		for (float i = 1; i >= 0; i -= showSpeed)
        {
            obj.Modulate = new Color(obj.Modulate.R, obj.Modulate.G, obj.Modulate.B, i);
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
        }
		obj.Hide();
	}

	public async void ShowLevelName(string name) {
		Label LevelNameLabel = GetNode<Label>("LevelName");
		LevelNameLabel.Text = name;

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		ShowLabel(LevelNameLabel, 0.01f);
		await ToSignal(GetTree().CreateTimer(2), "timeout");
		HideLabel(LevelNameLabel, 0.01f);
	}
}
