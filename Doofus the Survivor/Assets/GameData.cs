using System;

[Serializable]
public class PlayerData
{
    public int speed;
}

[Serializable]
public class PulpitData
{
    public int min_pulpit_destroy_time;
    public int max_pulpit_destroy_time;
    public double pulpit_spawn_time;
}

[Serializable]
public class GameData
{
    public PulpitData pulpit_data;
    public PlayerData player_data;
}

