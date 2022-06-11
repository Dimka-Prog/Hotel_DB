using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class RequestsTableDA
    {
        private MySqlDB mysql;
        private FillTables table;

        private string request;
        public string valueSearchTableDA = "";
        private string nameColomSearch;
        private string nameColomSort;

        public RequestsTableDA(MySqlDB mysql, FillTables table)
        {
            this.mysql = mysql;
            this.table = table;
        }

        public void Add(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, TextBox[] textBoxAdd)
        {
            mysql.connectionStatus();

            request = $"INSERT INTO DailyAccounting values ({comboBoxesReferences[0].Text}, '{textBoxAdd[0].Text}', '{textBoxAdd[1].Text}', '{textBoxAdd[2].Text}', '{textBoxAdd[3].Text}')";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillDailyAccounting(mysql, listView, comboBoxesUpd, comboBoxesReferences);
        }

        public void Remove(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, in string RoomNum)
        {
            mysql.connectionStatus();

            request = $"DELETE FROM DailyAccounting where RoomNum = {RoomNum}";
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.reader.Close();
            mysql.Connection.Close();

            table.fillDailyAccounting(mysql, listView, comboBoxesUpd, comboBoxesReferences);
        }

        public void onlySort(ListView listView, ComboBox[] comboBoxes, in string valueComboBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSort(valueComboBox);

            request = $"SELECT* FROM DailyAccounting ORDER BY {nameColomSort}";
            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void onlySearch(ListView listView, ComboBox[] comboBoxes, in string valueComboBox, in string valueTextBox)
        {
            mysql.connectionStatus();
            listView.Clear();

            checkValueComboBoxSearch(valueComboBox);

            request = $"SELECT* FROM DailyAccounting WHERE {nameColomSearch} LIKE '{valueTextBox}%'";
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

            if (valueSearchTableDA != "")
            {
                request = $"SELECT* FROM DailyAccounting WHERE {nameColomSearch} LIKE '{valueSearchTableDA}%' ORDER BY {nameColomSort}";
                requestProcessing(listView, comboBoxes, request);
            }

            table.reader.Close();
            mysql.Connection.Close();
        }

        public void Update(ListView listView, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences, TextBox[] textBoxesUpd)
        {
            mysql.connectionStatus();

            if (comboBoxesUpd[0].Text != "" && comboBoxesUpd[0].Text != "Старый номер" && comboBoxesReferences[1].Text != "" && comboBoxesReferences[1].Text != "Новый номер")
            {
                request = $"UPDATE DailyAccounting SET RoomNum = {comboBoxesReferences[1].Text} WHERE RoomNum = {comboBoxesUpd[0].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[1].Text != "" && comboBoxesUpd[1].Text != "Номер комнаты" && textBoxesUpd[0].Text != "" && textBoxesUpd[0].Text != "Новая дата обслуживания")
            {
                request = $"UPDATE DailyAccounting SET ServiceDate = '{textBoxesUpd[0].Text}' WHERE RoomNum = {comboBoxesUpd[1].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[2].Text != "" && comboBoxesUpd[2].Text != "Номер комнтаы" && textBoxesUpd[1].Text != "" && textBoxesUpd[1].Text != "Новый статус")
            {
                request = $"UPDATE DailyAccounting SET ConditionRoom = '{textBoxesUpd[1].Text}' WHERE RoomNum = {comboBoxesUpd[2].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[3].Text != "" && comboBoxesUpd[3].Text != "Номер комнаты" && textBoxesUpd[2].Text != "" && textBoxesUpd[2].Text != "Новая жалоба")
            {
                request = $"UPDATE DailyAccounting SET Complaints = '{textBoxesUpd[2].Text}' WHERE RoomNum = {comboBoxesUpd[3].Text}";
                table.command = new MySqlCommand(request, mysql.Connection);
                table.reader = table.command.ExecuteReader();
            }
            else if (comboBoxesUpd[4].Text != "" && comboBoxesUpd[4].Text != "Номер комнаты" && textBoxesUpd[3].Text != "" && textBoxesUpd[3].Text != "Новые оказанные услуги")
            {
                request = $"UPDATE DailyAccounting SET ServicesRendered = '{textBoxesUpd[3].Text}' WHERE RoomNum = {comboBoxesUpd[4].Text}";
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

            request = $"SELECT * FROM DailyAccounting where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                          $"(ServiceDate between '{textBoxesAdv[2].Text}' and '{textBoxesAdv[3].Text}')";

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

            request = $"SELECT * FROM DailyAccounting where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                                                          $"(ServiceDate between '{textBoxesAdv[2].Text}' and '{textBoxesAdv[3].Text}') ORDER BY {nameColomSort}";

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

            request = $"SELECT * FROM DailyAccounting where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(ServiceDate between '{textBoxesAdv[2].Text}' and '{textBoxesAdv[3].Text}') and {nameColomSearch} LIKE '{valueTextBox}%'";

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

            request = $"SELECT * FROM DailyAccounting where (RoomNum between {textBoxesAdv[0].Text} and {textBoxesAdv[1].Text}) and " +
                      $"(ServiceDate between '{textBoxesAdv[2].Text}' and '{textBoxesAdv[3].Text}') and {nameColomSearch} LIKE '{valueSearchTableDA}%' ORDER BY {nameColomSort}";

            requestProcessing(listView, comboBoxes, request);

            table.reader.Close();
            mysql.Connection.Close();
        }

        private void checkValueComboBoxSort(in string valueComboBoxSort)
        {
            if (valueComboBoxSort == "Номер комнаты")
                nameColomSort = "RoomNum";
            else if (valueComboBoxSort == "Дата обслуживания")
                nameColomSort = "ServiceDate";
            else if (valueComboBoxSort == "Статус комнаты")
                nameColomSort = "ConditionRoom";
        }

        private void checkValueComboBoxSearch(in string valueComboBoxSearch)
        {
            if (valueComboBoxSearch == "Номер ком.")
                nameColomSearch = "RoomNum";
            else if (valueComboBoxSearch == "Дата обслуж.")
                nameColomSearch = "ServiceDate";
            else if (valueComboBoxSearch == "Статус ком.")
                nameColomSearch = "ConditionRoom";
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

            if (textBoxesAdv[2].Text == "" || textBoxesAdv[2].Text == "От")
                textBoxesAdv[2].Text = "2022-01-01";

            if (textBoxesAdv[3].Text == "" || textBoxesAdv[3].Text == "До")
                textBoxesAdv[3].Text = "2122-01-01";
        }

        private void requestProcessing(ListView listView, ComboBox[] comboBoxes, in string request)
        {
            table.command = new MySqlCommand(request, mysql.Connection);
            table.reader = table.command.ExecuteReader();

            table.dataOutputTableDailyAccounting(table.reader, listView, comboBoxes);
        }
    }
}
