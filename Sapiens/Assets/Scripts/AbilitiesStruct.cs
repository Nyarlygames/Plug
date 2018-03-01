using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilitiesStruct {
    public IDictionary<string, int> abilities = new Dictionary<string, int>(); // each stats and level
    public List<IDictionary<string,  IDictionary<string, int>>> abilities_history = new List<IDictionary<string, IDictionary<string, int>>>(); 
    public IDictionary<string, int> affinities = new Dictionary<string, int>(); // affinities level from each activity gain
}
