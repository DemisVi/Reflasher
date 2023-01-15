using System.Text.Json;
using System.IO;

namespace Reflasher
{
    public struct CUCollection
    {
        private const string _filePath = "./CUList.json";
        private const string _defaultContent = "{\"CU1\":\"CU011\",\"CU2\":\"CU022\",\"CU3\":\"CU033\",\"CU4\":\"CU044\"}";

        public string? CU1 { get; init; }
        public string? CU2 { get; init; }
        public string? CU3 { get; init; }
        public string? CU4 { get; init; }

        public static CUCollection Instance
        {
            get
            {
                if (!File.Exists(_filePath)) File.WriteAllText(_filePath, _defaultContent);
                return JsonSerializer.Deserialize<CUCollection>(File.ReadAllText(_filePath));
            }
        }
    }
}