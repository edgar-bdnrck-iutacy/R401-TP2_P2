using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClientSeries.Models;

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

    public WSService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<Serie>?> GetAllSeriesAsync()
        => await _http.GetFromJsonAsync<List<Serie>>("api/series");

    public async Task<Serie?> GetSerieAsync(int idSerie)
        => await _http.GetFromJsonAsync<Serie>(string.Concat("api/series/", idSerie));

    public async Task<bool> PostSerieAsync(Serie serie)
    {
        using var resp = await _http.PostAsJsonAsync("api/series", serie);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> PutSerieAsync(int idSerie, Serie serie)
    {
        using var resp = await _http.PutAsJsonAsync(string.Concat("api/series/", idSerie), serie);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSerieAsync(int idSerie)
    {
        using var resp = await _http.DeleteAsync(string.Concat("api/series/", idSerie));
        return resp.IsSuccessStatusCode;
    }
}
