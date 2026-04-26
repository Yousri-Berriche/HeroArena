using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace HeroArena
{
    public partial class CreateCharacterWindow : Window
    {
        public CreateCharacterWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string characterName = CharacterNameBox!.Text;
            string characterClass = (ClassComboBox!.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";

            if (string.IsNullOrEmpty(characterName) || string.IsNullOrEmpty(characterClass))
            {
                MessageBox!.Text = "Remplis tous les champs!";
                return;
            }

            // Sauvegarder en BDD
            if (SaveCharacterToDB(characterName, characterClass))
            {
                // Ouvrir CharacterQualityWindow
                CharacterQualityWindow qualityWindow = new CharacterQualityWindow(characterName, characterClass);
                qualityWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox!.Text = "Erreur lors de la création!";
            }
        }

        private bool SaveCharacterToDB(string name, string heroClass)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;"))
                {
                    conn.Open();

                    // Insérer le héros créé dans la table Hero
                    string query = "INSERT INTO Hero (Name, Health) VALUES (@name, @health)";

                    int health = heroClass == "Warrior" ? 150 : (heroClass == "Mage" ? 100 : 120);

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@health", health);
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Remplis tous les champs!");
                return false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}