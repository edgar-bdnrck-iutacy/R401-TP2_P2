using ClientSeries.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientSeries.Services;

public class WSService : IService
{
    private readonly HttpClient _http;

    public WSService()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://apiservicev8-btc8ajawd9bme4gm.switzerlandnorth-01.azurewebsites.net/")
        };
    }

    // OPTION: pour tests, injection HttpClient
    public WSService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<Serie>?> GetAllSeriesAsync()
    {
        return await _http.GetFromJsonAsync<List<Serie>>("api/series");
    }

    public async Task<Serie?> GetSerieAsync(int idSerie)
    {
        var url = string.Concat("api/series/", idSerie);
        return await _http.GetFromJsonAsync<Serie>(url);
    }

    public async Task<bool> PostSerieAsync(Serie serie)
    {
        using var resp = await _http.PostAsJsonAsync("api/series", serie);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> PutSerieAsync(int idSerie, Serie serie)
    {
        var url = string.Concat("api/series/", idSerie);
        using var resp = await _http.PutAsJsonAsync(url, serie);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSerieAsync(int idSerie)
    {
        var url = string.Concat("api/series/", idSerie);
        using var resp = await _http.DeleteAsync(url);
        return resp.IsSuccessStatusCode;
    }
}
