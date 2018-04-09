using System;
using Gico.Common;
using Gico.Config;

namespace Gico.Models.Response
{
    public class KeyValueTypeStringModel
    {
        public string Value { get; set; }
        public string Id => Value;

        public string Text { get; set; }

        public bool Checked { get; set; }

        public static KeyValueTypeStringModel[] FromEnum(Type enumType, bool addDefaultItem = true)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            if (addDefaultItem)
            {
                length++;
            }
            KeyValueTypeStringModel[] keyValueModels = new KeyValueTypeStringModel[length];
            int i = 0;
            if (addDefaultItem)
            {
                keyValueModels[i] = new KeyValueTypeStringModel()
                {
                    Text = "Select One",
                    Value = string.Empty,
                };
                i++;
            }
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeStringModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value).AsString(),
                };
                i++;
            }
            return keyValueModels;
        }

        public static KeyValueTypeStringModel[] FromEnum(Type enumType, string selectedValue, bool addDefaultItem = true)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            if (addDefaultItem)
            {
                length++;
            }
            KeyValueTypeStringModel[] keyValueModels = new KeyValueTypeStringModel[length];
            int i = 0;
            if (addDefaultItem)
            {
                keyValueModels[i] = new KeyValueTypeStringModel()
                {
                    Text = "Select One",
                    Value = string.Empty,
                    Checked = false
                };
                i++;
            }
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeStringModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value).AsString(),
                    Checked = selectedValue == ((int)value).AsString()
                };
                i++;
            }
            return keyValueModels;
        }
    }
    public class KeyValueTypeIntModel
    {
        public int Value { get; set; }
        public int Id => Value;

        public string Text { get; set; }

        public bool Checked { get; set; }

        public static KeyValueTypeIntModel[] FromEnum(Type enumType, bool addDefaultItem = true)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            if (addDefaultItem)
            {
                length++;
            }
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            if (addDefaultItem)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = "Select One",
                    Value = 0,
                };
                i++;
            }
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                };
                i++;
            }
            return keyValueModels;
        }
        public static KeyValueTypeIntModel[] FromEnumSelectMutiple(Type enumType, int allStatus, bool addDefaultItem = true)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            if (addDefaultItem)
            {
                length++;
            }
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            if (addDefaultItem)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = "Select One",
                    Value = 0,
                    Checked = false
                };
                i++;
            }
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = (allStatus & (int)value) == (int)value
                };
                i++;
            }
            return keyValueModels;
        }
        public static KeyValueTypeIntModel[] FromEnum(Type enumType, int selectedValue, bool addDefaultItem = true)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            if (addDefaultItem)
            {
                length++;
            }
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            if (addDefaultItem)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = "Select One",
                    Value = 0,
                    Checked = false
                };
                i++;
            }
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = selectedValue == ((int)value)
                };
                i++;
            }
            return keyValueModels;
        }
    }

    public class JsTreeModel
    {
        public JsTreeModel()
        {
            State = new JstreeStateModel();
        }
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public JstreeStateModel State { get; set; }
    }

    public class JstreeStateModel
    {
        public bool Opened { get; set; }
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
    }
}