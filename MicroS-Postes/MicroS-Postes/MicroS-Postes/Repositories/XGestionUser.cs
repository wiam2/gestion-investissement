namespace MicroS_Postes.Repositories
{
    public class XGestionUser
    {
        private readonly HttpClient _httpClient;

        public XGestionUser(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> XGetInvestisseurById(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/XINV/affichageInvestisseur/{id}");
            response.EnsureSuccessStatusCode(); // Throw exception on non-success status codes
            var investisseurDTO = await response.Content.ReadAsAsync<dynamic>();
            return investisseurDTO;
        }
    }
}
