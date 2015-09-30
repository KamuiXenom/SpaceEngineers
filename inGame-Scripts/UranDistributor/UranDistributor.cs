// define container to store uranium
	string s_uranium_storage = "";

// define capacity of uranium in reactors
	float i_reactor_fuel_size = 2.5f;
	
// list of reactors
	List<IMyTerminalBlock> g_reactors = null;
	
// storage object
	IMyInventory g_storage = null;

// constructor
	void Main( string argument ) {
		
	// find uranium storage
		if ( !getUraniumStorage() ) {
			Echo( "No storage for uranium" );
		}			
		
	// get all reactors in current grid
		g_reactors = new List<IMyTerminalBlock>();
		GridTerminalSystem.GetBlocksOfType<IMyReactor>( g_reactors );
		
	// check count of reactors
		if ( g_reactors.Count > 0 ) {
			
		// disable conveyor usage on all reactors
			disableConveyorUsage( g_reactors );
			
			
			
			
			
		}
		
	}

// disable 'UseConveyorSystem'
	void disableConveyorUsage( List<IMyTerminalBlock> group ) {
		for( int i = 0; i < group.Count; ++i ){
			IMyTerminalBlock block = group[i];
			if ( block.HasAction( "UseConveyor" ) ) {
				block.SetUseConveyorSystem( false );
			}
		}
	}
	
// get index of given item in given inventory
	int getItemIndex( IMyInventory o_inventory, string s_item_name ) {
		
	// load all items of inventory
		List<IMyInventoryItem> l_items = o_inventory.GetItems();
		
	// check has items
		if ( l_items.Count ) {
			for ( int i = 0; i < l_items.Count; i++ ) {
				if ( Convert.ToString( l_items[i].Content.SubtypeName ) == s_item_name ) {
					
					
					
				}
			}
		}
		
	}
	
// get storage for uranium_ingots
	bool getUraniumStorage() {
		
	// check if storage undefined
		if ( g_storage is IMyInventory )
			return true;
		
		IMyInventoryOwner o_storage = GridTerminalSystem.GetBlockWithName( s_uranium_storage );
		if ( o_storage is IMyInventoryOwner ) {
			g_storage = dstCargoByName.GetInventory(0);
			return true;
		}
		
		return false;
		
	}
	
// move item from cargo to cargo with given amount
	bool moveItem( int i_itemSourceIndex, m_item_amount, IMyInventory o_source_inventory, IMyInventory o_target_inventory ) {
		 
		//o_target_inventory.TransferItemFrom( o_source_inventory, i_item_source_index, i_item_target_index, true, m_item_amount );
		o_target_inventory.TransferItemFrom( o_source_inventory, i_itemSourceIndex, null, true, m_item_amount );
		
	}

// apply action on group
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ){for( int i = 0; i < group.Count; ++i ){group[i].ApplyAction( action );}}
