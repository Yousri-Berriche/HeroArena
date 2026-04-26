using System.Windows;
using System.Windows.Controls;

namespace HeroArena
{
    public partial class BattleWindow : Window
    {
        private string playerHero;
        private int playerHP;
        private int enemyHP;
        private string enemyHero;

        public BattleWindow(string hero, int hp)
        {
            InitializeComponent();
            playerHero = hero;
            playerHP = hp;
            enemyHP = hp;
            enemyHero = "Enemy " + hero;

            InitializeBattle();
        }

        private void InitializeBattle()
        {
            PlayerHeroName!.Text = playerHero;
            PlayerHPText!.Text = playerHP + "/" + playerHP + " HP";
            PlayerHPBar!.Maximum = playerHP;
            PlayerHPBar!.Value = playerHP;

            EnemyHeroName!.Text = enemyHero;
            EnemyHPText!.Text = enemyHP + "/" + enemyHP + " HP";
            EnemyHPBar!.Maximum = enemyHP;
            EnemyHPBar!.Value = enemyHP;

            Spell1Button!.Content = "Slash (20)";
            Spell2Button!.Content = "Fireball (30)";
            Spell3Button!.Content = "Ice Storm (25)";
            Spell4Button!.Content = "Shadow (40)";

            // Afficher les sorts du héros
            Spell1Button!.Content = "Slash (20)";
            Spell2Button!.Content = "Fireball (30)";
            Spell3Button!.Content = "Ice Storm (25)";
            Spell4Button!.Content = "Shadow (40)";
        }

        private void Spell_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int damage = int.Parse(btn.Content.ToString().Split('(')[1].Split(')')[0]);

            // Dégâts sur l'ennemi
            enemyHP -= damage;
            EnemyHPText.Text = enemyHP + "/" + (int)EnemyHPBar.Maximum + " HP";
            EnemyHPBar.Value = enemyHP;
            BattleLogText.Text = "Tu as infligé " + damage + " dégâts!";

            if (enemyHP <= 0)
            {
                MessageBox.Show("Victoire! Tu as vaincu " + enemyHero + "!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                return;
            }

            // Contre-attaque ennemi
            int enemyDamage = 15;
            playerHP -= enemyDamage;
            PlayerHPText.Text = playerHP + "/" + (int)PlayerHPBar.Maximum + " HP";
            PlayerHPBar.Value = playerHP;
            BattleLogText.Text += "\n" + enemyHero + " t'a infligé " + enemyDamage + " dégâts!";

            if (playerHP <= 0)
            {
                MessageBox.Show("Défaite! Tu as été vaincu par " + enemyHero + "!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}