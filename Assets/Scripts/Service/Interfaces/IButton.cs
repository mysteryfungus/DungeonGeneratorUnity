


using UnityEngine.UIElements;

/// <summary>
/// Делаю на случай, если придется делать возврат или повтор нажатия клавиши в результате использования
/// таких функций в редакторе, как "Отмена" или "Вперед".
/// </summary>
public interface IButton : ILink<Button>
{
	public abstract void OnClick();
}
