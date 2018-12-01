using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphene.Inventory
{
    public class Poweups : IInventory
    {
        public Poweups(List<IWearable> wearables)
        {
            _wearables = wearables.ToArray();
        }
        
        private int _limit;
        private IWearable[] _wearables;
        
        public AddError AddItem(IItem item)
        {
            Debug.LogError("Poweruos cant carry itens");
            return AddError.Exception;
        }

        public AddError AddWearable(IWearable wearable)
        {
            if (Contains(wearable))
                return AddError.Duplicated;
            return Add(wearable);
        }

        private AddError Add(IWearable wearable)
        {
            for (int i = 0; i < _wearables.Length; i++)
            {
                if (_wearables[i] == null)
                {
                    _wearables[i] = wearable;
                    return AddError.Success;
                }
            }
            return AddError.NoMoreRoom;
        }

        bool Contains(IWearable wearable)
        {
            return _wearables.Any(w => w == wearable);
        }

        public IWearable GetByType(Type type)
        {
            foreach (var wearable in _wearables)
            {
                if (wearable.GetType() == type)
                    return wearable;
            }

            return null;
        }
    }
}