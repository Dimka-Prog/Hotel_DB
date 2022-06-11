using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTablePlacement
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTablePlacement = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTablePlacement(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, TextBox[] textBoxAdd)
        {
            mysql.connectionStatus();

            if (textBoxAdd[1].Text == "")
                textBoxAdd[1].Text = "null";
            else
                textBoxAdd[1].Text = $"'{textBoxAdd[1].Text}'";

            request = $"INSERT INTO Placement values ({comboBoxesReferences[0].Text}, {comboBoxesReferences[1].Text}, '{textBoxAdd[0].Text}', {textBoxAdd[1].Text})";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillPlacement(mysql, listView, comboBoxesUpd, comboBoxesReferences);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, TextBox[] textBoxesDel)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM Placement where RoomNum = {textBoxesDel[0].Text} and PassportNum = {textBoxesDel[1].Text}";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillPlacement(mysql, listView, comboBoxesUpd, comboBoxesReferences);
        }

        public void onlySort(ListView listView, ComboBox[] comboBoxes, in string valueComboBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSort(valueComboBox);

            request = $"SELECT* FROM Placement ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM Placement WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
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

            if (valueSearchTablePlacement != "")
            {
                request = $"SELECT* FROM Placement WHERE {nameColomSearch} LIKE '{valueSearchTablePlacement}%' ORDER BY {nameColomSort}";
                requestProcessing(listView, comboBoxes, request);
            }

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void Update(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, TextBox[] textBoxesUpd)
        {
            mysql.connectionStatus();

            if (comboBoxesUpd[0].Text != "" && comboBoxesUpd[0].Text != "Старый номер" && comboBoxesReferences[2].Text != "" && comboBoxesReferences[2].Text != "Новый номер")
            {
                request = $"UPDATE Placement SET RoomNum = {comboBoxesReferences[2].Text} WHERE RoomNum = {comboBoxesUpd[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[1].Text != "" && comboBoxesUpd[1].Text != "Старый номер" && comboBoxesReferences[3].Text != "" && comboBoxesReferences[3].Text != "Новый номер")
            {
                request = $"UPDATE Placement SET PassportNum = {comboBoxesReferences[3].Text} WHERE PassportNum = {comboBoxesUpd[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[2].Text != "" && comboBoxesUpd[2].Text != "Номер паспорта" && textBoxesUpd[0].Text != "" && textBoxesUpd[0].Text != "Новая дата заезда")
            {
                request = $"UPDATE Placement SET SetDate = '{textBoxesUpd[0].Text}' WHERE PassportNum = {comboBoxesUpd[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[3].Text != "" && comboBoxesUpd[3].Text != "Номер паспорта" && textBoxesUpd[1].Text != "Новая дата выезда")
            {
                if (textBoxesUpd[1].Text == "")
                    textBoxesUpd[1].Text = "null";
                else
                    textBoxesUpd[1].Text = $"'{textBoxesUpd[1].Text}'";

                request = $"UPDATE Placement SET DepartureDate = {textBoxesUpd[1].Text} WHERE PassportNum = {comboBoxesUpd[3].Text}";
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

            request = $"SELECT * FROM Placement where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(PassportNum between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (SetDate between '{textBoxesAdv[4].Text}' and '{textBoxesAdv[5].Text}')"; 

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

            request = $"SELECT * FROM Placement where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(PassportNum between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (SetDate between '{textBoxesAdv[4].Text}' and '{textBoxesAdv[5].Text}') ORDER BY {nameColomSort}";

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

            request = $"SELECT * FROM Placement where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(PassportNum between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (SetDate between '{textBoxesAdv[4].Text}' and '{textBoxesAdv[5].Text}') and {nameColomSearch} LIKE '{valueTextBox}%'";

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

            request = $"SELECT * FROM Placement where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(PassportNum between {textBoxesAdv[2].Text} and {textBoxesAdv[3].Text}) and (SetDate between '{textBoxesAdv[4].Text}' and '{textBoxesAdv[5].Text}') and {nameColomSearch} LIKE '{valueSearchTablePlacement}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "Номер комнаты")
                nameColomSort = "RoomNum";
            else if (valueComboBoxSort == "Номер паспорта")
                nameColomSort = "PassportNum";
            else if (valueComboBoxSort == "Дата заезда")
                nameColomSort = "SetDate";
            else if (valueComboBoxSort == "Дата выезда")
                nameColomSort = "DepartureDate";
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "Номер ком.")
                nameColomSearch = "RoomNum";
            else if (valueComboBoxSearch == "Номер пасп.")
                nameColomSearch = "PassportNum";
            else if (valueComboBoxSearch == "Дата заезда")
                nameColomSearch = "SetDate";
            else if (valueComboBoxSearch == "Дата выезда")
                nameColomSearch = "DepartureDate";
        }

        private void checkValueTextBoxAdvSearch(TextBox[] textBoxesAdv)
        {
            for (int i = 0; i < textBoxesAdv.Length - 2; i++)
            {
                if (i % 2 == 0 && (textBoxesAdv[i].Text == "" || textBoxesAdv[i].Text == "От"))
                    textBoxesAdv[i].Text = "0";
                else if (i % 2 != 0 && (textBoxesAdv[i].Text == "" || textBoxesAdv[i].Text == "До"))
                    textBoxesAdv[i].Text = $"{int.MaxValue}";
            }

            if (textBoxesAdv[4].Text == "" || textBoxesAdv[4].Text == "От")
                textBoxesAdv[4].Text = "2022-01-01";

            if (textBoxesAdv[5].Text == "" || textBoxesAdv[5].Text == "До")
                textBoxesAdv[5].Text = "2122-01-01";
        }

        private void requestProcessing(ListView listView, ComboBox[] comboBoxes, in string request)
        {
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.dataOutputTablePlacement(table.reader, listView, comboBoxes);
        }
    }
}
