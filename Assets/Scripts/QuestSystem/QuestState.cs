using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
   REQUIREMENTS_NOT_MET,
   CAN_START, //can start quest
   IN_PROGRESS,
   CAN_FINISH, //has completed all stpes
   FINISHED //turned in requests
    
}
