using InizioTest.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace InizioTest.Services
{
    public class JsonSaver
    {
        private readonly IJSRuntime jSRuntime;

        public JsonSaver(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }


        public async Task SaveResultToJsonAsync(SearchResult searchResult)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonContent = JsonSerializer.Serialize(searchResult, options);

            string filePath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "searchResult.json");

            await File.WriteAllTextAsync(filePath, jsonContent);

            await DownloadFileAsync(filePath);

        }


        private async Task DownloadFileAsync(string filePath)
        {

            var fileContent = await File.ReadAllTextAsync(filePath);

            await jSRuntime.InvokeVoidAsync("downloadFile", "searchResult.json", "application/json", fileContent);
        }
    }
}
