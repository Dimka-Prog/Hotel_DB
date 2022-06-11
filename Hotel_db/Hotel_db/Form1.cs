using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class Form1 : Form
    {
        private MySqlDB mysql = new MySqlDB();
        private FillTables table = new FillTables();

        private ComboBox[] comboBoxTableRT;

        private ComboBox[] comboBoxTableHS;

        private ComboBox[] comboBoxUpdTableRooms;
        private ComboBox[] comboBoxAddTableRooms;

        private ComboBox[] comboBoxTableGuests;

        private ComboBox[] comboBoxReferencesDataTablePlacement;
        private ComboBox[] comboBoxUpdTablePlacement;

        private ComboBox[] comboBoxUpdTableDA;
        private ComboBox[] comboBoxReferencesDataTableDA;

        private TextBox[] textBoxAdvTableRT;

        private TextBox[] textBoxAddTableHS;
        private TextBox[] textBoxUpdTableHS;
        private TextBox[] textBoxAdvTableHS;

        private TextBox[] textBoxAddTableRooms;
        private TextBox[] textBoxUpdTableRooms;
        private TextBox[] textBoxAdvTableRooms;

        private TextBox[] textBoxAddTableGuests;
        private TextBox[] textBoxUpdTableGuests;
        private TextBox[] textBoxAdvTableGuests;

        private TextBox[] textBoxAddTablePlacement;
        private TextBox[] textBoxDelTablePlacement;
        private TextBox[] textBoxUpdTablePlacement;
        private TextBox[] textBoxAdvTablePlacement;

        private TextBox[] textBoxAddTableDA;
        private TextBox[] textBoxUpdTableDA;
        private TextBox[] textBoxAdvTableDA;

        private RequestsTableRT requestsTableRT;
        private RequestsTableHS requestsTableHS;
        private RequestsTableRooms requestsTableRooms;
        private RequestsTableGuests requestsTableGuests;
        private RequestsTablePlacement requestsTablePlacement;
        private RequestsTableDA requestsTableDA;

        public int sizeWidthColoms;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mysql.server = "localhost";
            mysql.databaseName = "Hotel";
            mysql.userName = "root";
            mysql.password = "02082001DiMa";

            if (!mysql.isConnect())
            {
                MessageBox.Show("Connection error!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            requestsTableRT = new RequestsTableRT(mysql, table);
            requestsTableHS = new RequestsTableHS(mysql, table);
            requestsTableRooms = new RequestsTableRooms(mysql, table);
            requestsTableGuests = new RequestsTableGuests(mysql, table);
            requestsTablePlacement = new RequestsTablePlacement(mysql, table);
            requestsTableDA = new RequestsTableDA(mysql, table);

            comboBoxTableRT = new ComboBox[3] { comboBoxOldIDTableRT, comboBoxCurrentsIDTypeRoom, comboBoxCurrentsIDPrice };

            comboBoxTableHS = new ComboBox[5] { comboBoxOldStaffIDTableHS, comboBoxCurrentsIDFIOTableHS, comboBoxCurrentsIDPost, comboBoxCurrentsIDSalary, comboBoxCurrentsIDWorkSchedule };

            comboBoxAddTableRooms = new ComboBox[2] { comboBoxAddTypeIDTableRooms, comboBoxAddStaffIDTableRooms };
            comboBoxUpdTableRooms = new ComboBox[9] { comboBoxOldRoomNumTableRooms, comboBoxCurrentsRoomNumPlaces, comboBoxCurrentsRoomNumRoomFeatures, comboBoxCurrentsRoomNumFloor, 
                                                   comboBoxCurrentsRoomNumTypeID, comboBoxNewTypeIDTableRooms, comboBoxCurrentsRoomNumStaffID, comboBoxNewStaffIDTableRooms, comboBoxCurrentsRoomNumRoomStatus};

            comboBoxTableGuests = new ComboBox[5] { comboBoxOldPassportNumTableGuests, comboBoxCurrentsPassportNumFIO, comboBoxCurrentsPassportNumCitizenship, comboBoxCurrentsPassportNumTypeGuests, comboBoxCurrentsPassportNumDiscount };

            comboBoxReferencesDataTablePlacement = new ComboBox[4] { comboBoxAddRoomNumTablePlacement, comboBoxAddPassportNumTablePlacement, comboBoxNewRoomNumTablePlacement, comboBoxNewPassportNumTablePlacement };
            comboBoxUpdTablePlacement = new ComboBox[4] { comboBoxOldRoomNumTablePlacement, comboBoxOldPassportNumTablePlacement, comboBoxCurrentsPassportNumSetDate, comboBoxCurrentsPassportNumDepartureDate };

            comboBoxUpdTableDA = new ComboBox[5] { comboBoxOldRoomNumTableDailyAccounting, comboBoxCurrentsRoomNumServiceDate, comboBoxCurrentsRoomNumConditionRoom, comboBoxCurrentsRoomNumComplaints, comboBoxCurrentsRoomNumServicesRendered };
            comboBoxReferencesDataTableDA = new ComboBox[2] { comboBoxAddRoomNumTableDA, comboBoxNewRoomNumTableDailyAccounting };

            textBoxAdvTableRT = new TextBox[4] { textBoxMinTypeIDTableRT, textBoxMaxTypeIDTableRT, textBoxMinPrice, textBoxMaxPrice };

            textBoxAddTableHS = new TextBox[5] { textBoxAddStaffIDTableHS, textBoxAddFIOTableHS, textBoxAddPost, textBoxAddSalary, textBoxAddWorkSchedule };
            textBoxUpdTableHS = new TextBox[5] { textBoxNewStaffIDTableHS, textBoxNewFIOTableHS, textBoxNewPost, textBoxNewSalary, textBoxNewWorkSchedule };
            textBoxAdvTableHS = new TextBox[4] { textBoxMinStaffIDTableHS, textBoxMaxStaffIDTableHS, textBoxMinSalary, textBoxMaxSalary };

            textBoxAddTableRooms = new TextBox[5] { textBoxAddRoomNumTableRooms, textBoxAddPlaces, textBoxAddRoomFeatures, textBoxAddFlor, textBoxAddRoomStatus };
            textBoxUpdTableRooms = new TextBox[5] { textBoxNewRoomNumTableRooms, textBoxNewPlaces, textBoxNewRoomFeatures, textBoxNewFloor, textBoxNewRoomStatus };
            textBoxAdvTableRooms = new TextBox[8] { textBoxMinRoomNumTableRooms, textBoxMaxRoomNumTableRooms, textBoxMinPlaces, textBoxMaxPlaces, textBoxMinFloor, 
                                                     textBoxMaxFloor, textBoxMinTypeIDTableRooms, textBoxMaxTypeIDTableRooms};

            textBoxAddTableGuests = new TextBox[5] { textBoxAddPassportNumTableGuests, textBoxAddFIOTableGuests, textBoxAddCitizenship, textBoxAddTypeGuest, textBoxAddDiscount };
            textBoxUpdTableGuests = new TextBox[5] { textBoxNewPassportNumTableGuests, textBoxNewFIOTableGuests, textBoxNewCitizenship, textBoxNewTypeGuests, textBoxNewDiscount };
            textBoxAdvTableGuests = new TextBox[2] { textBoxMinPassportNumTableGuests, textBoxMaxPassportNumTableGuests };

            textBoxAddTablePlacement = new TextBox[2] { textBoxAddSetDate, textBoxAddDepartureDate };
            textBoxDelTablePlacement = new TextBox[2] { textBoxDelRoomNumTablePlacement, textBoxDelPassportNumTablePlacement };
            textBoxUpdTablePlacement = new TextBox[2] { textBoxNewSetDate, textBoxNewDepartureDate };
            textBoxAdvTablePlacement = new TextBox[6] { textBoxMinRoomNumTablePlacement, textBoxMaxRoomNumTablePlacement, textBoxMinPassportNumTablePlacement,
                                                        textBoxMaxPassportNumTablePlacement, textBoxMinSetDate, textBoxMaxSetDate};

            textBoxAddTableDA = new TextBox[4] { textBoxAddServiceDate, textBoxAddConditionRoom, textBoxAddComplaints, textBoxAddServicesRendered };
            textBoxUpdTableDA = new TextBox[4] { textBoxNewServiceDate, textBoxNewConditionRoom, textBoxNewComplaints, textBoxNewServicesRendered };
            textBoxAdvTableDA = new TextBox[4] { textBoxMinRoomNumTableDA, textBoxMaxRoomNumTableDA, textBoxMinServiceDate, textBoxMaxServiceDate };

            table.fillRoomType(mysql, listViewTableRT, comboBoxTableRT);
        }

        private void tabControlMainsTables_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == pageTableRoomType)
            {
                table.fillRoomType(mysql, listViewTableRT, comboBoxTableRT);

                clearPageAddTableRT();
                clearUpdTabControlTableRT();
                clearPageAdvSearchTableRT();
            }
            else if (e.TabPage == pageTableHotelStaff)
            {
                table.fillHotelStaff(mysql, listViewTableHS, comboBoxTableHS);

                clearPageAddTableHS();
                clearUpdTabControlTableHS();
                clearPageAdvSearchTableHS();
            }
            else if (e.TabPage == pageTableRooms)
            {
                table.fillRooms(mysql, listViewTableRooms, comboBoxUpdTableRooms, comboBoxAddTableRooms);

                clearPageAddTableRooms();
                clearUpdTabControlTableRooms();
                clearPageAdvSearchTableRooms();
            }
            else if (e.TabPage == pageTableGuests)
            {
                table.fillGuests(mysql, listViewTableGuests, comboBoxTableGuests);

                clearPageAddTableGuests();
                clearUpdTabControlTableGuests();
                clearPageAdvSearchTableGuests();
            }
            else if (e.TabPage == pageTablePlacement)
            {
                table.fillPlacement(mysql, listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement);

                clearPageAddTablePlacement();
                clearUpdTabControlTablePlacement();
                clearPageAdvSearchTablePlacement();
            }
            else if (e.TabPage == pageTableDailyAccounting)
            {
                table.fillDailyAccounting(mysql, listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA);

                clearPageAddTableDA();
                clearUpdTabControlTableDA();
                clearPageAdvSearchTableDA();
            }
        }

        private void Form1_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            mysql.Connection.Close();
        }

        private void listViewRoomType_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewRoomType_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTableRT = new bool[6];
        private void textBoxNewIDRoomType_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[0])
            {
                textBoxNewIDTableRT.Clear();
                checkClickElemUpdTableRT[0] = true;
            }
        }

        private void comboBoxOldIDRoomType_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[1])
            {
                comboBoxOldIDTableRT.Text = "";
                checkClickElemUpdTableRT[1] = true;
            }
        }

        private void textBoxNewRoomType_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[2])
            {
                textBoxNewRoomType.Clear();
                checkClickElemUpdTableRT[2] = true;
            }
        }

        private void comboBoxCurrentsIDRoomType_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[3])
            {
                comboBoxCurrentsIDTypeRoom.Text = "";
                checkClickElemUpdTableRT[3] = true;
            }
        }

        private void textBoxNewPrice_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[4])
            {
                textBoxNewPrice.Clear();
                checkClickElemUpdTableRT[4] = true;
            }
        }

        private void comboBoxCurrentsIDPrice_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRT[5])
            {
                comboBoxCurrentsIDPrice.Text = "";
                checkClickElemUpdTableRT[5] = true;
            }
        }

        bool[] checkClickElemAdvTableRT = new bool[4];
        private void textBoxMinValueTypeIDTableRT_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRT[0])
            {
                textBoxMinTypeIDTableRT.Clear();
                checkClickElemAdvTableRT[0] = true;
            }

        }

        private void textBoxMaxValueTypeIDTableRT_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRT[1])
            {
                textBoxMaxTypeIDTableRT.Clear();
                checkClickElemAdvTableRT[1] = true;
            }
        }

        private void textBoxMinValuePrice_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRT[2])
            {
                textBoxMinPrice.Clear();
                checkClickElemAdvTableRT[2] = true;
            }
        }

        private void textBoxMaxValuePrice_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRT[3])
            {
                textBoxMaxPrice.Clear();
                checkClickElemAdvTableRT[3] = true;
            }
        }


        private void tabControlSettingTableRT_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTableRT();
            textBoxDelTableRT.Clear();
            clearUpdTabControlTableRT();
            clearPageAdvSearchTableRT();
        }

        private void tabControlUpdTableRT_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTableRT();
        }

        public void clearPageAddTableRT()
        {
            textBoxAddTypeIDTableRT.Clear();
            textBoxAddTypeRoom.Clear();
            textBoxAddPrice.Clear();
        }
        public void clearUpdTabControlTableRT()
        {
            comboBoxOldIDTableRT.Text = "Старый ID";
            textBoxNewIDTableRT.Text = "Новый ID";

            comboBoxCurrentsIDTypeRoom.Text = "ID Типа";
            textBoxNewRoomType.Text = "Новый тип комнаты";

            comboBoxCurrentsIDPrice.Text = "ID Типа";
            textBoxNewPrice.Text = "Новая цена";

            for (int i = 0; i < checkClickElemUpdTableRT.Length; i++)
                checkClickElemUpdTableRT[i] = false;
        }

        public void clearPageAdvSearchTableRT()
        {
            for (int i = 0; i < textBoxAdvTableRT.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTableRT[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTableRT[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTableRT.Length; i++)
                checkClickElemAdvTableRT[i] = false;
        }

        private bool checkValueTextBoxAdvSearchTableRT()
        {
            for (int i = 0; i < textBoxAdvTableRT.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTableRT[i].Text != "" && textBoxAdvTableRT[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTableRT[i].Text != "" && textBoxAdvTableRT[i].Text != "До")
                    return false;
            }
            return true;
        }

        private void buttonApplyTableRT_Click(object sender, EventArgs e)
        {
            
            if (textBoxAddTypeIDTableRT.Text != "" && textBoxAddTypeRoom.Text != "" && textBoxAddPrice.Text != "")
            {
                requestsTableRT.Add(listViewTableRT, comboBoxTableRT, textBoxAddTypeIDTableRT.Text, textBoxAddTypeRoom.Text, textBoxAddPrice.Text);
                clearPageAddTableRT();
            }


            if (textBoxDelTableRT.Text != "")
            {
                requestsTableRT.Remove(listViewTableRT, comboBoxTableRT, textBoxDelTableRT.Text);
                textBoxDelTableRT.Text = "";
            }


            if ( ((comboBoxSortTableRT.Text != "" && comboBoxSortTableRT.Text != "(нет)" && textBoxSearchTableRT.Text == "") || 
                (textBoxSearchTableRT.Text != "" && comboBoxSearchTableRT.Text == "(нет)" &&
                 comboBoxSortTableRT.Text != "" && comboBoxSortTableRT.Text != "(нет)")) && checkValueTextBoxAdvSearchTableRT() )
            {
                requestsTableRT.Update(comboBoxTableRT, textBoxNewIDTableRT.Text, textBoxNewRoomType.Text, textBoxNewPrice.Text);
                requestsTableRT.onlySort(listViewTableRT, comboBoxTableRT, comboBoxSortTableRT.Text);

                clearUpdTabControlTableRT();
                clearPageAdvSearchTableRT();
                textBoxSearchTableRT.Text = "";
            }


            if (comboBoxSearchTableRT.Text != "" && comboBoxSortTableRT.Text != "" && comboBoxSearchTableRT.Text != "(нет)" &&
                comboBoxSortTableRT.Text != "(нет)" && textBoxSearchTableRT.Text != "" && checkValueTextBoxAdvSearchTableRT())
            {
                requestsTableRT.Update(comboBoxTableRT, textBoxNewIDTableRT.Text, textBoxNewRoomType.Text, textBoxNewPrice.Text);
                clearUpdTabControlTableRT();
                clearPageAdvSearchTableRT();

                requestsTableRT.valueSearchTableRT = textBoxSearchTableRT.Text;
                requestsTableRT.SortAndSearch(listViewTableRT, comboBoxTableRT, comboBoxSearchTableRT.Text, comboBoxSortTableRT.Text);
            }
           

            if (comboBoxSearchTableRT.Text != "" && comboBoxSearchTableRT.Text != "(нет)" && textBoxSearchTableRT.Text != "" && 
                (comboBoxSortTableRT.Text == "" || comboBoxSortTableRT.Text == "(нет)") && checkValueTextBoxAdvSearchTableRT())
            {
                requestsTableRT.Update(comboBoxTableRT, textBoxNewIDTableRT.Text, textBoxNewRoomType.Text, textBoxNewPrice.Text);
                clearUpdTabControlTableRT();
                clearPageAdvSearchTableRT();

                requestsTableRT.onlySearch(listViewTableRT, comboBoxTableRT, comboBoxSearchTableRT.Text, textBoxSearchTableRT.Text);
                requestsTableRT.valueSearchTableRT = textBoxSearchTableRT.Text;
            }


            if ((comboBoxSortTableRT.Text == "" || comboBoxSortTableRT.Text == "(нет)") && (comboBoxSearchTableRT.Text == "" || comboBoxSearchTableRT.Text == "(нет)") && checkValueTextBoxAdvSearchTableRT() == false)
            {
                requestsTableRT.onlyAdvancedSearch(listViewTableRT, comboBoxTableRT, textBoxAdvTableRT);
                textBoxSearchTableRT.Text = "";
            }
            else if (comboBoxSortTableRT.Text != "" && comboBoxSortTableRT.Text != "(нет)" && (comboBoxSearchTableRT.Text == "" || comboBoxSearchTableRT.Text == "(нет)") && checkValueTextBoxAdvSearchTableRT() == false)
            {
                requestsTableRT.AdvancedSearchAndSort(listViewTableRT, comboBoxTableRT, textBoxAdvTableRT, comboBoxSortTableRT.Text);
                textBoxSearchTableRT.Text = "";
            }
            else if ((comboBoxSortTableRT.Text == "" || comboBoxSortTableRT.Text == "(нет)") && comboBoxSearchTableRT.Text != "" && comboBoxSearchTableRT.Text != "(нет)" &&
                      textBoxSearchTableRT.Text != "" && checkValueTextBoxAdvSearchTableRT() == false)
            {
                requestsTableRT.AdvancedSearchAndMainSearch(listViewTableRT, comboBoxTableRT, textBoxAdvTableRT, comboBoxSearchTableRT.Text, textBoxSearchTableRT.Text);
            }
            else if (comboBoxSortTableRT.Text != "" && comboBoxSortTableRT.Text != "(нет)" && comboBoxSearchTableRT.Text != "" && comboBoxSearchTableRT.Text != "(нет)" &&
                     textBoxSearchTableRT.Text != "" && checkValueTextBoxAdvSearchTableRT() == false)
            {
                requestsTableRT.valueSearchTableRT = textBoxSearchTableRT.Text;
                requestsTableRT.AdvancedSearchAndSortAndSearch(listViewTableRT, comboBoxTableRT, textBoxAdvTableRT, comboBoxSortTableRT.Text, comboBoxSearchTableRT.Text);
            }


            if ( ((comboBoxSortTableRT.Text == "" && comboBoxSearchTableRT.Text == "") || (comboBoxSortTableRT.Text == "(нет)" && comboBoxSearchTableRT.Text == "") ||
                 (comboBoxSearchTableRT.Text == "(нет)" && comboBoxSortTableRT.Text == "") || (comboBoxSearchTableRT.Text == "(нет)" && comboBoxSortTableRT.Text == "(нет)")) && checkValueTextBoxAdvSearchTableRT())
            {
                requestsTableRT.Update(comboBoxTableRT, textBoxNewIDTableRT.Text, textBoxNewRoomType.Text, textBoxNewPrice.Text);
                clearUpdTabControlTableRT();
                clearPageAdvSearchTableRT();

                table.fillRoomType(mysql, listViewTableRT, comboBoxTableRT);
                requestsTableRT.valueSearchTableRT = "";
                textBoxSearchTableRT.Text = "";
            }
        }

        private void listViewTableHS_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewTableHS_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTableHS = new bool[10];
        private void comboBoxOldStaffIDTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[0])
            {
                comboBoxOldStaffIDTableHS.Text = "";
                checkClickElemUpdTableHS[0] = true;
            }
        }

        private void textBoxNewStaffIDTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[1])
            {
                textBoxNewStaffIDTableHS.Clear();
                checkClickElemUpdTableHS[1] = true;
            }
        }

        private void comboBoxCurrentsIDFIOTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[2])
            {
                comboBoxCurrentsIDFIOTableHS.Text = "";
                checkClickElemUpdTableHS[2] = true;
            }
        }

        private void textBoxNewFIOTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[3])
            {
                textBoxNewFIOTableHS.Clear();
                checkClickElemUpdTableHS[3] = true;
            }
        }

        private void comboBoxCurrentsIDPost_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[4])
            {
                comboBoxCurrentsIDPost.Text = "";
                checkClickElemUpdTableHS[4] = true;
            }
        }

        private void textBoxNewPost_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[5])
            {
                textBoxNewPost.Clear();
                checkClickElemUpdTableHS[5] = true;
            }
        }

        private void comboBoxCurrentsIDSalary_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[6])
            {
                comboBoxCurrentsIDSalary.Text = "";
                checkClickElemUpdTableHS[6] = true;
            }
        }

        private void textBoxNewSalary_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[7])
            {
                textBoxNewSalary.Clear();
                checkClickElemUpdTableHS[7] = true;
            }
        }

        private void comboBoxCurrentsIDWorkSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[8])
            {
                comboBoxCurrentsIDWorkSchedule.Text = "";
                checkClickElemUpdTableHS[8] = true;
            }
        }

        private void textBoxNewWorkSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableHS[9])
            {
                textBoxNewWorkSchedule.Clear();
                checkClickElemUpdTableHS[9] = true;
            }
        }

        bool[] checkClickElemAdvTableHS = new bool[4];
        private void textBoxMinStaffIDTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableHS[0])
            {
                textBoxMinStaffIDTableHS.Clear();
                checkClickElemAdvTableHS[0] = true;
            }
        }

        private void textBoxMaxStaffIDTableHS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableHS[1])
            {
                textBoxMaxStaffIDTableHS.Clear();
                checkClickElemAdvTableHS[1] = true;
            }
        }

        private void textBoxMinSalary_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableHS[2])
            {
                textBoxMinSalary.Clear();
                checkClickElemAdvTableHS[2] = true;
            }
        }

        private void textBoxMaxSalary_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableHS[3])
            {
                textBoxMaxSalary.Clear();
                checkClickElemAdvTableHS[3] = true;
            }
        }

        private void tabControlSettingTableHS_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTableHS();
            textBoxDelTableHS.Clear();
            clearUpdTabControlTableHS();
            clearPageAdvSearchTableHS();
        }

        private void tabControlUpdTableHS_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTableHS();
        }

        public void clearUpdTabControlTableHS()
        {
            comboBoxOldStaffIDTableHS.Text = "Старый ID";
            textBoxNewStaffIDTableHS.Text = "Новый ID";

            comboBoxCurrentsIDFIOTableHS.Text = "ID Сотруд.";
            textBoxNewFIOTableHS.Text = "Новое ФИО";

            comboBoxCurrentsIDPost.Text = "ID Сотруд.";
            textBoxNewPost.Text = "Новая должность";

            comboBoxCurrentsIDSalary.Text = "ID Сотруд.";
            textBoxNewSalary.Text = "Новая зар. плата";

            comboBoxCurrentsIDWorkSchedule.Text = "ID Сотруд.";
            textBoxNewWorkSchedule.Text = "Новый раб. график";

            for (int i = 0; i < checkClickElemUpdTableHS.Length; i++)
                checkClickElemUpdTableHS[i] = false;
        }

        public void clearPageAdvSearchTableHS()
        {
            for (int i = 0; i < textBoxAdvTableHS.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTableHS[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTableHS[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTableHS.Length; i++)
                checkClickElemAdvTableHS[i] = false;
        }

        private bool checkValueTextBoxAdvSearchTableHS()
        {
            for (int i = 0; i < textBoxAdvTableHS.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTableHS[i].Text != "" && textBoxAdvTableHS[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTableHS[i].Text != "" && textBoxAdvTableHS[i].Text != "До")
                    return false;
            }
            return true;
        }

        public void clearPageAddTableHS()
        {
            textBoxAddStaffIDTableHS.Clear();
            textBoxAddFIOTableHS.Clear();
            textBoxAddPost.Clear();
            textBoxAddSalary.Clear();
            textBoxAddWorkSchedule.Clear();
        }

        private void buttonApplyTableHS_Click(object sender, EventArgs e)
        {
            if (textBoxAddStaffIDTableHS.Text != "" && textBoxAddFIOTableHS.Text != "" && textBoxAddPost.Text != "" && textBoxAddSalary.Text != "" && textBoxAddWorkSchedule.Text != "")
            {
                requestsTableHS.Add(listViewTableHS, comboBoxTableHS, textBoxAddTableHS);
                clearPageAddTableHS();
            }


            if (textBoxDelTableHS.Text != "")
            {
                requestsTableHS.Remove(listViewTableHS, comboBoxTableHS, textBoxDelTableHS.Text);
                textBoxDelTableHS.Text = "";
            }


            if ( ((comboBoxSortTableHS.Text != "" && comboBoxSortTableHS.Text != "(нет)" && textBoxSearchTableHS.Text == "") ||
                (textBoxSearchTableHS.Text != "" && comboBoxSearchTableHS.Text == "(нет)" &&
                 comboBoxSortTableHS.Text != "" && comboBoxSortTableHS.Text != "(нет)")) && checkValueTextBoxAdvSearchTableHS())
            {
                requestsTableHS.Update(listViewTableHS, comboBoxTableHS, textBoxUpdTableHS);
                requestsTableHS.onlySort(listViewTableHS, comboBoxTableHS, comboBoxSortTableHS.Text);

                clearPageAdvSearchTableHS();
                clearUpdTabControlTableHS();
                textBoxSearchTableHS.Text = "";
            }


            if (comboBoxSearchTableHS.Text != "" && comboBoxSearchTableHS.Text != "(нет)" && textBoxSearchTableHS.Text != "" &&
                (comboBoxSortTableHS.Text == "" || comboBoxSortTableHS.Text == "(нет)") && checkValueTextBoxAdvSearchTableHS())
            {
                requestsTableHS.Update(listViewTableHS, comboBoxTableHS, textBoxUpdTableHS);
                requestsTableHS.onlySearch(listViewTableHS, comboBoxTableHS, comboBoxSearchTableHS.Text, textBoxSearchTableHS.Text);

                clearPageAdvSearchTableHS();
                clearUpdTabControlTableHS();
                requestsTableHS.valueSearchTableHS = textBoxSearchTableHS.Text;
            }


            if (comboBoxSearchTableHS.Text != "" && comboBoxSortTableHS.Text != "" && comboBoxSearchTableHS.Text != "(нет)" &&
                comboBoxSortTableHS.Text != "(нет)" && textBoxSearchTableHS.Text != "" && checkValueTextBoxAdvSearchTableHS())
            {
                requestsTableHS.Update(listViewTableHS, comboBoxTableHS, textBoxUpdTableHS);
                clearUpdTabControlTableHS();
                clearPageAdvSearchTableHS();

                requestsTableHS.valueSearchTableHS = textBoxSearchTableHS.Text;
                requestsTableHS.SortAndSearch(listViewTableHS, comboBoxTableHS, comboBoxSearchTableHS.Text, comboBoxSortTableHS.Text);
            }


            if ((comboBoxSortTableHS.Text == "" || comboBoxSortTableHS.Text == "(нет)") && (comboBoxSearchTableHS.Text == "" || comboBoxSearchTableHS.Text == "(нет)") && checkValueTextBoxAdvSearchTableHS() == false)
            {
                requestsTableHS.onlyAdvancedSearch(listViewTableHS, comboBoxTableHS, textBoxAdvTableHS);
                textBoxSearchTableHS.Text = "";
            }
            else if (comboBoxSortTableHS.Text != "" && comboBoxSortTableHS.Text != "(нет)" && (comboBoxSearchTableHS.Text == "" || comboBoxSearchTableHS.Text == "(нет)") && checkValueTextBoxAdvSearchTableHS() == false)
            {
                requestsTableHS.AdvancedSearchAndSort(listViewTableHS, comboBoxTableHS, textBoxAdvTableHS, comboBoxSortTableHS.Text);
                textBoxSearchTableHS.Text = "";
            }
            else if ((comboBoxSortTableHS.Text == "" || comboBoxSortTableHS.Text == "(нет)") && comboBoxSearchTableHS.Text != "" && comboBoxSearchTableHS.Text != "(нет)" &&
                      textBoxSearchTableHS.Text != "" && checkValueTextBoxAdvSearchTableHS() == false)
            {
                requestsTableHS.AdvancedSearchAndMainSearch(listViewTableHS, comboBoxTableHS, textBoxAdvTableHS, comboBoxSearchTableHS.Text, textBoxSearchTableHS.Text);
            }
            else if (comboBoxSortTableHS.Text != "" && comboBoxSortTableHS.Text != "(нет)" && comboBoxSearchTableHS.Text != "" && comboBoxSearchTableHS.Text != "(нет)" &&
                     textBoxSearchTableHS.Text != "" && checkValueTextBoxAdvSearchTableHS() == false)
            {
                requestsTableHS.valueSearchTableHS = textBoxSearchTableHS.Text;
                requestsTableHS.AdvancedSearchAndSortAndSearch(listViewTableHS, comboBoxTableHS, textBoxAdvTableHS, comboBoxSortTableHS.Text, comboBoxSearchTableHS.Text);
            }


            if ( ((comboBoxSortTableHS.Text == "" && comboBoxSearchTableHS.Text == "") || (comboBoxSortTableHS.Text == "(нет)" && comboBoxSearchTableHS.Text == "") ||
                 (comboBoxSearchTableHS.Text == "(нет)" && comboBoxSortTableHS.Text == "") || (comboBoxSearchTableHS.Text == "(нет)" && comboBoxSortTableHS.Text == "(нет)")) && checkValueTextBoxAdvSearchTableHS())
            {
                requestsTableHS.Update(listViewTableHS, comboBoxTableHS, textBoxUpdTableHS);
                clearUpdTabControlTableHS();
                clearPageAdvSearchTableHS();

                table.fillHotelStaff(mysql, listViewTableHS, comboBoxTableHS);
                requestsTableHS.valueSearchTableHS = "";
                textBoxSearchTableHS.Text = "";
            }
        }

        private void listViewTableRooms_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewTableRooms_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTableRooms = new bool[14];
        private void comboBoxOldRoomNumTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[0])
            {
                comboBoxOldRoomNumTableRooms.Text = "";
                checkClickElemUpdTableRooms[0] = true;
            }
        }

        private void textBoxNewRoomNumTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[1])
            {
                textBoxNewRoomNumTableRooms.Clear();
                checkClickElemUpdTableRooms[1] = true;
            }
        }

        private void comboBoxCurrentsRoomNumPlaces_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[2])
            {
                comboBoxCurrentsRoomNumPlaces.Text = "";
                checkClickElemUpdTableRooms[2] = true;
            }
        }

        private void textBoxNewPlaces_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[3])
            {
                textBoxNewPlaces.Clear();
                checkClickElemUpdTableRooms[3] = true;
            }
        }

        private void comboBoxCurrentsRoomNumRoomFeatures_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[4])
            {
                comboBoxCurrentsRoomNumRoomFeatures.Text = "";
                checkClickElemUpdTableRooms[4] = true;
            }
        }

        private void textBoxNewRoomFeatures_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[5])
            {
                textBoxNewRoomFeatures.Clear();
                checkClickElemUpdTableRooms[5] = true;
            }
        }

        private void comboBoxCurrentsRoomNumFloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[6])
            {
                comboBoxCurrentsRoomNumFloor.Text = "";
                checkClickElemUpdTableRooms[6] = true;
            }
        }

        private void textBoxNewFloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[7])
            {
                textBoxNewFloor.Clear();
                checkClickElemUpdTableRooms[7] = true;
            }
        }

        private void comboBoxOldTypeIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[8])
            {
                comboBoxCurrentsRoomNumTypeID.Text = "";
                checkClickElemUpdTableRooms[8] = true;
            }
        }

        private void comboBoxNewTypeIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[9])
            {
                comboBoxNewTypeIDTableRooms.Text = "";
                checkClickElemUpdTableRooms[9] = true;
            }
        }

        private void comboBoxOldStaffIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[10])
            {
                comboBoxCurrentsRoomNumStaffID.Text = "";
                checkClickElemUpdTableRooms[10] = true;
            }
        }

        private void comboBoxNewStaffIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[11])
            {
                comboBoxNewStaffIDTableRooms.Text = "";
                checkClickElemUpdTableRooms[11] = true;
            }
        }

        private void comboBoxCurrentsRoomNumRoomStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[12])
            {
                comboBoxCurrentsRoomNumRoomStatus.Text = "";
                checkClickElemUpdTableRooms[12] = true;
            }
        }

        private void textBoxNewRoomStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableRooms[13])
            {
                textBoxNewRoomStatus.Clear();
                checkClickElemUpdTableRooms[13] = true;
            }
        }

        bool[] checkClickElemAdvTableRooms = new bool[10];
        private void textBoxMinRoomNumTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[0])
            {
                textBoxMinRoomNumTableRooms.Clear();
                checkClickElemAdvTableRooms[0] = true;
            }
        }

        private void textBoxMaxRoomNumTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[1])
            {
                textBoxMaxRoomNumTableRooms.Clear();
                checkClickElemAdvTableRooms[1] = true;
            }
        }

        private void textBoxMinPlaces_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[2])
            {
                textBoxMinPlaces.Clear();
                checkClickElemAdvTableRooms[2] = true;
            }
        }

        private void textBoxMaxPlaces_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[3])
            {
                textBoxMaxPlaces.Clear();
                checkClickElemAdvTableRooms[3] = true;
            }
        }

        private void textBoxMinFloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[4])
            {
                textBoxMinFloor.Clear();
                checkClickElemAdvTableRooms[4] = true;
            }
        }

        private void textBoxMaxFloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[5])
            {
                textBoxMaxFloor.Clear();
                checkClickElemAdvTableRooms[5] = true;
            }
        }

        private void textBoxMinTypeIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[6])
            {
                textBoxMinTypeIDTableRooms.Clear();
                checkClickElemAdvTableRooms[6] = true;
            }
        }

        private void textBoxMaxTypeIDTableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableRooms[7])
            {
                textBoxMaxTypeIDTableRooms.Clear();
                checkClickElemAdvTableRooms[7] = true;
            }
        }

        private void tabControlSettingTableRooms_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTableRooms();
            textBoxDelTableRooms.Clear();
            clearUpdTabControlTableRooms();
            clearPageAdvSearchTableRooms();
        }

        private void tabControlUpdTableRooms_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTableRooms();
        }

        public void clearUpdTabControlTableRooms()
        {
            comboBoxOldRoomNumTableRooms.Text = "Старый номер";
            textBoxNewRoomNumTableRooms.Text = "Новый номер";

            comboBoxCurrentsRoomNumPlaces.Text = "Номер ком.";
            textBoxNewPlaces.Text = "Новые места";

            comboBoxCurrentsRoomNumRoomFeatures.Text = "Номер ком.";
            textBoxNewRoomFeatures.Text = "Новые особенности";

            comboBoxCurrentsRoomNumFloor.Text = "Номер ком.";
            textBoxNewFloor.Text = "Новый этаж";

            comboBoxCurrentsRoomNumTypeID.Text = "Номер ком.";
            comboBoxNewTypeIDTableRooms.Text = "Новый ID";

            comboBoxCurrentsRoomNumStaffID.Text = "Номер ком.";
            comboBoxNewStaffIDTableRooms.Text = "Новый ID";

            comboBoxCurrentsRoomNumRoomStatus.Text = "Номер ком.";
            textBoxNewRoomStatus.Text = "Новый статус";

            for (int i = 0; i < checkClickElemUpdTableRooms.Length; i++)
                checkClickElemUpdTableRooms[i] = false;
        }

        public void clearPageAdvSearchTableRooms()
        {
            for (int i = 0; i < textBoxAdvTableRooms.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTableRooms[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTableRooms[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTableRooms.Length; i++)
                checkClickElemAdvTableRooms[i] = false;
        }

        public void clearPageAddTableRooms()
        {
            textBoxAddRoomNumTableRooms.Clear();
            textBoxAddPlaces.Clear();
            textBoxAddRoomFeatures.Clear();
            textBoxAddFlor.Clear();
            comboBoxAddTypeIDTableRooms.Text = "";
            comboBoxAddStaffIDTableRooms.Text = "";
            textBoxAddRoomStatus.Clear();
        }

        private bool checkValueTextBoxAdvSearchTableRooms()
        {
            for (int i = 0; i < textBoxAdvTableRooms.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTableRooms[i].Text != "" && textBoxAdvTableRooms[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTableRooms[i].Text != "" && textBoxAdvTableRooms[i].Text != "До")
                    return false;
            }
            return true;
        }

        private void buttonApplyTableRooms_Click(object sender, EventArgs e)
        {
            if (textBoxAddRoomNumTableRooms.Text != "" && textBoxAddPlaces.Text != "" && textBoxAddFlor.Text != "" && comboBoxAddTypeIDTableRooms.Text != "" && textBoxAddRoomStatus.Text != "")
            {
                requestsTableRooms.Add(listViewTableRooms, comboBoxUpdTableRooms, comboBoxAddTableRooms, textBoxAddTableRooms);
                clearPageAddTableRooms();
            }


            if (textBoxDelTableRooms.Text != "")
            {
                requestsTableRooms.Remove(listViewTableRooms, comboBoxUpdTableRooms, comboBoxAddTableRooms, textBoxDelTableRooms.Text);
                textBoxDelTableRooms.Text = "";
            }


            if ( ((comboBoxSortTableRooms.Text != "" && comboBoxSortTableRooms.Text != "(нет)" && textBoxSearchTableRooms.Text == "") ||
                (textBoxSearchTableRooms.Text != "" && comboBoxSearchTableRooms.Text == "(нет)" &&
                 comboBoxSortTableRooms.Text != "" && comboBoxSortTableRooms.Text != "(нет)")) && checkValueTextBoxAdvSearchTableRooms())
            {
                requestsTableRooms.Update(listViewTableRooms, comboBoxUpdTableRooms, textBoxUpdTableRooms);
                requestsTableRooms.onlySort(listViewTableRooms, comboBoxUpdTableRooms, comboBoxSortTableRooms.Text);

                table.fillComboBoxesStaffID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);
                table.fillComboBoxesTypeID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);

                clearUpdTabControlTableRooms();
                clearPageAdvSearchTableRooms();
                textBoxSearchTableRooms.Text = "";
            }


            if (comboBoxSearchTableRooms.Text != "" && comboBoxSearchTableRooms.Text != "(нет)" && textBoxSearchTableRooms.Text != "" &&
                (comboBoxSortTableRooms.Text == "" || comboBoxSortTableRooms.Text == "(нет)") && checkValueTextBoxAdvSearchTableRooms())
            {
                requestsTableRooms.Update(listViewTableRooms, comboBoxUpdTableRooms, textBoxUpdTableRooms);
                requestsTableRooms.onlySearch(listViewTableRooms, comboBoxUpdTableRooms, comboBoxSearchTableRooms.Text, textBoxSearchTableRooms.Text);

                table.fillComboBoxesStaffID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);
                table.fillComboBoxesTypeID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);

                clearUpdTabControlTableRooms();
                clearPageAdvSearchTableRooms();
                requestsTableRooms.valueSearchTableRooms = textBoxSearchTableRooms.Text;
            }


            if (comboBoxSearchTableRooms.Text != "" && comboBoxSortTableRooms.Text != "" && comboBoxSearchTableRooms.Text != "(нет)" &&
                comboBoxSortTableRooms.Text != "(нет)" && textBoxSearchTableRooms.Text != "" && checkValueTextBoxAdvSearchTableRooms())
            {
                requestsTableRooms.Update(listViewTableRooms, comboBoxUpdTableRooms, textBoxUpdTableRooms);
                clearUpdTabControlTableRooms();
                clearPageAdvSearchTableRooms();

                table.fillComboBoxesStaffID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);
                table.fillComboBoxesTypeID(mysql, comboBoxUpdTableRooms, comboBoxAddTableRooms);

                requestsTableRooms.valueSearchTableRooms = textBoxSearchTableRooms.Text;
                requestsTableRooms.SortAndSearch(listViewTableRooms, comboBoxUpdTableRooms, comboBoxSearchTableRooms.Text, comboBoxSortTableRooms.Text);
            }


            if ((comboBoxSortTableRooms.Text == "" || comboBoxSortTableRooms.Text == "(нет)") && (comboBoxSearchTableRooms.Text == "" || comboBoxSearchTableRooms.Text == "(нет)") && checkValueTextBoxAdvSearchTableRooms() == false)
            {
                requestsTableRooms.onlyAdvancedSearch(listViewTableRooms, comboBoxUpdTableRooms, textBoxAdvTableRooms);
                textBoxSearchTableRooms.Text = "";
            }
            else if (comboBoxSortTableRooms.Text != "" && comboBoxSortTableRooms.Text != "(нет)" && (comboBoxSearchTableRooms.Text == "" || comboBoxSearchTableRooms.Text == "(нет)") && checkValueTextBoxAdvSearchTableRooms() == false)
            {
                requestsTableRooms.AdvancedSearchAndSort(listViewTableRooms, comboBoxUpdTableRooms, textBoxAdvTableRooms, comboBoxSortTableRooms.Text);
                textBoxSearchTableRooms.Text = "";
            }
            else if ((comboBoxSortTableRooms.Text == "" || comboBoxSortTableRooms.Text == "(нет)") && comboBoxSearchTableRooms.Text != "" && comboBoxSearchTableRooms.Text != "(нет)" &&
                      textBoxSearchTableRooms.Text != "" && checkValueTextBoxAdvSearchTableRooms() == false)
            {
                requestsTableRooms.AdvancedSearchAndMainSearch(listViewTableRooms, comboBoxUpdTableRooms, textBoxAdvTableRooms, comboBoxSearchTableRooms.Text, textBoxSearchTableRooms.Text);
            }
            else if (comboBoxSortTableRooms.Text != "" && comboBoxSortTableRooms.Text != "(нет)" && comboBoxSearchTableRooms.Text != "" && comboBoxSearchTableRooms.Text != "(нет)" &&
                     textBoxSearchTableRooms.Text != "" && checkValueTextBoxAdvSearchTableRooms() == false)
            {
                requestsTableRooms.valueSearchTableRooms = textBoxSearchTableRooms.Text;
                requestsTableRooms.AdvancedSearchAndSortAndSearch(listViewTableRooms, comboBoxUpdTableRooms, textBoxAdvTableRooms, comboBoxSortTableRooms.Text, comboBoxSearchTableRooms.Text);
            }


            if ( ((comboBoxSortTableRooms.Text == "" && comboBoxSearchTableRooms.Text == "") || (comboBoxSortTableRooms.Text == "(нет)" && comboBoxSearchTableRooms.Text == "") ||
                 (comboBoxSearchTableRooms.Text == "(нет)" && comboBoxSortTableRooms.Text == "") || (comboBoxSearchTableRooms.Text == "(нет)" && comboBoxSortTableRooms.Text == "(нет)")) && checkValueTextBoxAdvSearchTableRooms())
            {
                requestsTableRooms.Update(listViewTableRooms, comboBoxUpdTableRooms, textBoxUpdTableRooms);
                clearUpdTabControlTableRooms();
                clearPageAdvSearchTableRooms();

                table.fillRooms(mysql, listViewTableRooms, comboBoxUpdTableRooms, comboBoxAddTableRooms);
                requestsTableRooms.valueSearchTableRooms = "";
                textBoxSearchTableRooms.Text = "";
            }
        }

        private void listViewTableGuests_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewTableGuests_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTableGuests = new bool[10];

        private void comboBoxOldPassportNumTableGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[0])
            {
                comboBoxOldPassportNumTableGuests.Text = "";
                checkClickElemUpdTableGuests[0] = true;
            }
        }

        private void textBoxNewPassportNumTableGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[1])
            {
                textBoxNewPassportNumTableGuests.Clear();
                checkClickElemUpdTableGuests[1] = true;
            }
        }

        private void comboBoxCurrentsPassportNumFIO_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[2])
            {
                comboBoxCurrentsPassportNumFIO.Text = "";
                checkClickElemUpdTableGuests[2] = true;
            }
        }

        private void textBoxNewFIOTableGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[3])
            {
                textBoxNewFIOTableGuests.Clear();
                checkClickElemUpdTableGuests[3] = true;
            }
        }

        private void comboBoxCurrentsPassportNumCitizenship_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[4])
            {
                comboBoxCurrentsPassportNumCitizenship.Text = "";
                checkClickElemUpdTableGuests[4] = true;
            }
        }

        private void textBoxNewCitizenship_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[5])
            {
                textBoxNewCitizenship.Clear();
                checkClickElemUpdTableGuests[5] = true;
            }
        }

        private void comboBoxCurrentsPassportNumTypeGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[6])
            {
                comboBoxCurrentsPassportNumTypeGuests.Text = "";
                checkClickElemUpdTableGuests[6] = true;
            }
        }

        private void textBoxNewTypeGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[7])
            {
                textBoxNewTypeGuests.Clear();
                checkClickElemUpdTableGuests[7] = true;
            }
        }

        private void comboBoxCurrentsPassportNumDiscount_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[8])
            {
                comboBoxCurrentsPassportNumDiscount.Text = "";
                checkClickElemUpdTableGuests[8] = true;
            }
        }

        private void textBoxNewDiscount_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableGuests[9])
            {
                textBoxNewDiscount.Text = "";
                checkClickElemUpdTableGuests[9] = true;
            }
        }

        bool[] checkClickElemAdvTableGuests = new bool[2];

        private void textBoxMinPassportNumTableGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableGuests[0])
            {
                textBoxMinPassportNumTableGuests.Clear();
                checkClickElemAdvTableGuests[0] = true;
            }
        }

        private void textBoxMaxPassportNumTableGuests_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableGuests[1])
            {
                textBoxMaxPassportNumTableGuests.Clear();
                checkClickElemAdvTableGuests[1] = true;
            }
        }

        private void tabControlSettingTableGuests_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTableGuests();
            textBoxDelTableGuests.Clear();
            clearUpdTabControlTableGuests();
            clearPageAdvSearchTableGuests();
        }

        private void tabControlUpdTableGuests_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTableGuests();
        }

        public void clearPageAddTableGuests()
        {
            textBoxAddPassportNumTableGuests.Clear();
            textBoxAddFIOTableGuests.Clear();
            textBoxAddCitizenship.Clear();
            textBoxAddTypeGuest.Clear();
            textBoxAddDiscount.Clear();
        }

        public void clearUpdTabControlTableGuests()
        {
            comboBoxOldPassportNumTableGuests.Text = "Старый номер";
            textBoxNewPassportNumTableGuests.Text = "Новый номер";

            comboBoxCurrentsPassportNumFIO.Text = "Номер пасп.";
            textBoxNewFIOTableGuests.Text = "Новое ФИО";

            comboBoxCurrentsPassportNumCitizenship.Text = "Номер пасп.";
            textBoxNewCitizenship.Text = "Новое гражданство";

            comboBoxCurrentsPassportNumTypeGuests.Text = "Номер паспорта";
            textBoxNewTypeGuests.Text = "Новый тип гостя";

            comboBoxCurrentsPassportNumDiscount.Text = "Номер пасп.";
            textBoxNewDiscount.Text = "Новая скидка";

            for (int i = 0; i < checkClickElemUpdTableGuests.Length; i++)
                checkClickElemUpdTableGuests[i] = false;
        }

        public void clearPageAdvSearchTableGuests()
        {
            for (int i = 0; i < textBoxAdvTableGuests.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTableGuests[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTableGuests[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTableGuests.Length; i++)
                checkClickElemAdvTableGuests[i] = false;
        }

        private bool checkValueTextBoxAdvSearchTableGuests()
        {
            for (int i = 0; i < textBoxAdvTableGuests.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTableGuests[i].Text != "" && textBoxAdvTableGuests[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTableGuests[i].Text != "" && textBoxAdvTableGuests[i].Text != "До")
                    return false;
            }
            return true;
        }

        private void buttonApplyTableGuests_Click(object sender, EventArgs e)
        {
            if (textBoxAddPassportNumTableGuests.Text != "" && textBoxAddFIOTableGuests.Text != "" && textBoxAddCitizenship.Text != "" && textBoxAddTypeGuest.Text != "")
            {
                requestsTableGuests.Add(listViewTableGuests, comboBoxTableGuests, textBoxAddTableGuests);
                clearPageAddTableGuests();
            }


            if (textBoxDelTableGuests.Text != "")
            {
                requestsTableGuests.Remove(listViewTableGuests, comboBoxTableGuests, textBoxDelTableGuests.Text);
                textBoxDelTableGuests.Text = "";
            }


            if (((comboBoxSortTableGuests.Text != "" && comboBoxSortTableGuests.Text != "(нет)" && textBoxSearchTableGuests.Text == "") ||
                (textBoxSearchTableGuests.Text != "" && comboBoxSearchTableGuests.Text == "(нет)" &&
                 comboBoxSortTableGuests.Text != "" && comboBoxSortTableGuests.Text != "(нет)")) && checkValueTextBoxAdvSearchTableGuests())
            {
                requestsTableGuests.Update(listViewTableGuests, comboBoxTableGuests, textBoxUpdTableGuests);
                requestsTableGuests.onlySort(listViewTableGuests, comboBoxTableGuests, comboBoxSortTableGuests.Text);

                clearPageAdvSearchTableGuests();
                clearUpdTabControlTableGuests();
                textBoxSearchTableGuests.Text = "";
            }


            if (comboBoxSearchTableGuests.Text != "" && comboBoxSearchTableGuests.Text != "(нет)" && textBoxSearchTableGuests.Text != "" &&
                (comboBoxSortTableGuests.Text == "" || comboBoxSortTableGuests.Text == "(нет)") && checkValueTextBoxAdvSearchTableGuests())
            {
                requestsTableGuests.Update(listViewTableGuests, comboBoxTableGuests, textBoxUpdTableGuests);
                requestsTableGuests.onlySearch(listViewTableGuests, comboBoxTableGuests, comboBoxSearchTableGuests.Text, textBoxSearchTableGuests.Text);

                clearPageAdvSearchTableGuests();
                clearUpdTabControlTableGuests();
                requestsTableGuests.valueSearchTableGuests = textBoxSearchTableGuests.Text;
            }


            if (comboBoxSearchTableGuests.Text != "" && comboBoxSortTableGuests.Text != "" && comboBoxSearchTableGuests.Text != "(нет)" &&
                comboBoxSortTableGuests.Text != "(нет)" && textBoxSearchTableGuests.Text != "" && checkValueTextBoxAdvSearchTableGuests())
            {
                requestsTableGuests.Update(listViewTableGuests, comboBoxTableGuests, textBoxUpdTableGuests);
                clearUpdTabControlTableGuests();
                clearPageAdvSearchTableGuests();

                requestsTableGuests.valueSearchTableGuests = textBoxSearchTableGuests.Text;
                requestsTableGuests.SortAndSearch(listViewTableGuests, comboBoxTableGuests, comboBoxSearchTableGuests.Text, comboBoxSortTableGuests.Text);
            }


            if ((comboBoxSortTableGuests.Text == "" || comboBoxSortTableGuests.Text == "(нет)") && (comboBoxSearchTableGuests.Text == "" || comboBoxSearchTableGuests.Text == "(нет)") && checkValueTextBoxAdvSearchTableGuests() == false)
            {
                requestsTableGuests.onlyAdvancedSearch(listViewTableGuests, comboBoxTableGuests, textBoxAdvTableGuests);
                textBoxSearchTableGuests.Text = "";
            }
            else if (comboBoxSortTableGuests.Text != "" && comboBoxSortTableGuests.Text != "(нет)" && (comboBoxSearchTableGuests.Text == "" || comboBoxSearchTableGuests.Text == "(нет)") && checkValueTextBoxAdvSearchTableGuests() == false)
            {
                requestsTableGuests.AdvancedSearchAndSort(listViewTableGuests, comboBoxTableGuests, textBoxAdvTableGuests, comboBoxSortTableGuests.Text);
                textBoxSearchTableGuests.Text = "";
            }
            else if ((comboBoxSortTableGuests.Text == "" || comboBoxSortTableGuests.Text == "(нет)") && comboBoxSearchTableGuests.Text != "" && comboBoxSearchTableGuests.Text != "(нет)" &&
                      textBoxSearchTableGuests.Text != "" && checkValueTextBoxAdvSearchTableGuests() == false)
            {
                requestsTableGuests.AdvancedSearchAndMainSearch(listViewTableGuests, comboBoxTableGuests, textBoxAdvTableGuests, comboBoxSearchTableGuests.Text, textBoxSearchTableGuests.Text);
            }
            else if (comboBoxSortTableGuests.Text != "" && comboBoxSortTableGuests.Text != "(нет)" && comboBoxSearchTableGuests.Text != "" && comboBoxSearchTableGuests.Text != "(нет)" &&
                     textBoxSearchTableGuests.Text != "" && checkValueTextBoxAdvSearchTableGuests() == false)
            {
                requestsTableGuests.valueSearchTableGuests = textBoxSearchTableGuests.Text;
                requestsTableGuests.AdvancedSearchAndSortAndSearch(listViewTableGuests, comboBoxTableGuests, textBoxAdvTableGuests, comboBoxSortTableGuests.Text, comboBoxSearchTableGuests.Text);
            }


            if (((comboBoxSortTableGuests.Text == "" && comboBoxSearchTableGuests.Text == "") || (comboBoxSortTableGuests.Text == "(нет)" && comboBoxSearchTableGuests.Text == "") ||
                 (comboBoxSearchTableGuests.Text == "(нет)" && comboBoxSortTableGuests.Text == "") || (comboBoxSearchTableGuests.Text == "(нет)" && comboBoxSortTableGuests.Text == "(нет)")) && checkValueTextBoxAdvSearchTableGuests())
            {
                requestsTableGuests.Update(listViewTableGuests, comboBoxTableGuests, textBoxUpdTableGuests);
                clearUpdTabControlTableGuests();
                clearPageAdvSearchTableGuests();

                table.fillGuests(mysql, listViewTableGuests, comboBoxTableGuests);
                requestsTableGuests.valueSearchTableGuests = "";
                textBoxSearchTableGuests.Text = "";
            }
        }

        private void listViewTablePlacement_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewTablePlacement_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTablePlacement = new bool[8];

        private void comboBoxOldRoomNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[0])
            {
                comboBoxOldRoomNumTablePlacement.Text = "";
                checkClickElemUpdTablePlacement[0] = true;
            }
        }

        private void comboBoxNewRoomNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[1])
            {
                comboBoxNewRoomNumTablePlacement.Text = "";
                checkClickElemUpdTablePlacement[1] = true;
            }
        }

        private void comboBoxOldPassportNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[2])
            {
                comboBoxOldPassportNumTablePlacement.Text = "";
                checkClickElemUpdTablePlacement[2] = true;
            }
        }

        private void comboBoxNewPassportNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[3])
            {
                comboBoxNewPassportNumTablePlacement.Text = "";
                checkClickElemUpdTablePlacement[3] = true;
            }
        }

        private void comboBoxCurrentsPassportNumSetDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[4])
            {
                comboBoxCurrentsPassportNumSetDate.Text = "";
                checkClickElemUpdTablePlacement[4] = true;
            }
        }

        private void textBoxNewSetDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[5])
            {
                textBoxNewSetDate.Clear();
                checkClickElemUpdTablePlacement[5] = true;
            }
        }

        private void comboBoxCurrentsPassportNumDepartureDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[6])
            {
                comboBoxCurrentsPassportNumDepartureDate.Text = "";
                checkClickElemUpdTablePlacement[6] = true;
            }
        }

        private void textBoxNewDepartureDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTablePlacement[7])
            {
                textBoxNewDepartureDate.Clear();
                checkClickElemUpdTablePlacement[7] = true;
            }
        }

        bool[] checkClickElemAdvTablePlacement = new bool[6];

        private void textBoxMinRoomNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[0])
            {
                textBoxMinRoomNumTablePlacement.Clear();
                checkClickElemAdvTablePlacement[0] = true;
            }
        }

        private void textBoxMaxRoomNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[1])
            {
                textBoxMaxRoomNumTablePlacement.Clear();
                checkClickElemAdvTablePlacement[1] = true;
            }
        }

        private void textBoxMinPassportNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[2])
            {
                textBoxMinPassportNumTablePlacement.Clear();
                checkClickElemAdvTablePlacement[2] = true;
            }
        }

        private void textBoxMaxPassportNumTablePlacement_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[3])
            {
                textBoxMaxPassportNumTablePlacement.Clear();
                checkClickElemAdvTablePlacement[3] = true;
            }
        }

        private void textBoxMinSetDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[4])
            {
                textBoxMinSetDate.Clear();
                checkClickElemAdvTablePlacement[4] = true;
            }
        }

        private void textBoxMaxSetDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTablePlacement[5])
            {
                textBoxMaxSetDate.Clear();
                checkClickElemAdvTablePlacement[5] = true;
            }
        }

        private void tabControltabControlSettingTablePlacement_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTablePlacement();
            textBoxDelRoomNumTablePlacement.Clear();
            textBoxDelPassportNumTablePlacement.Clear();
            clearUpdTabControlTablePlacement();
            clearPageAdvSearchTablePlacement();
        }

        private void tabControlUpdTablePlacement_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTablePlacement();
        }

        public void clearPageAddTablePlacement()
        {
            comboBoxAddRoomNumTablePlacement.Text = "";
            comboBoxAddPassportNumTablePlacement.Text = "";
            textBoxAddSetDate.Clear();
            textBoxAddDepartureDate.Clear();
        }

        public void clearUpdTabControlTablePlacement()
        {
            comboBoxOldRoomNumTablePlacement.Text = "Старый номер";
            comboBoxNewRoomNumTablePlacement.Text = "Новый номер";

            comboBoxOldPassportNumTablePlacement.Text = "Старый номер";
            comboBoxNewPassportNumTablePlacement.Text = "Новый номер";

            comboBoxCurrentsPassportNumSetDate.Text = "Номер паспорта";
            textBoxNewSetDate.Text = "Новая дата заезда";

            comboBoxCurrentsPassportNumDepartureDate.Text = "Номер паспорта";
            textBoxNewDepartureDate.Text = "Новая дата выезда";

            for (int i = 0; i < checkClickElemUpdTablePlacement.Length; i++)
                checkClickElemUpdTablePlacement[i] = false;
        }

        public void clearPageAdvSearchTablePlacement()
        {
            for (int i = 0; i < textBoxAdvTablePlacement.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTablePlacement[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTablePlacement[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTablePlacement.Length; i++)
                checkClickElemAdvTablePlacement[i] = false;
        }

        private bool checkValueTextBoxAdvSearchTablePlacement()
        {
            for (int i = 0; i < textBoxAdvTablePlacement.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTablePlacement[i].Text != "" && textBoxAdvTablePlacement[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTablePlacement[i].Text != "" && textBoxAdvTablePlacement[i].Text != "До")
                    return false;
            }
            return true;
        }

        private void buttonApplyTablePlacement_Click(object sender, EventArgs e)
        {
            if (comboBoxAddRoomNumTablePlacement.Text != "" && comboBoxAddPassportNumTablePlacement.Text != "" && textBoxAddSetDate.Text != "")
            {
                requestsTablePlacement.Add(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxAddTablePlacement);
                clearPageAddTablePlacement();
            }


            if (textBoxDelRoomNumTablePlacement.Text != "" && textBoxDelPassportNumTablePlacement.Text != "")
            {
                requestsTablePlacement.Remove(listViewTableRooms, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxDelTablePlacement);
                textBoxDelRoomNumTablePlacement.Text = "";
                textBoxDelPassportNumTablePlacement.Text = "";
            }


            if (((comboBoxSortTablePlacement.Text != "" && comboBoxSortTablePlacement.Text != "(нет)" && textBoxSearchTablePlacement.Text == "") ||
                (textBoxSearchTablePlacement.Text != "" && comboBoxSearchTablePlacement.Text == "(нет)" && 
                 comboBoxSortTablePlacement.Text != "" && comboBoxSortTablePlacement.Text != "(нет)")) && checkValueTextBoxAdvSearchTablePlacement())
            {
                requestsTablePlacement.Update(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxUpdTablePlacement);
                requestsTablePlacement.onlySort(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxSortTablePlacement.Text);

                table.fillComboBoxesRoomNumTablePlacement(mysql, comboBoxReferencesDataTablePlacement);
                table.fillComboBoxesPassportNum(mysql, comboBoxReferencesDataTablePlacement);

                clearUpdTabControlTablePlacement();
                clearPageAdvSearchTablePlacement();
                textBoxSearchTablePlacement.Text = "";
            }


            if (comboBoxSearchTablePlacement.Text != "" && comboBoxSearchTablePlacement.Text != "(нет)" && textBoxSearchTablePlacement.Text != "" &&
                (comboBoxSortTablePlacement.Text == "" || comboBoxSortTablePlacement.Text == "(нет)") && checkValueTextBoxAdvSearchTablePlacement())
            {
                requestsTablePlacement.Update(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxUpdTablePlacement);
                requestsTablePlacement.onlySearch(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxSearchTablePlacement.Text, textBoxSearchTablePlacement.Text);

                table.fillComboBoxesRoomNumTablePlacement(mysql, comboBoxReferencesDataTablePlacement);
                table.fillComboBoxesPassportNum(mysql, comboBoxReferencesDataTablePlacement);

                clearUpdTabControlTablePlacement();
                clearPageAdvSearchTablePlacement();
                requestsTablePlacement.valueSearchTablePlacement = textBoxSearchTablePlacement.Text;
            }


            if (comboBoxSearchTablePlacement.Text != "" && comboBoxSortTablePlacement.Text != "" && comboBoxSearchTablePlacement.Text != "(нет)" &&
                comboBoxSortTablePlacement.Text != "(нет)" && textBoxSearchTablePlacement.Text != "" && checkValueTextBoxAdvSearchTablePlacement())
            {
                requestsTablePlacement.Update(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxUpdTablePlacement);
                clearUpdTabControlTablePlacement();
                clearPageAdvSearchTablePlacement();

                table.fillComboBoxesRoomNumTablePlacement(mysql, comboBoxReferencesDataTablePlacement);
                table.fillComboBoxesPassportNum(mysql, comboBoxReferencesDataTablePlacement);

                requestsTablePlacement.valueSearchTablePlacement = textBoxSearchTablePlacement.Text;
                requestsTablePlacement.SortAndSearch(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxSearchTablePlacement.Text, comboBoxSortTablePlacement.Text);
            }


            if ((comboBoxSortTablePlacement.Text == "" || comboBoxSortTablePlacement.Text == "(нет)") && (comboBoxSearchTablePlacement.Text == "" || comboBoxSearchTablePlacement.Text == "(нет)") && checkValueTextBoxAdvSearchTablePlacement() == false)
            {
                requestsTablePlacement.onlyAdvancedSearch(listViewTablePlacement, comboBoxUpdTablePlacement, textBoxAdvTablePlacement);
                textBoxSearchTablePlacement.Text = "";
            }
            else if (comboBoxSortTablePlacement.Text != "" && comboBoxSortTablePlacement.Text != "(нет)" && (comboBoxSearchTablePlacement.Text == "" || comboBoxSearchTablePlacement.Text == "(нет)") && checkValueTextBoxAdvSearchTablePlacement() == false)
            {
                requestsTablePlacement.AdvancedSearchAndSort(listViewTablePlacement, comboBoxUpdTablePlacement, textBoxAdvTablePlacement, comboBoxSortTablePlacement.Text);
                textBoxSearchTablePlacement.Text = "";
            }
            else if ((comboBoxSortTablePlacement.Text == "" || comboBoxSortTablePlacement.Text == "(нет)") && comboBoxSearchTablePlacement.Text != "" && comboBoxSearchTablePlacement.Text != "(нет)" &&
                      textBoxSearchTablePlacement.Text != "" && checkValueTextBoxAdvSearchTablePlacement() == false)
            {
                requestsTablePlacement.AdvancedSearchAndMainSearch(listViewTablePlacement, comboBoxUpdTablePlacement, textBoxAdvTablePlacement, comboBoxSearchTablePlacement.Text, textBoxSearchTablePlacement.Text);
            }
            else if (comboBoxSortTablePlacement.Text != "" && comboBoxSortTablePlacement.Text != "(нет)" && comboBoxSearchTablePlacement.Text != "" && comboBoxSearchTablePlacement.Text != "(нет)" &&
                     textBoxSearchTablePlacement.Text != "" && checkValueTextBoxAdvSearchTablePlacement() == false)
            {
                requestsTablePlacement.valueSearchTablePlacement = textBoxSearchTablePlacement.Text;
                requestsTablePlacement.AdvancedSearchAndSortAndSearch(listViewTablePlacement, comboBoxUpdTablePlacement, textBoxAdvTablePlacement, comboBoxSortTablePlacement.Text, comboBoxSearchTablePlacement.Text);
            }


            if (((comboBoxSortTablePlacement.Text == "" && comboBoxSearchTablePlacement.Text == "") || (comboBoxSortTablePlacement.Text == "(нет)" && comboBoxSearchTablePlacement.Text == "") ||
                 (comboBoxSearchTablePlacement.Text == "(нет)" && comboBoxSortTablePlacement.Text == "") || (comboBoxSearchTablePlacement.Text == "(нет)" && comboBoxSortTablePlacement.Text == "(нет)")) && checkValueTextBoxAdvSearchTablePlacement())
            {
                requestsTablePlacement.Update(listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement, textBoxUpdTablePlacement);
                clearUpdTabControlTablePlacement();
                clearPageAdvSearchTablePlacement();

                table.fillPlacement(mysql, listViewTablePlacement, comboBoxUpdTablePlacement, comboBoxReferencesDataTablePlacement);
                requestsTablePlacement.valueSearchTablePlacement = "";
                textBoxSearchTablePlacement.Text = "";
            }
        }

        private void listViewTableDailyAccounting_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
            e.DrawText();
        }

        private void listViewTableDailyAccounting_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        bool[] checkClickElemUpdTableDA = new bool[10];

        private void comboBoxOldRoomNumTableDailyAccounting_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[0])
            {
                comboBoxOldRoomNumTableDailyAccounting.Text = "";
                checkClickElemUpdTableDA[0] = true;
            }
        }

        private void comboBoxNewRoomNumTableDailyAccounting_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[1])
            {
                comboBoxNewRoomNumTableDailyAccounting.Text = "";
                checkClickElemUpdTableDA[1] = true;
            }
        }

        private void comboBoxCurrentsRoomNumServiceDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[2])
            {
                comboBoxCurrentsRoomNumServiceDate.Text = "";
                checkClickElemUpdTableDA[2] = true;
            }
        }

        private void textBoxNewServiceDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[3])
            {
                textBoxNewServiceDate.Clear();
                checkClickElemUpdTableDA[3] = true;
            }
        }

        private void comboBoxCurrentsRoomNumConditionRoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[4])
            {
                comboBoxCurrentsRoomNumConditionRoom.Text = "";
                checkClickElemUpdTableDA[4] = true;
            }
        }

        private void textBoxNewConditionRoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[5])
            {
                textBoxNewConditionRoom.Clear();
                checkClickElemUpdTableDA[5] = true;
            }
        }

        private void comboBoxCurrentsRoomNumComplaints_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[6])
            {
                comboBoxCurrentsRoomNumComplaints.Text = "";
                checkClickElemUpdTableDA[6] = true;
            }
        }

        private void textBoxNewComplaints_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[7])
            {
                textBoxNewComplaints.Clear();
                checkClickElemUpdTableDA[7] = true;
            }
        }

        private void comboBoxCurrentsRoomNumServicesRendered_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[8])
            {
                comboBoxCurrentsRoomNumServicesRendered.Text = "";
                checkClickElemUpdTableDA[8] = true;
            }
        }

        private void textBoxNewServicesRendered_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemUpdTableDA[9])
            {
                textBoxNewServicesRendered.Clear();
                checkClickElemUpdTableDA[8] = true;
            }
        }

        bool[] checkClickElemAdvTableDA = new bool[4];

        private void textBoxMinRoomNumTableDA_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableDA[0])
            {
                textBoxMinRoomNumTableDA.Clear();
                checkClickElemAdvTableDA[0] = true;
            }
        }

        private void textBoxMaxRoomNumTableDA_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableDA[1])
            {
                textBoxMaxRoomNumTableDA.Clear();
                checkClickElemAdvTableDA[1] = true;
            }
        }

        private void textBoxMinServiceDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableDA[2])
            {
                textBoxMinServiceDate.Clear();
                checkClickElemAdvTableDA[2] = true;
            }
        }

        private void textBoxMaxServiceDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkClickElemAdvTableDA[3])
            {
                textBoxMaxServiceDate.Clear();
                checkClickElemAdvTableDA[3] = true;
            }
        }

        private void tabControltabControlSettingTableDailyAccounting_Selected(object sender, TabControlEventArgs e)
        {
            clearPageAddTableDA();
            textBoxDelTableDailyAccounting.Clear();
            clearUpdTabControlTableDA();
            clearPageAdvSearchTableDA();
        }

        private void tabControlUpdTableDailyAccounting_Selected(object sender, TabControlEventArgs e)
        {
            clearUpdTabControlTableDA();
        }

        public void clearPageAddTableDA()
        {
            comboBoxAddRoomNumTableDA.Text = "";
            textBoxAddServiceDate.Clear();
            textBoxAddConditionRoom.Clear();
            textBoxAddComplaints.Clear();
            textBoxAddServicesRendered.Clear();
        }

        public void clearUpdTabControlTableDA()
        {
            comboBoxOldRoomNumTableDailyAccounting.Text = "Старый номер";
            comboBoxNewRoomNumTableDailyAccounting.Text = "Новый номер";

            comboBoxCurrentsRoomNumServiceDate.Text = "Номер комнаты";
            textBoxNewServiceDate.Text = "Новая дата обслуживания";

            comboBoxCurrentsRoomNumConditionRoom.Text = "Номер комнаты";
            textBoxNewConditionRoom.Text = "Новый статус";

            comboBoxCurrentsRoomNumComplaints.Text = "Номер комнаты";
            textBoxNewComplaints.Text = "Новая жалоба";

            comboBoxCurrentsRoomNumServicesRendered.Text = "Номер комнаты";
            textBoxNewServicesRendered.Text = "Новые оказанные услуги";

            for (int i = 0; i < checkClickElemUpdTableDA.Length; i++)
                checkClickElemUpdTableDA[i] = false;
        }

        public void clearPageAdvSearchTableDA()
        {
            for (int i = 0; i < textBoxAdvTableDA.Length; i++)
            {
                if (i % 2 == 0)
                    textBoxAdvTableDA[i].Text = "От";
                else if (i % 2 != 0)
                    textBoxAdvTableDA[i].Text = "До";
            }

            for (int i = 0; i < checkClickElemAdvTableDA.Length; i++)
                checkClickElemAdvTableDA[i] = false;
        }

        private bool checkValueTextBoxAdvSearchTableDA()
        {
            for (int i = 0; i < textBoxAdvTableDA.Length; i++)
            {
                if (i % 2 == 0 && textBoxAdvTableDA[i].Text != "" && textBoxAdvTableDA[i].Text != "От")
                    return false;
                else if (i % 2 != 0 && textBoxAdvTableDA[i].Text != "" && textBoxAdvTableDA[i].Text != "До")
                    return false;
            }
            return true;
        }

        private void buttonApplyTableDailyAccounting_Click(object sender, EventArgs e)
        {
            if (comboBoxAddRoomNumTableDA.Text != "" && textBoxAddServiceDate.Text != "" && textBoxAddConditionRoom.Text != "" && textBoxAddComplaints.Text != "" && textBoxAddServicesRendered.Text != "")
            {
                requestsTableDA.Add(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxAddTableDA);
                clearPageAddTableDA();
            }


            if (textBoxDelTableDailyAccounting.Text != "")
            {
                requestsTableDA.Remove(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxDelTableDailyAccounting.Text);
                textBoxDelTableDailyAccounting.Text = "";
            }


            if (((comboBoxSortTableDA.Text != "" && comboBoxSortTableDA.Text != "(нет)" && textBoxSearchTableDA.Text == "") ||
                (textBoxSearchTableDA.Text != "" && comboBoxSearchTableDA.Text == "(нет)" &&
                 comboBoxSortTableDA.Text != "" && comboBoxSortTableDA.Text != "(нет)")) && checkValueTextBoxAdvSearchTableDA())
            {
                requestsTableDA.Update(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxUpdTableDA);
                requestsTableDA.onlySort(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxSortTableDA.Text);

                table.fillComboBoxesRoomNumTableDA(mysql, comboBoxReferencesDataTableDA);
               
                clearUpdTabControlTableDA();
                clearPageAdvSearchTableDA();
                textBoxSearchTableDA.Text = "";
            }


            if (comboBoxSearchTableDA.Text != "" && comboBoxSearchTableDA.Text != "(нет)" && textBoxSearchTableDA.Text != "" &&
                (comboBoxSortTableDA.Text == "" || comboBoxSortTableDA.Text == "(нет)") && checkValueTextBoxAdvSearchTableDA())
            {
                requestsTableDA.Update(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxUpdTableDA);
                requestsTableDA.onlySearch(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxSearchTableDA.Text, textBoxSearchTableDA.Text);

                table.fillComboBoxesRoomNumTableDA(mysql, comboBoxReferencesDataTableDA);

                clearUpdTabControlTableDA();
                clearPageAdvSearchTableDA();
                requestsTableDA.valueSearchTableDA = textBoxSearchTableDA.Text;
            }


            if (comboBoxSearchTableDA.Text != "" && comboBoxSortTableDA.Text != "" && comboBoxSearchTableDA.Text != "(нет)" &&
                comboBoxSortTableDA.Text != "(нет)" && textBoxSearchTableDA.Text != "" && checkValueTextBoxAdvSearchTableDA())
            {
                requestsTableDA.Update(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxUpdTableDA);
                table.fillComboBoxesRoomNumTableDA(mysql, comboBoxReferencesDataTableDA);

                clearUpdTabControlTableDA();
                clearPageAdvSearchTableDA();

                requestsTableDA.valueSearchTableDA = textBoxSearchTableDA.Text;
                requestsTableDA.SortAndSearch(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxSearchTableDA.Text, comboBoxSortTableDA.Text);
            }


            if ((comboBoxSortTableDA.Text == "" || comboBoxSortTableDA.Text == "(нет)") && (comboBoxSearchTableDA.Text == "" || comboBoxSearchTableDA.Text == "(нет)") && checkValueTextBoxAdvSearchTableDA() == false)
            {
                requestsTableDA.onlyAdvancedSearch(listViewTableDailyAccounting, comboBoxUpdTableDA, textBoxAdvTableDA);
                textBoxSearchTableDA.Text = "";
            }
            else if (comboBoxSortTableDA.Text != "" && comboBoxSortTableDA.Text != "(нет)" && (comboBoxSearchTableDA.Text == "" || comboBoxSearchTableDA.Text == "(нет)") && checkValueTextBoxAdvSearchTableDA() == false)
            {
                requestsTableDA.AdvancedSearchAndSort(listViewTableDailyAccounting, comboBoxUpdTableDA, textBoxAdvTableDA, comboBoxSortTableDA.Text);
                textBoxSearchTableDA.Text = "";
            }
            else if ((comboBoxSortTableDA.Text == "" || comboBoxSortTableDA.Text == "(нет)") && comboBoxSearchTableDA.Text != "" && comboBoxSearchTableDA.Text != "(нет)" &&
                      textBoxSearchTableDA.Text != "" && checkValueTextBoxAdvSearchTableDA() == false)
            {
                requestsTableDA.AdvancedSearchAndMainSearch(listViewTableDailyAccounting, comboBoxUpdTableDA, textBoxAdvTableDA, comboBoxSearchTableDA.Text, textBoxSearchTableDA.Text);
            }
            else if (comboBoxSortTableDA.Text != "" && comboBoxSortTableDA.Text != "(нет)" && comboBoxSearchTableDA.Text != "" && comboBoxSearchTableDA.Text != "(нет)" &&
                     textBoxSearchTableDA.Text != "" && checkValueTextBoxAdvSearchTableDA() == false)
            {
                requestsTableDA.valueSearchTableDA = textBoxSearchTableDA.Text;
                requestsTableDA.AdvancedSearchAndSortAndSearch(listViewTableDailyAccounting, comboBoxUpdTableDA, textBoxAdvTableDA, comboBoxSortTableDA.Text, comboBoxSearchTableDA.Text);
            }


            if (((comboBoxSortTableDA.Text == "" && comboBoxSearchTableDA.Text == "") || (comboBoxSortTableDA.Text == "(нет)" && comboBoxSearchTableDA.Text == "") ||
                 (comboBoxSearchTableDA.Text == "(нет)" && comboBoxSortTableDA.Text == "") || (comboBoxSearchTableDA.Text == "(нет)" && comboBoxSortTableDA.Text == "(нет)")) && checkValueTextBoxAdvSearchTableDA())
            {
                requestsTableDA.Update(listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA, textBoxUpdTableDA);
                clearUpdTabControlTableDA();
                clearPageAdvSearchTableDA();

                table.fillDailyAccounting(mysql, listViewTableDailyAccounting, comboBoxUpdTableDA, comboBoxReferencesDataTableDA);
                requestsTableDA.valueSearchTableDA = "";
                textBoxSearchTableDA.Text = "";
            }
        }
    }
}

