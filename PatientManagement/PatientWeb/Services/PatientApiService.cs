using PatientShared.DTOs;

namespace PatientWeb.Services
{
    /// <summary>
    /// Provides HTTP operations for retrieving and creating patient records through the patients API.
    /// </summary>
    /// <remarks>Uses HttpClient to call the api/patients endpoints and deserialize JSON payloads to
    /// PatientDTO models.</remarks>
    public class PatientApiService
    {
        private readonly HttpClient _httpClient;

        public PatientApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PatientDTO>> GetPatientsAsync()
        {
            var patients= await _httpClient.GetFromJsonAsync<List<PatientDTO>>("api/patients");
            return patients ?? new List<PatientDTO>();
        }

        public async Task<PatientDTO> GetPatientAsync(int id)
        {
            var patient = await _httpClient.GetFromJsonAsync<PatientDTO>($"api/patients/{id}");
            return patient ?? new PatientDTO();
        }

        public async Task AddPatient(PatientDTO patient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/patients", patient);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePatient(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/patients/{id}");
            response.EnsureSuccessStatusCode();
        }


    }
}
