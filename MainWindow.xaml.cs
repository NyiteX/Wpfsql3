using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Wpfsql3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string str = @"Data Source = WIN-U669V8L9R5E; Initial Catalog = Rozetka; Trusted_Connection=True";
        string login_string = "";
        string pass_string = "";

        SqlDataAdapter adapter;
        public MainWindow()
        {
            InitializeComponent();
            t_b1.Text = "";
            label_info.Content = "";
        }


        async void Update_ba()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection())
                {
                    await connection.OpenAsync();
                    SqlCommand command= connection.CreateCommand();
                    command.CommandText = "INSERT INTO ";
                    command.Connection= connection;

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("dobavleno");
                    t_b1.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void Load_info_content(SqlDataReader reader)
        {
            label_info.Content = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                label_info.Content += reader.GetName(i) + "\t";
            }            
        }


        async void Load_tables()
        {
            if (admin_update.Visibility == Visibility.Visible)
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        if (reader.FieldCount > 0)
                        {
                            list_b1.Items.Clear();
                            t_b1.Text = "";
                            Load_info_content(reader);

                            while (await reader.ReadAsync())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    list_b1.Items.Add(reader.GetValue(i) + "\t");
                                }
                            }
                        }
                        //await reader.CloseAsync();
                    }
                    else
                        MessageBox.Show("Список пуст");
                }
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(str))
                    {
                        adapter = new SqlDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", connection);

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);

                        DataTable dt = dataset.Tables[0];

                        label_info.Content = "";

                        foreach (DataColumn column in dt.Columns)
                        {
                            label_info.Content += $"{column.ColumnName}\t";
                            foreach (DataRow row in dt.Rows)
                            {
                                var cells = row.ItemArray;
                                foreach (var cell in cells)
                                list_b1.Items.Add($"{cell}\t");
                            }
                        }
                    }
                }catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        async void Load()
        {
            try
            {
                if (admin_update.Visibility == Visibility.Visible)
                {
                    using (SqlConnection connection = new SqlConnection(str))
                    {
                        await connection.OpenAsync();

                        SqlCommand command = new SqlCommand("SELECT * FROM " + list_b1.SelectedItem, connection);
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            if (reader.FieldCount > 0)
                            {
                                list_b1.Items.Clear();
                                t_b1.Text = "";
                                Load_info_content(reader);

                                while (await reader.ReadAsync())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        t_b1.Text += reader.GetValue(i) + "\t";
                                    }
                                    t_b1.Text += "\n";
                                    list_b1.Items.Add(t_b1.Text);
                                    t_b1.Text = "";
                                }
                            }
                            //await reader.CloseAsync();
                        }
                        else
                            MessageBox.Show("Список пуст");
                    }
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(str))
                    {
                        adapter = new SqlDataAdapter("SELECT * FROM " + list_b1.SelectedItem, connection);

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);

                        DataTable dt = dataset.Tables[0];
                        
                        label_info.Content = "";
                        list_b1.Items.Clear();

                        foreach (DataColumn column in dt.Columns)
                        {
                            label_info.Content += $"{column.ColumnName}\t";
                            foreach (DataRow row in dt.Rows)
                            {
                                string tmp = "";
                                var cells = row.ItemArray;
                                foreach (var cell in cells)
                                    tmp += $"{cell}\t";
                                list_b1.Items.Add(tmp);
                            }
                        }
                    }
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 form = new Window1();

            form.ShowDialog();

            if(form.Login_tb.Text != "Login" || form.Pass_tb.Text != "Password")
            {
                login_string = form.Login_tb.Text;
                pass_string = form.Pass_tb.Text;
                if(login_string.Count() > 0 && pass_string.Length > 0)
                {
                    User1.Visibility = Visibility.Hidden;
                    Admin1.Visibility = Visibility.Hidden;
                    btn_Back.Visibility = Visibility.Visible;
                    user_readALL.Visibility = Visibility.Visible;
                    user_search.Visibility = Visibility.Visible;
                    //t_b1.Visibility = Visibility.Visible;
                    list_b1.Visibility = Visibility.Visible;
                    label_info.Visibility = Visibility.Visible;
                    user_read.Visibility = Visibility.Visible;
                    cls_btn.Visibility = Visibility.Visible;
                    login_string = "";
                    pass_string = "";
                }
            }            
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            User1.Visibility = Visibility.Visible;
            Admin1.Visibility = Visibility.Visible;
            btn_Back.Visibility = Visibility.Hidden;
            admin_delete.Visibility = Visibility.Hidden;
            user_readALL.Visibility = Visibility.Hidden;
            user_search.Visibility = Visibility.Hidden;
            admin_update.Visibility = Visibility.Hidden;
            //t_b1.Visibility = Visibility.Hidden;
            list_b1.Visibility= Visibility.Hidden;
            label_info.Visibility = Visibility.Hidden;
            user_read.Visibility = Visibility.Hidden;
            cls_btn.Visibility = Visibility.Hidden;
            label_info.Content = "";
            list_b1.Items.Clear();          
        }
        private void Button_Click_admin(object sender, RoutedEventArgs e)
        {
            Window1 form = new Window1();

            form.ShowDialog();

            if (form.Login_tb.Text != "Login" || form.Pass_tb.Text != "Password")
            {
                login_string = form.Login_tb.Text;
                pass_string = form.Pass_tb.Text;
                if (login_string.Count() > 0 && pass_string.Length > 0)
                {
                    User1.Visibility = Visibility.Hidden;
                    Admin1.Visibility = Visibility.Hidden;
                    btn_Back.Visibility = Visibility.Visible;
                    admin_delete.Visibility = Visibility.Visible;
                    user_readALL.Visibility = Visibility.Visible;
                    user_search.Visibility = Visibility.Visible;
                    admin_update.Visibility = Visibility.Visible;
                    //t_b1.Visibility = Visibility.Visible;
                    list_b1.Visibility = Visibility.Visible;
                    label_info.Visibility = Visibility.Visible;
                    user_read.Visibility = Visibility.Visible;
                    cls_btn.Visibility = Visibility.Visible;
                    login_string = "";
                    pass_string = "";
                }
            }
        }
        private void admin_readALL_Click(object sender, RoutedEventArgs e)
        {
            Load_tables();
        }
        private void admin_read_Click(object sender, RoutedEventArgs e)
        {
            if (label_info.Content.ToString().Count() > 0 && list_b1.SelectedIndex!= -1)
                Load();
        }
        private void cls_btn_Click(object sender, RoutedEventArgs e)
        {
            label_info.Content = "";
            list_b1.Items.Clear();
        }
    }
}
