using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTableRT
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTableRT = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTableRT(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxes, in string TypeID, in string RoomType, in string Price)
        {
            mysql.connectionStatus();

            request = $"INSERT INTO RoomType values ({TypeID}, '{RoomType}', {Price})";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillRoomType(mysql, listView, comboBoxes);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxes, in string TypeID)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM RoomType where TypeID = {TypeID}";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillRoomType(mysql, listView, comboBoxes);
        }

        public void onlySort(ListView listView, ComboBox[] comboBoxes, in string valueComboBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSort(valueComboBox);

            request = $"SELECT* FROM RoomType ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM RoomType WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void SortAndSearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBoxSearch, in string valueComboBoxSort)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBoxSearch);
            checkValueComboBoxSort(valueComboBoxSort);

            if (valueSearchTableRT != "")
            {
                request = $"SELECT* FROM RoomType WHERE {nameColomSearch} LIKE '{valueSearchTableRT}%' ORDER BY {nameColomSort}";
                requestProcessing(listView, comboBoxes, request);
            }

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void Update(ComboBox[] comboBoxes, in string newTypeID, in string newRoomType, in string newPrice)
        {
            mysql.connectionStatus();

            if (comboBoxes[0].Text != "" && comboBoxes[0].Text != "Старый ID" && newTypeID != "" && newTypeID != "Новый ID")
            {
                request = $"UPDATE RoomType SET TypeID = {newTypeID} WHERE TypeID = {comboBoxes[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }

            if (comboBoxes[1].Text != "" && comboBoxes[1].Text != "ID Типа" && newRoomType != "" && newRoomType != "Новый комнаты")
            {
                request = $"UPDATE RoomType SET RoomType = '{newRoomType}' WHERE TypeID = {comboBoxes[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }

            if (comboBoxes[2].Text != "" && comboBoxes[2].Text != "ID Типа" && newPrice != "" && newPrice != "Новая цена")
            {
                request = $"UPDATE RoomType SET Price = '{newPrice}' WHERE TypeID = {comboBoxes[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }

            mysql.Connection.Close();
        }

        public void onlyAdvancedSearch(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxesAdv)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueTextBoxAdvSearch(textBoxesAdv);

            request = $"SELECT * FROM RoomType where (TypeID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                   $"(Price between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text})";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void AdvancedSearchAndSort(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxesAdv, in string valueComboBoxSort)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueTextBoxAdvSearch(textBoxesAdv);
            checkValueComboBoxSort(valueComboBoxSort);

            request = $"SELECT * FROM RoomType where (TypeID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                   $"(Price between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void AdvancedSearchAndMainSearch(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxesAdv, in string valueComboBoxSearch, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueTextBoxAdvSearch(textBoxesAdv);
            checkValueComboBoxSearch(valueComboBoxSearch);

            request = $"SELECT * FROM RoomType where (TypeID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Price between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and {nameColomSearch} LIKE '{valueTextBox}%'";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void AdvancedSearchAndSortAndSearch(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxesAdv, in string valueComboBoxSort, in string valueComboBoxSearch)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueTextBoxAdvSearch(textBoxesAdv);
            checkValueComboBoxSort(valueComboBoxSort);
            checkValueComboBoxSearch(valueComboBoxSearch);

            request = $"SELECT * FROM RoomType where (TypeID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Price between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and {nameColomSearch} LIKE '{valueSearchTableRT}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "ID Типа")
                nameColomSearch = "TypeID";
            else if (valueComboBoxSearch == "Типы комнат")
                nameColomSearch = "RoomType";
            else if (valueComboBoxSearch == "Цена")
                nameColomSearch = "Price";
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "ID Типа")
                nameColomSort = "TypeID";
            else if (valueComboBoxSort == "Типы комнат")
                nameColomSort = "RoomType";
            else if (valueComboBoxSort == "Цена")
                nameColomSort = "Price";
        }

        private void checkValueTextBoxAdvSearch(TextBox[] textBoxesAdv)
        {
            for (int i = 0; i < textBoxesAdv.Length; i++)
            {
                if (i % 2 == 0 && (textBoxesAdv[i].Text == "" || textBoxesAdv[i].Text == "От"))
                    textBoxesAdv[i].Text = "0";
                else if (i % 2 != 0 && (textBoxesAdv[i].Text == "" || textBoxesAdv[i].Text == "До"))
                    textBoxesAdv[i].Text = $"{int.MaxValue}";
            }
        }

        private void requestProcessing(ListView listView, ComboBox[] comboBoxes, in string request)
        {
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.dataOutputTableRT(table.reader, listView, comboBoxes);
        }
    }
}
