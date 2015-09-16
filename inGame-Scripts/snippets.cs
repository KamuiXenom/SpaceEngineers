// execute an action on group.blocks
	void ApplyActionOnGroup( List<IMyTerminalBlock> group, String action ) {
		for(int i = 0; i < group.Count; ++i){group[i].ApplyAction(action);}
	}

// door state of group
	Boolean GetDoorStateOfGroup(List<IMyTerminalBlock> door, Boolean shouldBe) {
		var state = true;
		for(int i = 0; i < door.Count; ++i) {
			if(door[i] is IMyDoor) {
				var d = (IMyDoor)door[i];
				state = state && d.Open == shouldBe;
			}
		}
		return state;
	}