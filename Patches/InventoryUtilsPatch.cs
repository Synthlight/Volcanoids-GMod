using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            //foreach (var item in GameResources.Instance.Items) {
            //    GMod.Log(LogLevel.Info, $"\"{item.Category.SortOrder}\": \"{item.Name}\",");
            //}

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
                itemsToSort.Sort((item1, item2) => {
                    var item1Name = item1.itemDefinition.Name;
                    var item2Name = item2.itemDefinition.Name;

                    return item1Name == item2Name ? 0 : GMod.itemSortOrders.TryGet(item1Name).CompareTo(GMod.itemSortOrders.TryGet(item2Name));
                });

                foreach (var item in itemsToSort) {
                    //GMod.Log(LogLevel.Info, $"Adding {item.amount} \"{item.itemDefinition.Name}\".");
                    inventoryView.Inventory.Add(___m_context.Context, item.itemDefinition, item.propertySet, item.amount, 0);
                    item.propertySet.Clear();
                }
            }
        }

        [HarmonyPatch]
        public class InventoryOnlineSlotPanelUiPatch {
            [HarmonyTargetMethod]
            public static MethodBase TargetMethod() {
                var target = typeof(InventoryOnlineSlotPanelUi).GetMethod("DoCompare", BindingFlags.Public | BindingFlags.Instance);
                if (target != null) {
                    return target;
                }

                //typeof(IComparer<>).MakeGenericType(typeof(InventoryItem))
                //new Type[] {typeof(InventoryItem), typeof(InventoryItem)};

                return (from interfaceType in typeof(InventoryOnlineSlotPanelUi).GetInterfaces()
                        where interfaceType.Name.Contains("Compare")
                        select typeof(InventoryOnlineSlotPanelUi).GetInterfaceMap(interfaceType)
                                                                 .InterfaceMethods
                                                                 .First(info => info.Name == "Compare"))
                    .FirstOrDefault();
            }

            [HarmonyPrefix]
            public static bool Prefix(ref int __result, ref InventoryItem __0, ref InventoryItem __1) {
                if (__0.Item == null || __0.Item.Name == null || __1.Item == null || __1.Item.Name == null) return true;

                __result = GMod.itemSortOrders.TryGet(__0.Item.Name).CompareTo(GMod.itemSortOrders.TryGet(__1.Item.Name));
                return false;
            }
        }

        private class ItemTemp {
            public ItemDefinition itemDefinition;
            public readonly PropertySet propertySet = new PropertySet(8);
            public int amount;
        }
    }
}