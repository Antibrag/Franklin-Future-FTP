using Godot;

public partial class WallChecker : Area3D
{
	public bool IsCollidingObj { get; set; }

	public void OnCheckerExited_Area(Area3D area) 
	{
		IsCollidingObj = false; 
	}    

    public void OnCheckerEntered_Area(Area3D area) 
	{
		if (area.IsInGroup("Wall"))
			IsCollidingObj = true;
	} 
	
}
