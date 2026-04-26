using System.Windows;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HeroArena
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, (int hp, List<string> spells)> heroes;
        private string selectedHero = "";

        public MainWindow()
        {
            InitializeComponent();
            heroes = new Dictionary<string, (int, List<string>)>();
            InitializeHeroes();
            LoadHeroList();
        }

        private bool VerifyConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;"))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void InitializeHeroes()
        {
            heroes = new Dictionary<string, (int, List<string>)>();

            try
            {
                using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;"))
                {
                    conn.Open();
                    string query = "SELECT Name, Health FROM Hero";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string heroName = reader["Name"].ToString() ?? "";
                                int health = (int)reader["Health"];
                                var spells = GetSpellsFromDB(heroName);
                                heroes[heroName] = (health, spells);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Erreur BDD: " + ex.Message);
            }
        }

        private List<string> GetSpellsFromDB(string heroName)
        {
            List<string> spells = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;"))
                {
                    conn.Open();
                    string query = @"
                        SELECT s.Name, s.Damage FROM Spell s
                        INNER JOIN HeroSpell hs ON s.ID = hs.SpellID
                        INNER JOIN Hero h ON hs.HeroID = h.ID
                        WHERE h.Name = @heroName
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@heroName", heroName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string spellName = reader["Name"].ToString() ?? "";
                                int damage = (int)reader["Damage"];
                                spells.Add($"{spellName} ({damage} dmg)");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Erreur BDD: " + ex.Message);
            }

            return spells;
        }

        private void LoadHeroList()
        {
            HeroListBox!.Items.Clear();
            foreach (var hero in heroes.Keys)
            {
                HeroListBox!.Items.Add(hero);
            }
        }

        private void HeroListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (HeroListBox!.SelectedItem != null)
            {
                selectedHero = HeroListBox!.SelectedItem.ToString() ?? "";
                if (heroes.ContainsKey(selectedHero))
                {
                    var (hp, spells) = heroes[selectedHero];
                    HeroNameText!.Text = selectedHero;
                    HeroHPText!.Text = hp + " HP";
                    SpellListBox!.Items.Clear();
                    foreach (var spell in spells)
                    {
                        SpellListBox!.Items.Add(spell);
                    }
                }
            }
        }

        private void StartBattle_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedHero))
            {
                System.Windows.MessageBox.Show("Choisis un héros d'abord!");
                return;
            }

            BattleWindow battleWindow = new BattleWindow(selectedHero, heroes[selectedHero].hp);
            battleWindow.Show();
            this.Close();
        }
    }
}