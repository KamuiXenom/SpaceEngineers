
// define capacity of uranium in reactors
	float i_reactor_fuel_size = 2.5f;
	
// list of reactors
	List<IMyTerminalBlock> g_reactors = null;

// constructor
	void Main( string argument ) {
		
	// get all reactors in current grid
		g_reactors = new List<IMyTerminalBlock>();
		GridTerminalSystem.GetBlocksOfType<IMyReactor>( g_reactors );
		
		//disableConveyorUse( g_reactors );
		
	}

// disable 'UseConveyorSystem'
	void disableConveyorUse( List<IMyTerminalBlock> group ) {
		for( int i = 0; i < group.Count; ++i ){
			IMyTerminalBlock block = group[i];
			if ( block.HasAction( "UseConveyor" ) ) {
				block.SetUseConveyorSystem( false );
			}
		}
	}
	
//? move item from cargo to other cargo

// apply action on group
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ){for( int i = 0; i < group.Count; ++i ){group[i].ApplyAction( action );}}
