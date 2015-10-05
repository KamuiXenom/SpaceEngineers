// define container to store uranium on one place
	string s_storage = "";

// define capacity of uranium in reactors
	float i_reactor_fuel_size = 2.5f;
	
// list of reactors
	List<IMyTerminalBlock> g_reactors = null;
	
// list of storages with uranium
	List<IMyInventory> g_ingots_storage = null;
	
// storage object
	IMyInventory g_storage = null;
	
// ECHO storage
	List<String> l_echo = null;

// constructor
	void Main( string argument ) {
		
	// reset echo
		l_echo = new List<String>();
		
	// find uranium storage
		if ( !getUraniumStorage() ) {
			
			l_echo.Add( "! centered storage container not found" );
			
		}			
		
	// get all reactors in current grid
		g_reactors = new List<IMyTerminalBlock>();
		GridTerminalSystem.GetBlocksOfType<IMyReactor>( g_reactors );
		
	// check count of reactors
		if ( g_reactors.Count > 0 ) {
		
		// info reactor count
			l_echo.Add( "* " + g_reactors.Count + " reactors found" );
		
		// find uranium ingots
			if ( !getUraniumIngots() ) {
				
				l_echo.Add( "! can't find any uranium ingot" );
				
			} else {
			
			// disable conveyor usage on all reactors
				disableConveyorUsage( g_reactors );
				
			// roll all reactors for refueling
				
			
			}
			
		}
		
	// no reactors
		else {
			
			l_echo.Add( "! no reactors found; Exit" );
			
		}
		
	// combine echo list items to string and out
		string s_echo = String.Join( "\n", l_echo.ToArray() );
		Echo( "" + s_echo );
		
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
	
// get storage for uranium_ingots
	bool getUraniumStorage() {
		
	// check if storage undefined
		if ( g_storage is IMyInventory )
			return true;
		
		IMyInventoryOwner o_storage = (IMyInventoryOwner)GridTerminalSystem.GetBlockWithName( s_storage );
		if ( o_storage is IMyInventoryOwner ) {
			g_storage = o_storage.GetInventory(0);
			return true;
		}
		
		return false;
		
	}
	
// get all uranium ingots in current available grid
	bool getUraniumIngots() {
		
		float i_all_amount = 0.0f;
		
	// get all storages
		List<IMyInventoryOwner> l_inventorys = new List<IMyInventoryOwner>();
		GetBlocksOfType<IMyInventoryOwner>( l_inventorys );
		
		if ( l_inventory.Count > 0 ) { 
		
		// roll found inventorys
			for ( int i = 0; i < l_inventory.Count; ++i ) {
				
				
				
			}
			
		// check amount
			if ( i_all_amount > 0.0f ) {return true;}
		
		}
		
	// ------
		return false;
		
	}
	
// move item from cargo to cargo by itemIndex
	bool moveItem( IMyInventory o_source_inventory, IMyInventory o_target_inventory, string s_item_name, float i_item_amount = 0.0f ) {
		
	// get item index of given itemname
		int i_itemSourceIndex = getItemIndex( o_source_inventory, s_item_name );
		
	// do
		return moveItemByIndex( o_source_inventory, o_target_inventory, i_itemSourceIndex, i_item_amount );
		
	}
	
// move item from cargo to cargo by itemname
	bool moveItemByIndex( IMyInventory o_source_inventory, IMyInventory o_target_inventory, int i_itemSourceIndex, float i_item_amount = 0.0f ) {
		
	// check inventorys, they may not be the same
		if ( o_source_inventory == o_target_inventory ) {return false;}
		
	// no index found exit
		if ( i_itemSourceIndex == 0 ) {return false;}
		
	// check can be move
		if ( !o_source_inventory.IsConnectedTo( o_target_inventory ) ) {
			
			IMyTerminalBlock s_target_inventory = (IMyTerminalBlock)o_target_inventory; 
			l_echo.Add( "! " + s_target_inventory.CustomName + " has no conveyor connection" );
			return false;
			
		}
		
	// convert float to MyFixedPoint
		if ( i_item_amount > 0.0f ) {
			
			VRage.MyFixedPoint o_item_amount = (VRage.MyFixedPoint)( i_item_amount );
			return o_target_inventory.TransferItemFrom( o_source_inventory, i_itemSourceIndex, null, true, o_item_amount );
			
		} else {
			
			return o_target_inventory.TransferItemFrom( o_source_inventory, i_itemSourceIndex, null, true, null );
			
		}
		
	}
	
// get index of given item in given inventory
	int getItemIndex( IMyInventory o_inventory, string s_item_name ) {
		
	// check if exists an item in this inventory
		if ( !o_inventory.IsItemAt(0) )
			return 0;
		
	// load all items of inventory
		List<IMyInventoryItem> l_items = o_inventory.GetItems();
		
	// check has items
		if ( l_items.Count > 0 ) {
			for ( int i = 0; i < l_items.Count; i++ ) {
				if ( Convert.ToString( l_items[i].Content.SubtypeName ) == s_item_name ) {
					return i;
				}
			}
		}
		
	// -------
		return 0;
		
	}

// apply action on group
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ){for( int i = 0; i < group.Count; ++i ){group[i].ApplyAction( action );}}
