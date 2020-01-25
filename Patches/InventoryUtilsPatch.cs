using System.Collections.Generic;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class InventoryUtilsPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(InventoryUtils).GetMethod(nameof(InventoryUtils.Restack), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        public static void Postfix(ref InventoryUtils __instance, ref InventoryUiContext ___m_context) {
            var tab = __instance.Tab;
            if (tab == null) return;

            for (var i = 0; i < tab.InventoryCount; i++) {
                var inventoryView = tab[i];
                if (!inventoryView.Inventory.ValidateUser(inventoryView.User)) continue;

                var itemsToSort = new List<ItemTemp>();

                for (var j = 0; j < inventoryView.SlotCount; j++) {
                    if (!inventoryView.TryGetSlot(j, out var inventoryItem)) continue;
                    var itemTemp = new ItemTemp();

                    inventoryItem.Stats.CopyTo(itemTemp.propertySet);
                    itemTemp.amount = inventoryView.Inventory.Remove(___m_context.Context, inventoryItem.Item, inventoryItem.Stats, inventoryItem.Amount, j);
                    itemTemp.itemDefinition = inventoryItem.Item;

                    itemsToSort.Add(itemTemp);
                }

                //itemsToSort.Sort((item1, item2) => item1.itemDefinition.Category.SortOrder.CompareTo(item2.itemDefinition.Category.SortOrder));
                itemsToSort.Sort((item1, item2) => GMod.GetItemSortOrder(item1.itemDefinition.Name).CompareTo(GMod.GetItemSortOrder(item2.itemDefinition.Name)));

                foreach (var item in itemsToSort) {
                    GMod.Log(LogLevel.Info, $"Adding {item.amount} \"{item.itemDefinition.Name}\".");
                    inventoryView.Inventory.Add(___m_context.Context, item.itemDefinition, item.propertySet, item.amount, 0);
                    item.propertySet.Clear();
                }
            }
        }

        private class ItemTemp {
            public ItemDefinition itemDefinition;
            public readonly PropertySet propertySet = new PropertySet(8);
            public int amount;
        }
    }
}