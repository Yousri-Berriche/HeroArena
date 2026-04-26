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

        private void InitializeHeroes()
        {
            heroes = new Dictionary<string, (int, List<string>)>
            {
                { "Warrior", (150, GetSpellsFromDB("Warrior")) },
                { "Mage", (100, GetSpellsFromDB("Mage")) },
                { "Assassin", (120, GetSpellsFromDB("Assassin")) }
            };
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
                MessageBox.Show("Erreur BDD: " + ex.Message);
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
                MessageBox.Show("Choisis un héros d'abord!");
                return;
            }

            BattleWindow battleWindow = new BattleWindow(selectedHero, heroes[selectedHero].hp);
            battleWindow.Show();
            this.Close();
        }
    }
}