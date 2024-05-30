using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Event
{
    public class CharacterEvent
    {
        public static UnityAction<GameObject,int> characterTookDamage;
        public static UnityAction<GameObject,int> characterHealed;
    }
}
