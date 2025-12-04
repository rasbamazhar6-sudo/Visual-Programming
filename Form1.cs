using lab_11.Data;
using lab_11.Models;

namespace lab_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using var context = new LibraryContext();
            context.Database.EnsureCreated();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new LibraryContext())
                {
                    if (!context.Authors.Any())
                    {
                        context.Authors.AddRange(
                            new Author { Name = "J.K. Rowling" },
                            new Author { Name = "George Orwell" },
                            new Author { Name = "Agatha Christie" }
                        );

                        context.Categories.AddRange(
                            new Category { CategoryName = "Fantasy" },
                            new Category { CategoryName = "Dystopian" },
                            new Category { CategoryName = "Mystery" }
                        );
                        context.SaveChanges();

                        context.Books.AddRange(
                            new Book { Title = "HP 1", AuthorId = 1, CategoryId = 1 },
                            new Book { Title = "1984", AuthorId = 2, CategoryId = 2 },
                            new Book { Title = "Murder on the Orient Express", AuthorId = 3, CategoryId = 3 },
                            new Book { Title = "HP 2", AuthorId = 1, CategoryId = 1 },
                            new Book { Title = "HP 3", AuthorId = 1, CategoryId = 1 }
                        );
                        context.SaveChanges();
                    }
                }

                label1.Text = "Sample data inserted successfully!";
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var context = new LibraryContext())
            {
                var result = from b in context.Books
                             join a in context.Authors
                             on b.AuthorId equals a.AuthorId
                             select new { b.Title, Author = a.Name };

                dataGridView1.DataSource = result.ToList();
                label1.Text = "Books with authors loaded.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var context = new LibraryContext())
            {
                var result = from b in context.Books
                             join c in context.Categories
                             on b.CategoryId equals c.CategoryId
                             select new { b.Title, Category = c.CategoryName };

                dataGridView1.DataSource = result.ToList();
                label1.Text = "Books with categories loaded.";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var context = new LibraryContext())
            {
                var result = from a in context.Authors
                             join b in context.Books
                             on a.AuthorId equals b.AuthorId into bookGroup
                             select new
                             {
                                 Author = a.Name,
                                 BookCount = bookGroup.Count()
                             };

                dataGridView1.DataSource = result.ToList();
                label1.Text = "Author book counts loaded.";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var context = new LibraryContext())
            {
                var result = from a in context.Authors
                             join b in context.Books
                             on a.AuthorId equals b.AuthorId into bookGroup
                             from bg in bookGroup.DefaultIfEmpty()
                             select new
                             {
                                 Author = a.Name,
                                 BookName = bg == null ? "No Book" : bg.Title
                             };

                dataGridView1.DataSource = result.ToList();
                label1.Text = "Left join result loaded.";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
