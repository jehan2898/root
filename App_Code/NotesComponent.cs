using AjaxControlToolkit;
using ExtendedDropDownList;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace NotesComponent
{
    public class NotesOperation
    {
        private SqlConnection sqlConnection;

        private SqlCommand sqlCommand;

        private SqlDataAdapter sqlDataAdapter;

        private DataSet dataSet;

        private SqlDataReader sqlDataReader;

        private string strConn;

        private Hashtable _hashtableControlCollection = new Hashtable();

        private string _xml_File = "";

        private Page _web_Page;

        private string _Case_ID = string.Empty;

        private string _User_ID = string.Empty;

        private string _Company_ID = string.Empty;

        public string Case_ID
        {
            get
            {
                return this._Case_ID;
            }
            set
            {
                this._Case_ID = value;
            }
        }

        public string Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                this._Company_ID = value;
            }
        }

        public string User_ID
        {
            get
            {
                return this._User_ID;
            }
            set
            {
                this._User_ID = value;
            }
        }

        public Page WebPage
        {
            get
            {
                return this._web_Page;
            }
            set
            {
                this._web_Page = value;
            }
        }

        public string Xml_File
        {
            get
            {
                return this._xml_File;
            }
            set
            {
                this._xml_File = value;
            }
        }

        public NotesOperation()
        {
            this.strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        }

        public void SaveNotesOperation()
        {
            int i;
            int j;
            int k;
            string str;
            string[] strArrays;
            string[] value;
            char[] chrArray;
            try
            {
                if (!this.WebPage.IsPostBack)
                {
                    this.WebPage.Session["CONTROL_OLD_VALUES"] = null;
                }
                this.sqlConnection = new SqlConnection(this.strConn);
                this.sqlConnection.Open();
                ArrayList arrayLists = new ArrayList();
                for (i = 0; i <= this.WebPage.Form.Controls.Count - 1; i++)
                {
                    if (this.WebPage.Form.Controls[i].ID != null)
                    {
                        if (this.WebPage.Form.Controls[i].GetType() == typeof(Panel))
                        {
                            for (j = 0; j <= ((Panel)this.WebPage.Form.Controls[i]).Controls.Count - 1; j++)
                            {
                                if (((Panel)this.WebPage.Form.Controls[i]).Controls[j].ID != null)
                                {
                                    arrayLists.Add(((Panel)this.WebPage.Form.Controls[i]).Controls[j]);
                                }
                            }
                        }
                        else if (this.WebPage.Form.Controls[i].GetType() == typeof(HtmlTable))
                        {
                            for (k = 0; k <= ((HtmlTable)this.WebPage.Form.Controls[i]).Controls.Count - 1; k++)
                            {
                                if (((HtmlTable)this.WebPage.Form.Controls[i]).Controls[k].ID != null)
                                {
                                    arrayLists.Add(((HtmlTable)this.WebPage.Form.Controls[i]).Controls[k]);
                                }
                            }
                        }
                        else if (this.WebPage.Form.Controls[i].GetType() == typeof(TabContainer))
                        {
                            for (k = 0; k <= ((TabContainer)this.WebPage.Form.Controls[i]).Controls.Count - 1; k++)
                            {
                                if (((TabContainer)this.WebPage.Form.Controls[i]).Controls[k].ID == null)
                                {
                                    arrayLists.Add(((TabContainer)this.WebPage.Form.Controls[i]).Controls[k]);
                                }
                                else if (((TabContainer)this.WebPage.Form.Controls[i]).Controls[k].GetType() == typeof(TabPanel))
                                {
                                    for (j = 0; j <= ((TabPanel)((TabContainer)this.WebPage.Form.Controls[i]).Controls[k]).Controls.Count - 1; j++)
                                    {
                                        for (int l = 0; l <= ((TabPanel)((TabContainer)this.WebPage.Form.Controls[i]).Controls[k]).Controls[j].Controls.Count - 1; l++)
                                        {
                                            if (((TabPanel)((TabContainer)this.WebPage.Form.Controls[i]).Controls[k]).Controls[j].Controls[l].ID != null)
                                            {
                                                arrayLists.Add(((TabPanel)((TabContainer)this.WebPage.Form.Controls[i]).Controls[k]).Controls[j].Controls[l]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (this.WebPage.Form.Controls[i].GetType() != typeof(ContentPlaceHolder))
                        {
                            arrayLists.Add(this.WebPage.Form.Controls[i]);
                        }
                        else
                        {
                            for (int m = 0; m <= ((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls.Count - 1; m++)
                            {
                                if (((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m].ID != null)
                                {
                                    if (((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m].GetType() == typeof(Panel))
                                    {
                                        for (j = 0; j <= ((Panel)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls.Count - 1; j++)
                                        {
                                            if (((Panel)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[j].ID != null)
                                            {
                                                arrayLists.Add(((Panel)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[j]);
                                            }
                                        }
                                    }
                                    else if (((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m].GetType() != typeof(TabContainer))
                                    {
                                        arrayLists.Add(((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]);
                                    }
                                    else
                                    {
                                        for (int n = 0; n <= ((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls.Count - 1; n++)
                                        {
                                            if (((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n].ID != null)
                                            {
                                                if (((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n].GetType() == typeof(TabPanel))
                                                {
                                                    for (int o = 0; o <= ((TabPanel)((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n]).Controls.Count - 1; o++)
                                                    {
                                                        for (int p = 0; p <= ((TabPanel)((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n]).Controls[o].Controls.Count - 1; p++)
                                                        {
                                                            if (((TabPanel)((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n]).Controls[o].Controls[p].ID != null)
                                                            {
                                                                arrayLists.Add(((TabPanel)((TabContainer)((ContentPlaceHolder)this.WebPage.Form.Controls[i]).Controls[m]).Controls[n]).Controls[o].Controls[p]);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                XmlDocument xmlDocument = new XmlDocument();
                if (this.WebPage.Application[this.Xml_File] == null)
                {
                    xmlDocument.Load(string.Concat(AppDomain.CurrentDomain.BaseDirectory.ToString(), "XML/", this.Xml_File));
                    this.WebPage.Application[this.Xml_File] = xmlDocument;
                }
                else
                {
                    xmlDocument = (XmlDocument)this.WebPage.Application[this.Xml_File];
                }
                XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("COLLECTION/CONTROL");
                XmlNode[] itemOf = new XmlNode[xmlNodeLists.Count];
                bool flag = false;
                for (int q = 0; q < xmlNodeLists.Count; q++)
                {
                    itemOf[q] = xmlNodeLists[q];
                    flag = false;
                    for (i = 0; i <= arrayLists.Count - 1; i++)
                    {
                        this.sqlCommand = new SqlCommand()
                        {
                            CommandText = xmlDocument.SelectSingleNode("COLLECTION/NOTESOPERATION").Attributes["Name"].Value.ToString(),
                            CommandType = CommandType.StoredProcedure,
                            Connection = this.sqlConnection
                        };
                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_CODE", xmlDocument.SelectSingleNode("COLLECTION").Attributes["NOTES_CODE"].Value.ToString());
                        this.sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", this.Case_ID);
                        this.sqlCommand.Parameters.AddWithValue("@SZ_USER_ID", this.User_ID);
                        this.sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", this.Company_ID);
                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_TYPE", xmlDocument.SelectSingleNode("COLLECTION").Attributes["NOTES_TYPE"].Value.ToString());
                        this.sqlCommand.Parameters.AddWithValue("@FLAG", "ADD");
                        if (((Control)arrayLists[i]).ID.ToString() == itemOf[q].Attributes["ID"].Value.ToString())
                        {
                            if (itemOf[q].Attributes["Type"].Value.ToString() == "TextBox")
                            {
                                if (((TextBox)arrayLists[i]).Text != "")
                                {
                                    if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), ((TextBox)arrayLists[i]).Text);
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                    {
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).Add(itemOf[q].Attributes["ID"].Value.ToString(), ((TextBox)arrayLists[i]).Text);
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(((TextBox)arrayLists[i]).Text))
                                    {
                                        value = new string[] { itemOf[q].Attributes["Title"].Value, " change ", ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString(), " to ", ((TextBox)arrayLists[i]).Text };
                                        str = string.Concat(value);
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = ((TextBox)arrayLists[i]).Text;
                                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                        this.sqlCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            else if (itemOf[q].Attributes["Type"].Value.ToString() == "DropDownList")
                            {
                                if (((DropDownList)arrayLists[i]).SelectedValue != "")
                                {
                                    if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                    {
                                        Hashtable hashtables = this._hashtableControlCollection;
                                        string str1 = itemOf[q].Attributes["ID"].Value.ToString();
                                        value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((DropDownList)arrayLists[i]).SelectedValue, "#", ((DropDownList)arrayLists[i]).SelectedItem.Text };
                                        hashtables.Add(str1, string.Concat(value));
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                    {
                                        Hashtable item = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                        string str2 = itemOf[q].Attributes["ID"].Value.ToString();
                                        value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((DropDownList)arrayLists[i]).SelectedValue, "#", ((DropDownList)arrayLists[i]).SelectedItem.Text };
                                        item.Add(str2, string.Concat(value));
                                    }
                                    else
                                    {
                                        Hashtable item1 = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                        value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((DropDownList)arrayLists[i]).SelectedValue, "#", ((DropDownList)arrayLists[i]).SelectedItem.Text };
                                        if (!item1.ContainsValue(string.Concat(value)))
                                        {
                                            string str3 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                            chrArray = new char[] { '#' };
                                            strArrays = str3.Split(chrArray);
                                            str = "";
                                            if ((strArrays.GetValue(1).ToString() == "0" ? true : !(strArrays.GetValue(1).ToString() != "")))
                                            {
                                                str = string.Concat(itemOf[q].Attributes["Title"].Value, " change to ", ((DropDownList)arrayLists[i]).SelectedItem.Text);
                                            }
                                            else
                                            {
                                                value = new string[] { itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(2).ToString(), " to ", ((DropDownList)arrayLists[i]).SelectedItem.Text };
                                                str = string.Concat(value);
                                            }
                                            Hashtable hashtables1 = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                            string value1 = itemOf[q].Attributes["ID"].Value;
                                            value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((DropDownList)arrayLists[i]).SelectedValue, "#", ((DropDownList)arrayLists[i]).SelectedItem.Text };
                                            hashtables1[value1] = string.Concat(value);
                                            this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                            this.sqlCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else if (itemOf[q].Attributes["Type"].Value.ToString() == "RadioButtonList")
                            {
                                if (((RadioButtonList)arrayLists[i]).SelectedValue != "")
                                {
                                    if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((RadioButtonList)arrayLists[i]).SelectedValue, "#", ((RadioButtonList)arrayLists[i]).SelectedItem.Text));
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                    {
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((RadioButtonList)arrayLists[i]).SelectedValue, "#", ((RadioButtonList)arrayLists[i]).SelectedItem.Text));
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((RadioButtonList)arrayLists[i]).SelectedValue, "#", ((RadioButtonList)arrayLists[i]).SelectedItem.Text)))
                                    {
                                        string str4 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                        chrArray = new char[] { '#' };
                                        strArrays = str4.Split(chrArray);
                                        value = new string[] { itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to ", ((RadioButtonList)arrayLists[i]).SelectedItem.Text };
                                        str = string.Concat(value);
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((RadioButtonList)arrayLists[i]).SelectedValue, "#", ((RadioButtonList)arrayLists[i]).SelectedItem.Text);
                                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                        this.sqlCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            else if (itemOf[q].Attributes["Type"].Value.ToString() == "ListBox")
                            {
                                if (((ListBox)arrayLists[i]).SelectedValue != "")
                                {
                                    if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((ListBox)arrayLists[i]).SelectedValue, "#", ((ListBox)arrayLists[i]).SelectedItem.Text));
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                    {
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((ListBox)arrayLists[i]).SelectedValue, "#", ((ListBox)arrayLists[i]).SelectedItem.Text));
                                    }
                                    else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((ListBox)arrayLists[i]).SelectedValue, "#", ((ListBox)arrayLists[i]).SelectedItem.Text)))
                                    {
                                        string str5 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                        chrArray = new char[] { '#' };
                                        strArrays = str5.Split(chrArray);
                                        value = new string[] { itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to ", ((ListBox)arrayLists[i]).SelectedItem.Text };
                                        str = string.Concat(value);
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), ((ListBox)arrayLists[i]).SelectedValue, "#", ((ListBox)arrayLists[i]).SelectedItem.Text);
                                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                        this.sqlCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            else if (itemOf[q].Attributes["Type"].Value.ToString() == "CheckBox")
                            {
                                if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                {
                                    if (!((CheckBox)arrayLists[i]).Checked)
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected"));
                                    }
                                    else
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected"));
                                    }
                                }
                                else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                {
                                    if (!((CheckBox)arrayLists[i]).Checked)
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected"));
                                    }
                                    else
                                    {
                                        this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected"));
                                    }
                                }
                                else if (((CheckBox)arrayLists[i]).Checked)
                                {
                                    if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected")))
                                    {
                                        string str6 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                        chrArray = new char[] { '#' };
                                        strArrays = str6.Split(chrArray);
                                        str = string.Concat(itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to Selected");
                                        ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected");
                                        this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                        this.sqlCommand.ExecuteNonQuery();
                                    }
                                }
                                else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected")))
                                {
                                    string str7 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                    chrArray = new char[] { '#' };
                                    strArrays = str7.Split(chrArray);
                                    str = string.Concat(itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to Unselected");
                                    ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected");
                                    this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                    this.sqlCommand.ExecuteNonQuery();
                                }
                            }
                            else if (!(itemOf[q].Attributes["Type"].Value.ToString() == "RadioButton"))
                            {
                                if (itemOf[q].Attributes["Type"].Value.ToString() == "ExtendedDropDownList")
                                {
                                    if (((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Text != "NA")
                                    {
                                        if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                                        {
                                            Hashtable hashtables2 = this._hashtableControlCollection;
                                            string str8 = itemOf[q].Attributes["ID"].Value.ToString();
                                            value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Text, "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text };
                                            hashtables2.Add(str8, string.Concat(value));
                                        }
                                        else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                                        {
                                            Hashtable item2 = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                            string str9 = itemOf[q].Attributes["ID"].Value.ToString();
                                            value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Text, "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text };
                                            item2.Add(str9, string.Concat(value));
                                        }
                                        else
                                        {
                                            Hashtable item3 = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                            value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Text, "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text };
                                            if (!item3.ContainsValue(string.Concat(value)))
                                            {
                                                string str10 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                                chrArray = new char[] { '#' };
                                                strArrays = str10.Split(chrArray);
                                                str = "";
                                                if ((strArrays.GetValue(1).ToString() == "NA" ? true : !(strArrays.GetValue(1).ToString() != "")))
                                                {
                                                    str = string.Concat(itemOf[q].Attributes["Title"].Value, " change to ", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text);
                                                }
                                                else
                                                {
                                                    value = new string[] { itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(2).ToString(), " to ", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text };
                                                    str = string.Concat(value);
                                                }
                                                Hashtable hashtables3 = (Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"];
                                                string value2 = itemOf[q].Attributes["ID"].Value;
                                                value = new string[] { itemOf[q].Attributes["ID"].Value.ToString(), "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Text, "#", ((ExtendedDropDownList.ExtendedDropDownList)arrayLists[i]).Selected_Text };
                                                hashtables3[value2] = string.Concat(value);
                                                this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                                this.sqlCommand.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                                else if (itemOf[q].Attributes["Type"].Value.ToString() == "CheckBoxList")
                                {
                                }
                            }
                            else if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                            {
                                if (!((RadioButton)arrayLists[i]).Checked)
                                {
                                    this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected"));
                                }
                                else
                                {
                                    this._hashtableControlCollection.Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected"));
                                }
                            }
                            else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsKey(itemOf[q].Attributes["ID"].Value.ToString()))
                            {
                                if (!((RadioButton)arrayLists[i]).Checked)
                                {
                                    ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected"));
                                }
                                else
                                {
                                    ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).Add(itemOf[q].Attributes["ID"].Value.ToString(), string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected"));
                                }
                            }
                            else if (((RadioButton)arrayLists[i]).Checked)
                            {
                                if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected")))
                                {
                                    string str11 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                    chrArray = new char[] { '#' };
                                    strArrays = str11.Split(chrArray);
                                    str = string.Concat(itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to Selected");
                                    ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "1#Selected");
                                    this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                    this.sqlCommand.ExecuteNonQuery();
                                }
                            }
                            else if (!((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"]).ContainsValue(string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected")))
                            {
                                string str12 = ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value].ToString();
                                chrArray = new char[] { '#' };
                                strArrays = str12.Split(chrArray);
                                str = string.Concat(itemOf[q].Attributes["Title"].Value, " change ", strArrays.GetValue(1).ToString(), " to Unselected");
                                ((Hashtable)this.WebPage.Session["CONTROL_OLD_VALUES"])[itemOf[q].Attributes["ID"].Value] = string.Concat(itemOf[q].Attributes["ID"].Value.ToString(), "0#Unselected");
                                this.sqlCommand.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", str);
                                this.sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                if (this.WebPage.Session["CONTROL_OLD_VALUES"] == null)
                {
                    this.WebPage.Session["CONTROL_OLD_VALUES"] = this._hashtableControlCollection;
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}