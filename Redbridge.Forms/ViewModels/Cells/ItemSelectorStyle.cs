namespace Redbridge.Forms
{
    /// <summary>
    /// The way in which an item can be selected, e.g. an action sheet pop up for a small number of items, 
    /// or a picker for say a grouped list.
    /// </summary>
    public enum ItemSelectorStyle
    {
        Auto = 0, // If there are more than x items (based on device I guess) then a picker view will be used.
        ActionSheet = 1,
        PickerView = 2,
    }
}
