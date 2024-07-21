using Inventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SaveSystem
{
    [Serializable]
    public class InventoryData
    {
        public List<InventoryItem> items = new List<InventoryItem>();

    }
}
