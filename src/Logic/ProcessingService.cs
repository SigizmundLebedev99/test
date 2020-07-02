using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace test.Logic{
    class ProcessingService{
        ParsingService _parsingService;
        PathBuildingService _pbService;
        public ProcessingService(ParsingService parsingService, PathBuildingService pbService){
            _parsingService = parsingService;
            _pbService = pbService;
        }

        public async Task<object> ProcessData(IFormFile file){
            using var stream = file.OpenReadStream();

            var data = await _parsingService.Parse(stream);

            var koordinates = _pbService.BuildPath(data);

            return koordinates;
        }
    }
}