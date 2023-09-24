using Godot;
using System;

public partial class HUD : Control
{
    public override void _Process(double delta) 
	{
		GetNode<Label>("FPS").Text = $"FPS: {Engine.GetFramesPerSecond()}";
		GetNode<Label>("MemoryUse").Text = $"Memory use: {(float)OS.GetStaticMemoryUsage() / 1024 / 1024}";
		GetNode<Label>("StepsCount").Text = "Steps:\n" + Convert.ToString(LevelControl.CurrentLevel.Steps["CurrentSteps"]);
	}

    private async void ShowObj(Control obj, float showSpeed) {
		obj.Show();
		for (float i = 0; i <= 1; i += showSpeed)
        {
            obj.Modulate = new Color(obj.Modulate.R, obj.Modulate.G, obj.Modulate.B, i);
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
        }
	}

	private async void HideObj(Control obj, float showSpeed) {
		for (float i = 1; i >= 0; i -= showSpeed)
        {
            obj.Modulate = new Color(obj.Modulate.R, obj.Modulate.G, obj.Modulate.B, i);
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
        }
		obj.Hide();
	}

	public async void ShowEleperator(string text) {
		Label Eleperator = GetNode<Label>("Eleperator");
		Eleperator.Text = text;

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		ShowObj(Eleperator, 0.01f);
		await ToSignal(GetTree().CreateTimer(2), "timeout");
		HideObj(Eleperator, 0.01f);
	}
}
