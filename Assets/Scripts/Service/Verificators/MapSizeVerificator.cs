using System;

public class MapSizeVerificator : IVerificator<string>
{
	private readonly int _maxSize = 100;
	private readonly int _minSize = 20;

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