using System;

public class RoomSizeVerificator : IVerificator<string>
{
	private readonly int _maxSize = 40;
	private readonly int _minSize = 10;

	public bool Check(string value)
	{
		try
		{
			var newValue = Convert.ToInt32(value);

			if (newValue < _maxSize && newValue > _minSize)
			{
				return true;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}
}
