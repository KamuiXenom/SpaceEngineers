// define container to store uranium
	string s_uranium_storage = "";

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
	
// move item from cargo to cargo with given amount
	bool moveItem( string s_item, float i_amount, IMyInventory o_source, IMyInventory o_target ) {
		
		//This ist the Method to Transfer the Selectet Item from the Source_inventory to the Destination Inventory   
		dstInventory.TransferItemFrom( source_invetory, itemCount, null, true, null );
		
	}
	
//? move item from cargo to other cargo

// apply action on group
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ){for( int i = 0; i < group.Count; ++i ){group[i].ApplyAction( action );}}
