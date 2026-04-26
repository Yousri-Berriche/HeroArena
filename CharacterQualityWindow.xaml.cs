using System.Collections.Generic;
using System.Windows;

namespace HeroArena
{
    public partial class CharacterQualityWindow : Window
    {
        private string characterName;
        private string characterClass;
        private int characterHP;
        private List<string> characterSpells;

        public CharacterQualityWindow(string name, string heroClass)
        {
            InitializeComponent();
            characterName = name;
            characterClass = heroClass;

            LoadCharacterData();
        }

        private void LoadCharacterData()
        {
            // Charger les stats selon la classe
            if (characterClass == "Warrior")
            {
                characterHP = 150;
                characterSpells = new List<string> { "Slash (20)", "Shield Bash (15)", "Fireball (30)", "Backstab (35)" };
            }
            else if (characterClass == "Mage")
            {
                characterHP = 100;
                characterSpells = new List<string> { "Fireball (30)", "Ice Storm (25)", "Shadow Step (40)", "Slash (20)" };
            }
            else if (characterClass == "Assassin")
            {
                characterHP = 120;
                characterSpells = new List<string> { "Backstab (35)", "Shadow Step (40)", "Slash (20)", "Ice Storm (25)" };
            }

            CharacterNameText!.Text = characterName;
            CharacterClassText!.Text = characterClass;
            CharacterHPText!.Text = characterHP + " HP";

            SpellListBox!.Items.Clear();
            foreach (var spell in characterSpells)
            {
                SpellListBox!.Items.Add(spell);
            }
        }

        private void StartAdventureButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bienvenue " + characterName + "! Ton aventure commence!");
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }
    }
}