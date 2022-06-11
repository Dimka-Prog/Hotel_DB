using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class FillTables
    {
        public MySqlCommand command;
        public MySqlDataReader reader;

        private string queryRoomType = "SELECT* FROM RoomType";
        private string queryHotelStaff = "SELECT* FROM HotelStaff";
        private string queryRooms = "SELECT* FROM Rooms";
        private string queryGuests = "SELECT* FROM Guests";
        private string queryPlacement = "SELECT* FROM Placement";
        private string queryDailyAccounting = "SELECT* FROM DailyAccounting";

        public void fillRoomType(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxes)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryRoomType, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTableRT(reader, listViewTable, comboBoxes);

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillHotelStaff(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxes)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryHotelStaff, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTableHS(reader, listViewTable, comboBoxes);

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillRooms(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesAdd)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryRooms, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTableRooms(reader, listViewTable, comboBoxesUpd);
            
            reader.Close();
            mysql.Connection.Close();

            fillComboBoxesTypeID(mysql, comboBoxesUpd, comboBoxesAdd);
            fillComboBoxesStaffID(mysql, comboBoxesUpd, comboBoxesAdd);
        }

        public void fillComboBoxesTypeID(MySqlDB mysql, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesAdd)
        {
            mysql.connectionStatus();
            comboBoxesAdd[0].Items.Clear();

            command = new MySqlCommand("SELECT TypeID FROM RoomType ORDER BY TypeID", mysql.Connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBoxesUpd[5].Items.Add(reader.GetString(0)); // comboBoxCurrentsRoomNumTypeID
                comboBoxesAdd[0].Items.Add(reader.GetString(0)); // comboBoxAddTypeIDTableRooms
            }

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillComboBoxesStaffID(MySqlDB mysql, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesAdd)
        {
            mysql.connectionStatus();
            comboBoxesAdd[1].Items.Clear();

            command = new MySqlCommand("SELECT StaffID FROM HotelStaff ORDER BY StaffID", mysql.Connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBoxesUpd[7].Items.Add(reader.GetString(0)); // comboBoxCurrentsRoomNumStaffID
                comboBoxesAdd[1].Items.Add(reader.GetString(0)); // comboBoxAddStaffIDTableRooms
            }

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillGuests(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxes)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryGuests, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTableGuests(reader, listViewTable, comboBoxes);

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillPlacement(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryPlacement, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTablePlacement(reader, listViewTable, comboBoxesUpd);

            reader.Close();
            mysql.Connection.Close();

            fillComboBoxesRoomNumTablePlacement(mysql, comboBoxesReferences);
            fillComboBoxesPassportNum(mysql, comboBoxesReferences);
        }

        public void fillComboBoxesRoomNumTablePlacement(MySqlDB mysql, ComboBox[] comboBoxesReferences)
        {
            mysql.connectionStatus();
            comboBoxesReferences[0].Items.Clear();
            comboBoxesReferences[2].Items.Clear();

            command = new MySqlCommand("SELECT RoomNum FROM Rooms ORDER BY RoomNum", mysql.Connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBoxesReferences[0].Items.Add(reader.GetString(0));
                comboBoxesReferences[2].Items.Add(reader.GetString(0));
            }

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillComboBoxesPassportNum(MySqlDB mysql, ComboBox[] comboBoxesReferences)
        {
            mysql.connectionStatus();
            comboBoxesReferences[1].Items.Clear();
            comboBoxesReferences[3].Items.Clear();

            command = new MySqlCommand("SELECT PassportNum FROM Guests ORDER BY PassportNum", mysql.Connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBoxesReferences[1].Items.Add(reader.GetString(0));
                comboBoxesReferences[3].Items.Add(reader.GetString(0));
            }

            reader.Close();
            mysql.Connection.Close();
        }

        public void fillDailyAccounting(MySqlDB mysql, ListView listViewTable, ComboBox[] comboBoxesUpd, ComboBox[] comboBoxesReferences)
        {
            mysql.connectionStatus();
            listViewTable.Clear();

            command = new MySqlCommand(queryDailyAccounting, mysql.Connection);
            reader = command.ExecuteReader();

            dataOutputTableDailyAccounting(reader, listViewTable, comboBoxesUpd);

            reader.Close();
            mysql.Connection.Close();

            fillComboBoxesRoomNumTableDA(mysql, comboBoxesReferences);
        }

        public void fillComboBoxesRoomNumTableDA(MySqlDB mysql, ComboBox[] comboBoxesReferences)
        {
            mysql.connectionStatus();
            comboBoxesReferences[0].Items.Clear();
            comboBoxesReferences[1].Items.Clear();

            command = new MySqlCommand("SELECT RoomNum FROM Rooms ORDER BY RoomNum", mysql.Connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBoxesReferences[0].Items.Add(reader.GetString(0));
                comboBoxesReferences[1].Items.Add(reader.GetString(0));
            }

            reader.Close();
            mysql.Connection.Close();
        }

        public void dataOutputTableRT(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewRoomType(listViewTable);

            string TypeID, RoomType, Price;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                TypeID = reader.GetString(0);
                RoomType = reader.GetString(1);
                Price = reader.GetString(2);

                for (int i = 0; i < comboBoxes.Length; i++)
                    comboBoxes[i].Items.Add(TypeID);

                ListViewItem listViewItem = new ListViewItem(new string[] {"", TypeID, RoomType, Price});
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewRoomType(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 3;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("ID Типа", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Типы комнат", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Цена", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 4; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }

        public void dataOutputTableHS(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewHotelStaff(listViewTable);

            string StaffID, FIO, Post, Salary, WorkSchedule;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                StaffID = reader.GetString(0);
                FIO = reader.GetString(1);
                Post = reader.GetString(2);
                Salary = reader.GetString(3);
                WorkSchedule = reader.GetString(4);

                for (int i = 0; i < comboBoxes.Length; i++)
                    comboBoxes[i].Items.Add(StaffID);

                ListViewItem listViewItem = new ListViewItem(new string[] {"", StaffID, FIO, Post, Salary, WorkSchedule});
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewHotelStaff(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 5;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("ID Сотрудника", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("ФИО", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Должность", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Заработная плата", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Рабочий график", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 6; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }

        public void dataOutputTableRooms(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewRooms(listViewTable);

            string RoomNum, Places, RoomFeatures, Floor, TypeID, StaffID, RoomStatus;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                RoomNum = reader.GetString(0);
                Places = reader.GetString(1);

                if (!reader.IsDBNull(2))
                    RoomFeatures = reader.GetString(2);
                else
                    RoomFeatures = "";

                Floor = reader.GetString(3);
                TypeID = reader.GetString(4);

                if (!reader.IsDBNull(5))
                    StaffID = reader.GetString(5);
                else
                    StaffID = "";

                RoomStatus = reader.GetString(6);

                for (int i = 0; i < comboBoxes.Length; i++)
                {
                    if (i != 5 && i != 7)
                        comboBoxes[i].Items.Add(RoomNum);
                }

                ListViewItem listViewItem = new ListViewItem(new string[] { "", RoomNum, Places, RoomFeatures, Floor, TypeID, StaffID, RoomStatus });
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewRooms(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 5;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("Номер комнаты", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Места", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Особенности номера", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Этаж", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("ID Типа", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("ID Сотрудника", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Статус комнаты", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 8; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }

        public void dataOutputTableGuests(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewGuests(listViewTable);

            string PassportNum, FIO, Citizenship, TypeGuest, Discount;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                PassportNum = reader.GetString(0);
                FIO = reader.GetString(1);
                Citizenship = reader.GetString(2);
                TypeGuest = reader.GetString(3);

                if (!reader.IsDBNull(4))
                    Discount = reader.GetString(4);
                else
                    Discount = "";

                for (int i = 0; i < comboBoxes.Length; i++)
                    comboBoxes[i].Items.Add(PassportNum);

                ListViewItem listViewItem = new ListViewItem(new string[] { "", PassportNum, FIO, Citizenship, TypeGuest, Discount });
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewGuests(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 5;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("Номер паспорта", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("ФИО", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Гражданство", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Тип гостя", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Скидка", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 6; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }

        public void dataOutputTablePlacement(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewPlacement(listViewTable);

            string RoomNum, PassportNum, SetDate, DepartureDate;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                RoomNum = reader.GetString(0);
                PassportNum = reader.GetString(1);
                SetDate = reader.GetString(2);

                if (!reader.IsDBNull(3))
                    DepartureDate = reader.GetString(3);
                else
                    DepartureDate = "";

                for (int i = 0; i < comboBoxes.Length; i++)
                {
                    if (i != 0)
                        comboBoxes[i].Items.Add(PassportNum);
                    else comboBoxes[i].Items.Add(RoomNum);
                }

                ListViewItem listViewItem = new ListViewItem(new string[] { "", RoomNum, PassportNum, SetDate, DepartureDate });
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewPlacement(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 4;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("Номер комнаты", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Номер паспорта", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Дата заезда", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Дата выезда", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 5; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }

        public void dataOutputTableDailyAccounting(MySqlDataReader reader, ListView listViewTable, ComboBox[] comboBoxes)
        {
            settinglistViewDailyAccounting(listViewTable);

            string RoomNum, ServiceDate, ConditionRoom, Complaints, ServicesRendered;

            for (int i = 0; i < comboBoxes.Length; i++)
                comboBoxes[i].Items.Clear();

            while (reader.Read())
            {
                RoomNum = reader.GetString(0);
                ServiceDate = reader.GetString(1);
                ConditionRoom = reader.GetString(2);
                Complaints = reader.GetString(3);
                ServicesRendered = reader.GetString(4);

                for (int i = 0; i < comboBoxes.Length; i++)
                    comboBoxes[i].Items.Add(RoomNum);

                ListViewItem listViewItem = new ListViewItem(new string[] { "", RoomNum, ServiceDate, ConditionRoom, Complaints, ServicesRendered });
                listViewTable.Items.Add(listViewItem);
                listViewItem.Font = new Font("Sylfaen", 13, FontStyle.Regular);
            }
        }

        public void settinglistViewDailyAccounting(ListView listViewTable)
        {
            int sizeWidthColoms = listViewTable.Size.Width / 5;

            listViewTable.Columns.Add("", 0, textAlign: HorizontalAlignment.Left);
            listViewTable.Columns.Add("Номер комнаты", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Дата обслуживания", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Статус комнаты", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Жалобы", sizeWidthColoms, textAlign: HorizontalAlignment.Center);
            listViewTable.Columns.Add("Оказанные услуги", sizeWidthColoms, textAlign: HorizontalAlignment.Center);

            listViewTable.Font = new Font(listViewTable.Font, FontStyle.Bold);

            for (int cols = 1; cols < 6; cols++)
                listViewTable.Columns[cols].Width = sizeWidthColoms;
        }
    }
}
