using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTableRooms
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTableRooms = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTableRooms(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesAdd, TextBox[] textBox)
        {
            mysql.connectionStatus();

            if (textBox[2].Text == "")
                textBox[2].Text = "null";
            else
                textBox[2].Text = $"'{textBox[2].Text}'";

            if (comboBoxesAdd[1].Text == "")
                comboBoxesAdd[1].Text = "null";

            request = $"INSERT INTO Rooms values ({textBox[0].Text}, {textBox[1].Text}, {textBox[2].Text}, {textBox[3].Text}, {comboBoxesAdd[0].Text}, {comboBoxesAdd[1].Text}, '{textBox[4].Text}')";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillRooms(mysql, listView, comboBoxesUpd, comboBoxesAdd);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesAdd, in string RoomNum)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM Rooms where RoomNum = {RoomNum}";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillRooms(mysql, listView, comboBoxesUpd, comboBoxesAdd);
        }

        public void onlySort(ListView listView, ComboBox[] comboBoxes, in string valueComboBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSort(valueComboBox);

            request = $"SELECT* FROM Rooms ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM Rooms WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
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

            if (valueSearchTableRooms != "")
            {
                request = $"SELECT* FROM Rooms WHERE {nameColomSearch} LIKE '{valueSearchTableRooms}%' ORDER BY {nameColomSort}";
                requestProcessing(listView, comboBoxes, request);
            }

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void Update(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxes)
        {
            mysql.connectionStatus();

            if (comboBoxes[0].Text != "" && comboBoxes[0].Text != "Старый номер" && textBoxes[0].Text != "" && textBoxes[0].Text != "Новый номер")
            {
                request = $"UPDATE Rooms SET RoomNum = {textBoxes[0].Text} WHERE RoomNum = {comboBoxes[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[1].Text != "" && comboBoxes[1].Text != "Номер ком." && textBoxes[1].Text != "" && textBoxes[1].Text != "Новые места")
            {
                request = $"UPDATE Rooms SET Places = {textBoxes[1].Text} WHERE RoomNum = {comboBoxes[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[2].Text != "" && comboBoxes[2].Text != "Номер ком." && textBoxes[2].Text != "Новые особенности")
            {
                if (textBoxes[2].Text == "")
                    textBoxes[2].Text = "null";
                else
                    textBoxes[2].Text = $"'{textBoxes[2].Text}'";

                request = $"UPDATE Rooms SET RoomFeatures = {textBoxes[2].Text} WHERE RoomNum = {comboBoxes[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[3].Text != "" && comboBoxes[3].Text != "Номер ком." && textBoxes[3].Text != "" && textBoxes[3].Text != "Новый этаж")
            {
                request = $"UPDATE Rooms SET Floor = {textBoxes[3].Text} WHERE RoomNum = {comboBoxes[3].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[4].Text != "" && comboBoxes[4].Text != "Номер ком." && comboBoxes[5].Text != "" && comboBoxes[5].Text != "Новый ID")
            {
                request = $"UPDATE Rooms SET TypeID = {comboBoxes[5].Text} WHERE RoomNum = {comboBoxes[4].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[6].Text != "" && comboBoxes[6].Text != "Номер ком." && comboBoxes[7].Text != "Новый ID")
            {
                if (comboBoxes[7].Text == "")
                    comboBoxes[7].Text = "null";

                request = $"UPDATE Rooms SET StaffID = {comboBoxes[7].Text} WHERE RoomNum = {comboBoxes[6].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[8].Text != "" && comboBoxes[8].Text != "Номер ком." && textBoxes[4].Text != "" && textBoxes[4].Text != "Новый статус")
            {
                request = $"UPDATE Rooms SET RoomStatus = '{textBoxes[4].Text}' WHERE RoomNum = {comboBoxes[8].Text}";
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

            request = $"SELECT * FROM Rooms where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Places between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (Floor between {textBoxesAdv[4].Text} and {textBoxesAdv[5].Text}) and " +
                      $"(TypeID between {textBoxesAdv[6].Text} and {textBoxesAdv[7].Text})";

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

            request = $"SELECT * FROM Rooms where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Places between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (Floor between {textBoxesAdv[4].Text} and {textBoxesAdv[5].Text}) and " +
                      $"(TypeID between {textBoxesAdv[6].Text} and {textBoxesAdv[7].Text}) ORDER BY {nameColomSort}";

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

            request = $"SELECT * FROM Rooms where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Places between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (Floor between {textBoxesAdv[4].Text} and {textBoxesAdv[5].Text}) and " +
                      $"(TypeID between {textBoxesAdv[6].Text} and {textBoxesAdv[7].Text}) and {nameColomSearch} LIKE '{valueTextBox}%'";

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

            request = $"SELECT * FROM Rooms where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Places between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (Floor between {textBoxesAdv[4].Text} and {textBoxesAdv[5].Text}) and " +
                      $"(TypeID between {textBoxesAdv[6].Text} and {textBoxesAdv[7].Text}) and {nameColomSearch} LIKE '{valueSearchTableRooms}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "Номер ком.")
                nameColomSearch = "RoomNum";
            else if (valueComboBoxSearch == "Места")
                nameColomSearch = "Places";
            else if (valueComboBoxSearch == "Особ. ном.")
                nameColomSearch = "RoomFeatures";
            else if (valueComboBoxSearch == "Этаж")
                nameColomSearch = "Floor";
            else if (valueComboBoxSearch == "ID Типа")
                nameColomSearch = "TypeID";
            else if (valueComboBoxSearch == "ID Сотруд.")
                nameColomSearch = "StaffID";
            else if (valueComboBoxSearch == "Статус ком.")
                nameColomSearch = "RoomStatus";
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "Номер комнаты")
                nameColomSort = "RoomNum";
            else if (valueComboBoxSort == "Места")
                nameColomSort = "Places";
            else if (valueComboBoxSort == "Особенности номера")
                nameColomSort = "RoomFeatures";
            else if (valueComboBoxSort == "Этаж")
                nameColomSort = "Floor";
            else if (valueComboBoxSort == "ID Типа")
                nameColomSort = "TypeID";
            else if (valueComboBoxSort == "ID Сотрудника")
                nameColomSort = "StaffID";
            else if (valueComboBoxSort == "Статус комнаты")
                nameColomSort = "RoomStatus";
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

            table.dataOutputTableRooms(table.reader, listView, comboBoxes);
        }
    }
}
