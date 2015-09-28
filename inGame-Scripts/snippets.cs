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
*
*/
	Boolean GetDoorStateOfGroup( List<IMyTerminalBlock> door, Boolean shouldBe ) {
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
	void SetColor(List<IMyTerminalBlock> blocks, Color color) {
		for( int i = 0; i < blocks.Count; i++ ) {
			IMyLightingBlock light = blocks[i] as IMyLightingBlock;
			light.SetValue("Color", color);
		}
	}
	
/**
*	disable 'UseConveyorSystem'
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
