using System;
using System.IO;
namespace HBCase.Scenario
{
    public interface IScenarioOperations
    {
        void StartScenarios();
    }
    public class ScenarioOperations: IScenarioOperations
    {
        private readonly IScenarioApplier _scenarioApplier;
        private readonly IScenarioReader _scenarioReader;
        public ScenarioOperations(IScenarioReader scenarioReader, IScenarioApplier scenarioApplier)
        {
            _scenarioReader = scenarioReader;
            _scenarioApplier = scenarioApplier;
        }
        public void StartScenarios()
        {
            var basePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

            var filePaths = new string[] { 
                basePath + @"\ScenarioFiles\Scenario1.txt",
                basePath + @"\ScenarioFiles\Scenario2.txt" ,
                basePath + @"\ScenarioFiles\Scenario3.txt" ,
                basePath + @"\ScenarioFiles\Scenario4.txt" ,
                basePath + @"\ScenarioFiles\Scenario5.txt"
            };

            foreach (var filePath in filePaths)
            {
                var commands = _scenarioReader.ReadScenario(filePath);

                _scenarioApplier.ApplyCommands(commands);

                Console.WriteLine("\n");
            }
        }
    }
}
