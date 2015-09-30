/**
*	execute an action on group.blocks
*
*	@param	list of IMyTerminalBlock all subtypes with actions
*	@param	action string
*/
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ) {
		for(int i = 0; i < group.Count; ++i){group[i].ApplyAction(action);}
	}

/**
*	door state of group
*
*	@param	list of IMyTerminalBlock subtype IMyDoor
*	@param	boolean true=Open false=Closed
*	@return boolean
*/
	bool GetDoorStateOfGroup( List<IMyTerminalBlock> door, Boolean shouldBe ) {
		var state = true;
		for(int i = 0; i < door.Count; ++i) {
			if(door[i] is IMyDoor) {
				var d = (IMyDoor)door[i];
				state = state && d.Open == shouldBe;
			}
		}
		return state;
	}

/**
*	set color on light group
*
*	@param	list of IMyTerminalBlock subtype IMyLightingBlock
*	@param	color definition by Color structure 'VRageMath.Color'
*/
	void SetColor( List<IMyTerminalBlock> blocks, Color color ) {
		for( int i = 0; i < blocks.Count; i++ ) {
			IMyLightingBlock light = blocks[i] as IMyLightingBlock;
			light.SetValue("Color", color);
		}
	}
	
/**
*	disable 'UseConveyorSystem'
*
*	checks the action 'UseConveyor' before execute
*
*	@param list of IMyTerminalBlock
*/
	void disableConveyorUse( List<IMyTerminalBlock> group ) {
		for( int i = 0; i < group.Count; ++i ){
			IMyTerminalBlock block = group[i];
			if ( block.HasAction( "UseConveyor" ) ) {
				block.SetUseConveyorSystem( false );
			}
		}
	}

/**
*	move spezific item by index
*
*	- exit by same inventory
*	- exit by zero index of item
*	- if an amount defined, this will convert to needed 'VRage.MyFixedPoint'
*	
*	@param	IMyInventory	source
*	@param	IMyInventory	target
*	@param	integer			item index in source inventory
*	@param	float			item amount (optional); not set move all from source
*	@return	boolean
*/	
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
	
/**
*	move spezific item by subtypename
*
*	- needs method moveItemByIndex
*	- needs method getItemIndex for index detection
*
*	@param	IMyInventory	source
*	@param	IMyInventory	target
*	@param	string			item subtypename
*	@param	float			item amount (optional); not set move all from source
*	@return	boolean
*/
	bool moveItem( IMyInventory o_source_inventory, IMyInventory o_target_inventory, string s_item_name, float i_item_amount = 0.0f ) {
		
	// get item index of given itemname
		int i_itemSourceIndex = getItemIndex( o_source_inventory, s_item_name );
		
	// do
		return moveItemByIndex( o_source_inventory, o_target_inventory, i_itemSourceIndex, i_item_amount );
		
	}
	
/**
*	gets the index of given item in inventory
*
*	- return 0: inventory has no items
*	- return 0: if item can be not found in given inventory
*
*	@param	IMyInventoy		inventory to find item
*	@param	string			item subtypename
*	@return	integer			item index in inventory
*/
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
	
	
	
	
	
	