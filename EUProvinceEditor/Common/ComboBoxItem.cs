using System;

namespace EUProvinceEditor.Common
{
    [Serializable]
    public class ComboBoxItem<T>
    {
        protected T m_value;
        protected string m_label;

        public ComboBoxItem()
        {
        }

        public ComboBoxItem(T value, string label)
        {
            m_value = value;
            m_label = label;
        }

        public T Value
        {
            get => m_value;
            set => m_value = value;
        }
        public string Label
        {
            get => m_label;
            set => m_label = value;
        }
    }
}
