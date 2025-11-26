using System.Data;
using System.Data.SqlClient;


namespace Lab_9_a
{
    public partial class Form1 : Form
    {
        private readonly string connString = (
                                            @"Data Source=SECRETIVE-PLOTT\SQLEXPRESS;
                                                Initial Catalog=StudentDB;
                                                Integrated Security=True;");



        private int? selectedRegId = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Items.Add("Male");
                    comboBox1.Items.Add("Female");
                }

                if (comboBox2.Items.Count == 0)
                {
                    comboBox2.Items.Add("BSCS");
                    comboBox2.Items.Add("BBA");
                    comboBox2.Items.Add("BSSE");
                    comboBox2.Items.Add("BSIT");
                    comboBox2.Items.Add("MCS");
                }

                dateTimePicker1.MaxDate = DateTime.Today;
                dateTimePicker1.Value = DateTime.Today.AddYears(-18);

                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during form load: " + ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                   
                    if (row.Cells["RegId"].Value != null)
                    {
                        int id;
                        if (int.TryParse(row.Cells["RegId"].Value.ToString(), out id))
                        {
                            selectedRegId = id;
                            
                            textBox1.Text = row.Cells["Name"].Value?.ToString() ?? "";
                            textBox2.Text = row.Cells["FatherName"].Value?.ToString() ?? "";
                            textBox3.Text = row.Cells["CNIC"].Value?.ToString() ?? "";
                            comboBox1.SelectedItem = row.Cells["Gender"].Value?.ToString() ?? null;
                            DateTime dob;
                            if (DateTime.TryParse(row.Cells["DOB"].Value?.ToString(), out dob))
                                dateTimePicker1.Value = dob;
                            textBox4.Text = row.Cells["Address"].Value?.ToString() ?? "";
                            comboBox2.SelectedItem = row.Cells["DegreeProgram"].Value?.ToString() ?? null;
                            textBox5.Text = row.Cells["MatricGrade"].Value?.ToString() ?? "";
                            textBox6.Text = row.Cells["InterGrade"].Value?.ToString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting student: " + ex.Message);
            }
        }
        private void LoadStudents()
        {
            
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string sql = "SELECT RegId, Name, FatherName, CNIC, Gender, DOB, Address, DegreeProgram, MatricGrade, InterGrade FROM Students";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns["RegId"] != null)
                            dataGridView1.Columns["RegId"].ReadOnly = true;
                    }
                }

               
                selectedRegId = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }
        private bool ValidateInputs(out double matricGrade, out double interGrade)
        {
            matricGrade = 0;
            interGrade = 0;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Name is required.");
                textBox1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Father Name is required.");
                textBox2.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("CNIC is required.");
                textBox3.Focus();
                return false;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Gender.");
                comboBox1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Address is required.");
                textBox4.Focus();
                return false;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Select Degree Program.");
                comboBox2.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text) || !double.TryParse(textBox5.Text, out matricGrade))
            {
                MessageBox.Show("Matric Grade is required and must be a number.");
                textBox5.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox6.Text) || !double.TryParse(textBox6.Text, out interGrade))
            {
                MessageBox.Show("Inter Grade is required and must be a number.");
                textBox6.Focus();
                return false;
            }

            if (matricGrade < 0 || matricGrade > 110)
            {
                MessageBox.Show("Matric Grade seems invalid. Enter a realistic number.");
                textBox5.Focus();
                return false;
            }
            if (interGrade < 0 || interGrade > 110)
            {
                MessageBox.Show("Inter Grade seems invalid. Enter a realistic number.");
                textBox6.Focus();
                return false;
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
       
            try
            {
                double matric, inter;
                if (!ValidateInputs(out matric, out inter))
                    return;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string insertSql = @"
                        INSERT INTO Students
                        (Name, FatherName, CNIC, Gender, DOB, Address, DegreeProgram, MatricGrade, InterGrade)
                        VALUES
                        (@Name, @FatherName, @CNIC, @Gender, @DOB, @Address, @DegreeProgram, @MatricGrade, @InterGrade)";

                    using (SqlCommand cmd = new SqlCommand(insertSql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FatherName", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@CNIC", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@Address", textBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@DegreeProgram", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MatricGrade", matric);
                        cmd.Parameters.AddWithValue("@InterGrade", inter);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Student added successfully.");
                            LoadStudents();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("No rows inserted. Please check the data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!selectedRegId.HasValue)
                {
                    MessageBox.Show("Please select a student from the table to update (click a row).");
                    return;
                }

                double matric, inter;
                if (!ValidateInputs(out matric, out inter))
                    return;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    string updateSql = @"
                        UPDATE Students
                        SET Name = @Name,
                            FatherName = @FatherName,
                            CNIC = @CNIC,
                            Gender = @Gender,
                            DOB = @DOB,
                            Address = @Address,
                            DegreeProgram = @DegreeProgram,
                            MatricGrade = @MatricGrade,
                            InterGrade = @InterGrade
                        WHERE RegId = @RegId";

                    using (SqlCommand cmd = new SqlCommand(updateSql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FatherName", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@CNIC", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@Address", textBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@DegreeProgram", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MatricGrade", matric);
                        cmd.Parameters.AddWithValue("@InterGrade", inter);
                        cmd.Parameters.AddWithValue("@RegId", selectedRegId.Value);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Student record updated successfully.");
                            LoadStudents();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. The RegId may not exist anymore.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!selectedRegId.HasValue)
                {
                    MessageBox.Show("Please select a student from the table to delete (click a row).");
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure you want to delete student with RegId = {selectedRegId.Value} ?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string deleteSql = "DELETE FROM Students WHERE RegId = @RegId";
                    using (SqlCommand cmd = new SqlCommand(deleteSql, con))
                    {
                        cmd.Parameters.AddWithValue("@RegId", selectedRegId.Value);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Student deleted successfully.");
                            LoadStudents();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Delete failed. The record may not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message);
            }

        }
        private void ClearInputs()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            textBox4.Text = "";
            comboBox2.SelectedItem = null;
            textBox5.Text = "";
            textBox6.Text = "";
            selectedRegId = null;
        }
    }
}
