using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Utility
{
    public static class Helper
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(System.DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                    property.SetValue(item, day, null);
                }
                else
                {
                    if (row[property.Name] == DBNull.Value)
                        property.SetValue(item, null, null);
                    else
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }

        public static string DefaultZero(this string text)
        {
            if (text == "")
            {
                text = "0";
            }
            return text;
        }

        public static string DefaultZero(this object text)
        {
            if (text == null)
            {
                text = "0";
            }
            if (text.ToString() == "")
            {
                text = "0";
            }
            return text.ToString();
        }


        public static double ToDoubleOrZero(this TextBox textBox)
        {
            if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                return 0.0;

            return double.TryParse(textBox.Text, out double value) ? value : 0.0;
        }

        // Convert TextBox.Text to int safely
        public static int ToIntOrZero(this TextBox textBox)
        {
            if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                return 0;

            return int.TryParse(textBox.Text, out int value) ? value : 0;
        }

        // Set default value if empty
        public static void SetDefaultIfEmpty(this TextBox textBox, string defaultValue)
        {
            if (textBox == null) return;
            if (string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text == "0")
                textBox.Text = defaultValue;
        }

        // For your existing DefaultZero() replacement on objects
        public static double DefaultZeroD(this object value)
        {
            if (value == null) return 0.0;
            return double.TryParse(value.ToString(), out double result) ? result : 0.0;
        }

   

    }



}
