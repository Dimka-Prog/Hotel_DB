using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTableHS
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTableHS = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTableHS(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxes, TextBox[] textBox)
        {
            mysql.connectionStatus();

            request = $"INSERT INTO HotelStaff values ({textBox[0].Text}, '{textBox[1].Text}', '{textBox[2].Text}', {textBox[3].Text}, '{textBox[4].Text}')";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillHotelStaff(mysql, listView, comboBoxes);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxes, in string StaffID)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM HotelStaff where StaffID = {StaffID}";
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

            request = $"SELECT* FROM HotelStaff ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM HotelStaff WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
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

            if (valueSearchTableHS != "")
            {
                request = $"SELECT* FROM HotelStaff WHERE {nameColomSearch} LIKE '{valueSearchTableHS}%' ORDER BY {nameColomSort}";
                requestProcessing(listView, comboBoxes, request);
            }

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void Update(ListView listView, ComboBox[] comboBoxes, TextBox[] textBoxes)
        {
            mysql.connectionStatus();

            if (comboBoxes[0].Text != "" && comboBoxes[0].Text != "Старый ID" && textBoxes[0].Text != "" && textBoxes[0].Text != "Новый ID")
            {
                request = $"UPDATE HotelStaff SET StaffID = {textBoxes[0].Text} WHERE StaffID = {comboBoxes[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[1].Text != "" && comboBoxes[1].Text != "ID Сотруд." && textBoxes[1].Text != "" && textBoxes[1].Text != "Новое ФИО")
            {
                request = $"UPDATE HotelStaff SET FIO = '{textBoxes[1].Text}' WHERE StaffID = {comboBoxes[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[2].Text != "" && comboBoxes[2].Text != "ID Сотруд." && textBoxes[2].Text != "" && textBoxes[2].Text != "Новая должность")
            {
                request = $"UPDATE HotelStaff SET Post = '{textBoxes[2].Text}' WHERE StaffID = {comboBoxes[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[3].Text != "" && comboBoxes[3].Text != "ID Сотруд." && textBoxes[3].Text != "" && textBoxes[3].Text != "Новая зар. плата")
            {
                request = $"UPDATE HotelStaff SET Salary = {textBoxes[3].Text} WHERE StaffID = {comboBoxes[3].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[4].Text != "" && comboBoxes[4].Text != "ID Сотруд." && textBoxes[4].Text != "" && textBoxes[4].Text != "Новый раб. график")
            {
                request = $"UPDATE HotelStaff SET WorkSchedule = '{textBoxes[4].Text}' WHERE StaffID = {comboBoxes[4].Text}";
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

            request = $"SELECT * FROM HotelStaff where (StaffID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                   $"(Salary between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text})";

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

            request = $"SELECT * FROM HotelStaff where (StaffID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                   $"(Salary between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) ORDER BY {nameColomSort}";

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

            request = $"SELECT * FROM HotelStaff where (StaffID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Salary between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and {nameColomSearch} LIKE '{valueTextBox}%'";

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

            request = $"SELECT * FROM HotelStaff where (StaffID between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(Salary between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and {nameColomSearch} LIKE '{valueSearchTableHS}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "ID Сотруд.")
                nameColomSearch = "StaffID";
            else if (valueComboBoxSearch == "ФИО")
                nameColomSearch = "FIO";
            else if (valueComboBoxSearch == "Должность")
                nameColomSearch = "Post";
            else if (valueComboBoxSearch == "Зар. плата")
                nameColomSearch = "Salary";
            else if (valueComboBoxSearch == "Раб. график")
                nameColomSearch = "WorkSchedule";
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "ID Сотрудника")
                nameColomSort = "StaffID";
            else if (valueComboBoxSort == "ФИО")
                nameColomSort = "FIO";
            else if (valueComboBoxSort == "Должность")
                nameColomSort = "Post";
            else if (valueComboBoxSort == "Заработная плата")
                nameColomSort = "Salary";
            else if (valueComboBoxSort == "Рабочий график")
                nameColomSort = "WorkSchedule";
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

            table.dataOutputTableHS(table.reader, listView, comboBoxes);
        }
    }
}
