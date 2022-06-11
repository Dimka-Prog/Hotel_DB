using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTableGuests
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTableGuests = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTableGuests(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxes, TextBox[] textBox)
        {
            mysql.connectionStatus();

            if (textBox[4].Text == "")
                textBox[4].Text = "null";

            request = $"INSERT INTO Guests values ({textBox[0].Text}, '{textBox[1].Text}', '{textBox[2].Text}', '{textBox[3].Text}', {textBox[4].Text})";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillGuests(mysql, listView, comboBoxes);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxes, in string PassportNum)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM Guests where PassportNum = {PassportNum}";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillGuests(mysql, listView, comboBoxes);
        }

        public void onlySort(ListView listView, ComboBox[] comboBoxes, in string valueComboBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSort(valueComboBox);

            request = $"SELECT* FROM Guests ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM Guests WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
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

            if (valueSearchTableGuests != "")
            {
                request = $"SELECT* FROM Guests WHERE {nameColomSearch} LIKE '{valueSearchTableGuests}%' ORDER BY {nameColomSort}";
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
                request = $"UPDATE Guests SET PassportNum = {textBoxes[0].Text} WHERE PassportNum = {comboBoxes[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[1].Text != "" && comboBoxes[1].Text != "Номер пасп." && textBoxes[1].Text != "" && textBoxes[1].Text != "Новое ФИО")
            {
                request = $"UPDATE Guests SET FIO = '{textBoxes[1].Text}' WHERE PassportNum = {comboBoxes[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[2].Text != "" && comboBoxes[2].Text != "Номер пасп." && textBoxes[2].Text != "" && textBoxes[2].Text != "Новое гражданство")
            {
                request = $"UPDATE Guests SET Citizenship = '{textBoxes[2].Text}' WHERE PassportNum = {comboBoxes[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[3].Text != "" && comboBoxes[3].Text != "Номер паспорта" && textBoxes[3].Text != "" && textBoxes[3].Text != "Новый тип гостя")
            {
                request = $"UPDATE Guests SET TypeGuest = '{textBoxes[3].Text}' WHERE PassportNum = {comboBoxes[3].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxes[4].Text != "" && comboBoxes[4].Text != "Номер пасп." && textBoxes[4].Text != "Новая скидка")
            {
                if (textBoxes[4].Text == "")
                    textBoxes[4].Text = "null";

                request = $"UPDATE Guests SET Discount = {textBoxes[4].Text} WHERE PassportNum = {comboBoxes[4].Text}";
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

            request = $"SELECT * FROM Guests where PassportNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}";

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

            request = $"SELECT * FROM Guests where PassportNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text} ORDER BY {nameColomSort}";

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

            request = $"SELECT * FROM Guests where PassportNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text} and {nameColomSearch} LIKE '{valueTextBox}%'";

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

            request = $"SELECT * FROM Guests where PassportNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text} and {nameColomSearch} LIKE '{valueSearchTableGuests}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "Номер паспорта")
                nameColomSort = "PassportNum";
            else if (valueComboBoxSort == "ФИО")
                nameColomSort = "FIO";
            else if (valueComboBoxSort == "Гражданство")
                nameColomSort = "Citizenship";
            else if (valueComboBoxSort == "Тип гостя")
                nameColomSort = "TypeGuest";
            else if (valueComboBoxSort == "Скидка")
                nameColomSort = "Discount";
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "Номер пасп.")
                nameColomSearch = "PassportNum";
            else if (valueComboBoxSearch == "ФИО")
                nameColomSearch = "FIO";
            else if (valueComboBoxSearch == "Граждан-во")
                nameColomSearch = "Citizenship";
            else if (valueComboBoxSearch == "Тип гостя")
                nameColomSearch = "TypeGuest";
            else if (valueComboBoxSearch == "Скидка")
                nameColomSearch = "Discount";
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

            table.dataOutputTableGuests(table.reader, listView, comboBoxes);
        }
    }
}
