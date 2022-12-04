using System;

public interface INotifier
{
    event Action<string> Notification;
    event Action<string> LevelChanged;

}
