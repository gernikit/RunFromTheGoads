using System;

public abstract class GlobalEvent { }

public class GameStartEvent : GlobalEvent { }

public class LevelRestartEvent : GlobalEvent { }

public class PlayerLostEvent : GlobalEvent { }

public class PlayerWinEvent : GlobalEvent { }
