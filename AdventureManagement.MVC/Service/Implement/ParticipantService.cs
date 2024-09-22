using AdventureManagement.MVC.Models;
using AdventureManagement.MVC.Service.Interface;
using Newtonsoft.Json;
using System.Text;

namespace AdventureManagement.MVC.Service.Implement
{
    public class ParticipantService : IParticipantService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://localhost:7191/api/participants";

        public ParticipantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ParticipantVM>> GetAllParticipantsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ParticipantVM>>(json);
                }
                else
                {
                    throw new Exception("Không thể lấy danh sách người tham gia từ API.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ParticipantDetailVM> GetParticipantByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ParticipantDetailVM>(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Người tham gia không tồn tại.");
            }
            else
            {
                throw new Exception("Có lỗi khi lấy thông tin người tham gia.");
            }
        }

        public async Task<bool> CreateParticipantAsync(ParticipantCreateVM participant)
        {
            var content = new StringContent(JsonConvert.SerializeObject(participant), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Dữ liệu đầu vào không hợp lệ.");
            }
            else
            {
                throw new Exception("Có lỗi khi tạo mới người tham gia.");
            }
        }

        public async Task<bool> UpdateParticipantAsync(int id, ParticipantUpdateVM participant)
        {
            var content = new StringContent(JsonConvert.SerializeObject(participant), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Người tham gia không tồn tại.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Dữ liệu đầu vào không hợp lệ.");
            }
            else
            {
                throw new Exception("Có lỗi khi cập nhật người tham gia.");
            }
        }

        public async Task<bool> DeleteParticipantAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Người tham gia không tồn tại.");
            }
            else
            {
                throw new Exception("Có lỗi khi xóa người tham gia.");
            }
        }
    }
}
