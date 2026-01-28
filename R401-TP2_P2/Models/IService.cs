using ClientSeries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientSeries.Services;

public interface IService
{
    Task<List<Serie>?> GetAllSeriesAsync();
    Task<Serie?> GetSerieAsync(int idSerie);
    Task<bool> PostSerieAsync(Serie serie);
    Task<bool> PutSerieAsync(int idSerie, Serie serie);
    Task<bool> DeleteSerieAsync(int idSerie);
}
