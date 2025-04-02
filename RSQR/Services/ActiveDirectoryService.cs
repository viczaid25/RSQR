using System.DirectoryServices;

namespace RSQR.Services
{
    public class ActiveDirectoryService
    {
        private readonly string _domain;

        public ActiveDirectoryService(string domain)
        {
            _domain = domain;
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                using (var entry = new DirectoryEntry($"LDAP://{_domain}", username, password))
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = $"(sAMAccountName={username})";
                        searcher.PropertiesToLoad.Add("displayName"); // Campo que quieres traer


                        var result = searcher.FindOne();

                        // Si se encuentra el usuario y las credenciales son válidas, retorna true
                        return result != null;
                    }
                }
            }
            catch
            {
                // Si hay un error (credenciales incorrectas, etc.), retorna false
                return false;
            }
        }

        public string GetDisplayName(string username, string password)
        {
            try
            {
                using (var entry = new DirectoryEntry($"LDAP://{_domain}", username, password))
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = $"(sAMAccountName={username})";
                        searcher.PropertiesToLoad.Add("displayName");

                        var result = searcher.FindOne();

                        if (result != null && result.Properties["displayName"].Count > 0)
                        {
                            return result.Properties["displayName"][0].ToString();
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }

    }
}