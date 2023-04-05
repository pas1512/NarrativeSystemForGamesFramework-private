using MyFramework.InventorySystem;
using MyFramework.InventorySystem.Interfaces;
using MyFramework.InventorySystem.Types;
using System;
using UnityEngine;

[Serializable]
public class StackableItem : Item
{
    [SerializeField] private int _number;
    public override int Number => _number;

    private StackableType _itemType => (StackableType)Type;

    public StackableItem(): base() { }

    public StackableItem(StackableType type, int number)
        : base(type)
    {
        if (type == null)
            throw new InvalidCastException($"In object {this.GetType()} item type is null");

        _number = number;
    }

    public override bool TryApply(IItem other, out IItem rest)
    {
        if( _itemType == other.Type && _number < _itemType.MaxNumber)
        {
            StackableItem stackableItem = (StackableItem) other;
            int resultNumber = _number + stackableItem._number;
            int overflow = resultNumber - _itemType.MaxNumber;

            if (overflow > 0)
            {
                _number = _itemType.MaxNumber;
                rest = GetIdentical(overflow);
            }
            else
            {
                _number = resultNumber;
                rest = null;
            }
            return true;
        }

        rest = other;
        return false;
    }
    
    public override bool TryAlternativeAction(out IItem result)
    {
        int number = Mathf.FloorToInt(_number / 2);

        if (number > 0)
        {
            _number -= number;
            Debug.Log(_itemType);
            result = GetIdentical(number);
            return true;
        }

        result = null;
        return false;
    }

    public override object Clone()
    {
        if (_itemType == null)
            throw new InvalidCastException($"In object {this.GetType()} item type is null");

        return new StackableItem(_itemType, _number);
    }

    public override float Quality()
    {
        return _number;
    }

    public int TakeFullOrAvailableNumber(int number)
    {
        if (number < 0)
            throw new ArgumentException("Number cant be less then zero");

        if(number < _number)
        {
            _number -= number;
            return number;
        }
        else
        {
            int rest = _number;
            _number = 0;
            return rest;
        }
    }

    protected virtual StackableItem GetIdentical( int number)
    {
        if (_itemType == null)
            throw new InvalidCastException($"In object {GetType()} item type is null");

        return new StackableItem(_itemType, number);
    }
}