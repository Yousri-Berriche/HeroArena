using System;
using Microsoft.Data.SqlClient;

namespace HeroArena.ViewModels
{
    public class LoginVMX
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;";

        public bool VerifyLogin(string username, string password)
        {
            try
            {
                string passwordHash = HashPassword(password);

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Login WHERE Username = @username AND PasswordHash = @hash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@hash", passwordHash);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Erreur: " + ex.Message);
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
    }
}