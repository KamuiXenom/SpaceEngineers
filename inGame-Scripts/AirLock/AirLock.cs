// name settings
	string g_doorOutsideName = "TorAussen";
	string g_doorInsideName = "TorInnen";
	string g_airVentName = "Luft";
	string g_timerName = "Zeitschaltuhr";

// group settings
	bool g_doorOutsideIsGroup = true;
	bool g_doorInsideIsGroup = true;
	bool g_airVentIsGroup = true;

// runtime variables
	bool g_initialize = true;

	IMyTimerBlock g_timer = null;

	List<IMyAirVent> g_airVent = null;
	List<IMyTerminalBlock> g_doorOutside = null;
	List<IMyTerminalBlock> g_doorInside = null;

	List<Func<int>> g_queuedActions = new List<Func<int>>();

// constructor
	void Main() {
		
		if( g_initialize ) {
			
			getBlocks();
			g_initialize = false;
			
		}
	   
		if(g_queuedActions.Count == 0) {
			
			if(GetDoorStateOfGroup(g_doorOutside, true)) {
				
				g_queuedActions.Add(CloseDoorOutside);
				g_queuedActions.Add(Pressurize);
				g_queuedActions.Add(OpenDoorInside);
				
			} else {
				
				g_queuedActions.Add(CloseDoorInside);
				g_queuedActions.Add(Depressurize);
				g_queuedActions.Add(OpenDoorOutside);
				
			}
			
		}
	 
		g_queuedActions[0]();
		g_queuedActions.RemoveAt(0);
		
	}

// get all needed blocks
	void getBlocks() {
		
	// get grouped blocks
		var groups = GridTerminalSystem.BlockGroups;
		for(int i = 0; i < groups.Count; ++i) {
			if ( g_doorOutsideIsGroup && groups[i].Name == g_doorOutsideName ) { g_doorOutside = groups[i].Blocks; }
			else if ( g_doorInsideIsGroup && groups[i].Name == g_doorInsideName ) { g_doorInside = groups[i].Blocks; }
			else if ( g_airVentIsGroup && groups[i].Name == g_airVentName ) { g_airVent = groups[i].Blocks; }
		}
		
	// get single blocks
		if ( !g_doorOutsideIsGroup ) { g_doorOutside[0] = (IMyTerminalBlock)GridTerminalSystem.GetBlockWithName( g_doorOutsideName ); }
		if ( !g_doorInsideIsGroup ) { g_doorOutside[0] = (IMyTerminalBlock)GridTerminalSystem.GetBlockWithName( g_doorInsideName ); }
		if ( !g_airVentIsGroup ) { g_airVent[0] = (IMyAirVent)GridTerminalSystem.GetBlockWithName( g_airVentName ); }
		
		g_timer = (IMyTimerBlock)GridTerminalSystem.GetBlockWithName( g_timerName );
		
	}
	
// apply action on group
	void ApplyActionOnGroup(List<IMyTerminalBlock> group, String action){for( int i = 0; i < group.Count; ++i ){group[i].ApplyAction( action );}}

// action methods
	int CloseDoorOutside() {
		ApplyActionOnGroup(g_doorOutside, "Open_Off");
		g_timer.SetValue("TriggerDelay", 10.0f);
		g_timer.ApplyAction("Start");
		return 0;
	}
	 
	int Pressurize() {
		ApplyActionOnGroup(g_airVent, "Depressurize_Off");
		g_timer.SetValue("TriggerDelay", 6.0f);
		g_timer.ApplyAction("Start");
		return 0;
	}
	 
	int Depressurize() {
		ApplyActionOnGroup(g_airVent, "Depressurize_On");
		g_timer.SetValue("TriggerDelay", 7.0f);
		g_timer.ApplyAction("Start");
		return 0;
	}
	 
	int OpenDoorInside() {
		ApplyActionOnGroup(g_doorInside, "Open_On");
		return 0;
	}
	 
	int CloseDoorInside() {
		ApplyActionOnGroup(g_doorInside, "Open_Off");
		g_timer.SetValue("TriggerDelay", 10.0f);
		g_timer.ApplyAction("Start");
		return 0;
	}
	
	int OpenDoorOutside() {
		ApplyActionOnGroup(g_doorOutside, "Open_On");
		return 0;
	}
	 
	Boolean GetDoorStateOfGroup( List<IMyTerminalBlock> door, Boolean shouldBe ) {
	   var state = true;
		for( int i = 0; i < door.Count; ++i ) {
			if( door[i] is IMyDoor ) {
				var d = (IMyDoor)door[i];
				state = state && d.Open == shouldBe;
			}
		}
		return state;
	}