using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "CupColors", menuName = "Scriptables/CupColors")]
    public class CupConfig : ScriptableObject
    {
        public List<Color> Colors;
    }
}
