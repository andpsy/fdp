using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FDP
{
    static class Language
    {
        static Dictionary<string, Dictionary<string, string>> language_dictionary = new Dictionary<string, Dictionary<string,string>>();
        public static Dictionary<string, Dictionary<string, string>> LanguageDictionary
        {
            get {return language_dictionary;}
            set {language_dictionary = value;}
        }

        public static Dictionary<string, string> Labels
        {
            get { return LanguageDictionary["Labels"]; }
        }

        public static Dictionary<string, string> MessageBoxes
        {
            get { return LanguageDictionary["MessageBoxes"]; }
        }

        public static Dictionary<string, string> ErrorMessages
        {
            get { return LanguageDictionary["ErrorMessages"]; }
        }

        public static Dictionary<string, string> ColumnHeaders
        {
            get { return LanguageDictionary["ColumnHeaders"]; }
        }

        public static Dictionary<string, Dictionary<string, string>> Populate(string language_file)
        {
            try
            {
                DataSet language = new DataSet("language");
                language.ReadXml(language_file);
                foreach (DataTable dt in language.Tables)
                {
                    Dictionary<string, string> language_section = new Dictionary<string, string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        language_section.Add(dr[0].ToString(), dr[1].ToString());
                    }
                    language_dictionary.Add(dt.TableName, language_section);
                }
                return language_dictionary;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void LoadLabels(System.Windows.Forms.Form form)
        {
            try
            {
                form.Text = GetLabelText(form.Name, form.Text);
                form.Text = form.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
            }
            catch { }

            foreach (System.Windows.Forms.Control ctrl in form.Controls)
            {
                try
                {
                    switch (ctrl.GetType().Name)
                    {
                        case "NaviBar":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBand ti in ((Guifreaks.NavigationBar.NaviBar)ctrl).Bands)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "NaviBand":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBandClientArea ti in ((Guifreaks.NavigationBar.NaviBand)ctrl).Controls)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "NaviBandClientArea":
                            try
                            {
                                foreach (System.Windows.Forms.Control ti in ((Guifreaks.NavigationBar.NaviBandClientArea)ctrl).Controls)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "ToolStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripItem ti in ((System.Windows.Forms.ToolStrip)ctrl).Items)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "MenuStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripMenuItem ti in ((System.Windows.Forms.MenuStrip)ctrl).Items)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "StatusStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripStatusLabel ti in ((System.Windows.Forms.StatusStrip)ctrl).Items)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        default:
                            try
                            {
                                try
                                {
                                    //ctrl.Text = LanguageDictionary["Labels"][String.Format("{0}.{1}", form.Name, ctrl.Name)];
                                    ctrl.Text = GetLabelText(String.Format("{0}.{1}", form.Name, ctrl.Name), ctrl.Text);
                                    ctrl.Text = ctrl.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                                catch { }
                            }
                            catch { }
                            break;
                    }
                    //LoadLabels(form.Name, ctrl);
                }
                catch { }
                LoadLabels(form.Name, ctrl);
            }
        }

        public static void LoadLabels(string form_name, System.Windows.Forms.Control ctrl)
        {
            foreach (System.Windows.Forms.Control child_ctrl in ctrl.Controls)
            {
                /*
                try
                {
                    //child_ctrl.Text = LanguageDictionary["Labels"][String.Format("{0}.{1}", form_name, child_ctrl.Name)];
                    child_ctrl.Text = GetLabelText(String.Format("{0}.{1}", form_name, child_ctrl.Name), child_ctrl.Text);
                }
                catch { }
                LoadLabels(form_name, child_ctrl);
                */
                try
                {
                    switch (child_ctrl.GetType().Name)
                    {
                        case "NaviBar":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBand ti in ((Guifreaks.NavigationBar.NaviBar)child_ctrl).Bands)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "NaviBand":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBandClientArea ti in ((Guifreaks.NavigationBar.NaviBand)child_ctrl).Controls)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "NaviBandClientArea":
                            try
                            {
                                foreach (System.Windows.Forms.Control ti in ((Guifreaks.NavigationBar.NaviBandClientArea)child_ctrl).Controls)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "ToolStrip": 
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripItem ti in ((System.Windows.Forms.ToolStrip)child_ctrl).Items)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        case "MenuStrip":
                            try
                            {
                                //foreach (System.Windows.Forms.ToolStripMenuItem ti in ((System.Windows.Forms.MenuStrip)child_ctrl).Items)
                                foreach (System.Windows.Forms.ToolStripMenuItem ti in CommonFunctions.GetItems((System.Windows.Forms.MenuStrip)child_ctrl))
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch (Exception exp) { exp.ToString(); }
                            break;
                        case "StatusStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripStatusLabel ti in ((System.Windows.Forms.StatusStrip)child_ctrl).Items)
                                {
                                    ti.Text = GetLabelText(String.Format("{0}.{1}", form_name, ti.Name), ti.Text);
                                    ti.Text = ti.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                                }
                            }
                            catch { }
                            break;
                        default:
                            try
                            {
                                //ctrl.Text = LanguageDictionary["Labels"][String.Format("{0}.{1}", form.Name, ctrl.Name)];
                                child_ctrl.Text = GetLabelText(String.Format("{0}.{1}", form_name, child_ctrl.Name), child_ctrl.Text);
                                child_ctrl.Text = child_ctrl.Text.Replace("<<Company>>", SettingsClass.GetCompanySetting("ALIAS"));
                            }
                            catch { }
                            break;
                    }
                    //LoadLabels(form_name, child_ctrl);
                }
                catch { }
                LoadLabels(form_name, child_ctrl);
            }

        }

        public static string GetMessageBoxText(string id, string text_if_null)
        {
            try
            {
                return MessageBoxes[id] == null ? text_if_null : MessageBoxes[id];
            }
            catch { return text_if_null; }
        }

        public static string GetLabelText(string id, string text_if_null)
        {
            try
            {
                return Labels[id] == null ? text_if_null : Labels[id];
            }
            catch { return text_if_null; }
        }

        public static string GetErrorText(string id, string text_if_null)
        {
            try
            {
                return ErrorMessages[id] == null ? text_if_null : ErrorMessages[id];
            }
            catch { return text_if_null; }
        }

        public static string GetColumnHeaderText(string id, string text_if_null)
        {
            try
            {
                return ColumnHeaders[id] == null ? text_if_null.Replace("_", " ").ToUpper() : ColumnHeaders[id];
            }
            catch { return text_if_null.Replace("_", " ").ToUpper(); }
        }

        public static void PopulateGridColumnHeaders(System.Windows.Forms.DataGridView data_grid_view)
        {
            if(data_grid_view.DataSource is DataTable)
            {
                foreach (DataColumn dc in ((DataTable)data_grid_view.DataSource).Columns)
                {
                    try
                    {
                        data_grid_view.Columns[dc.ColumnName].HeaderText = Language.GetColumnHeaderText(dc.ColumnName.ToLower(), dc.ColumnName.ToUpper());
                    }
                    catch
                    {
                        data_grid_view.Columns[dc.ColumnName.ToLower().Replace("_id", "")].HeaderText = Language.GetColumnHeaderText(dc.ColumnName.ToLower().Replace("_id", ""), dc.ColumnName.ToUpper().Replace("_ID", ""));
                    }
                }
                return;
            }
            if (data_grid_view.DataSource is System.Windows.Forms.BindingSource)
            {
                try
                {
                    DataTable dt = (DataTable)((System.Windows.Forms.BindingSource)data_grid_view.DataSource).DataSource;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            data_grid_view.Columns[dc.ColumnName].HeaderText = Language.GetColumnHeaderText(dc.ColumnName.ToLower(), dc.ColumnName.ToUpper());
                        }
                        catch
                        {
                            data_grid_view.Columns[dc.ColumnName.ToLower().Replace("_id", "")].HeaderText = Language.GetColumnHeaderText(dc.ColumnName.ToLower().Replace("_id", ""), dc.ColumnName.ToUpper().Replace("_ID", ""));
                        }
                    }
                    return;
                }
                catch { }
            }
        }

    }
}
