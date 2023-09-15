using Microsoft.Extensions.Logging;

namespace MN_Logs;

public class Worker
{
    private ILogger<Worker> _logger;
        private Random _random;
        private string[] _carriersName;
        private string[] _pointAction;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _random = new Random();
            
            // rozważ do appsetingsó
            _carriersName = new [] {"Poczta Polska", "DPD", "Inpost", "UPS"};
            _pointAction = new [] {"Dodanie", "Aktualizacja", "Dezaktywacja", "Nic do zmiany"};
        }
        public void Execute()
        {
            foreach (var carrierName in _carriersName)
            {
                ExecuteRandomAction(carrierName);
                var nothingToChangePoints = GetNothingToChangeNumberOfPoints(carrierName);
                for (int i = 0; i < nothingToChangePoints; i++)
                {
                    NothingToChangePoint(carrierName);
                }
            }
            // Example();
        }

        private void ExecuteRandomAction(string carrierName)
        {
            for (int i = 0; i < 100; i++)
            {
                var strategy = _random.Next(1, 4);
                switch (strategy)
                {
                    case 1:
                        NewPoint(carrierName);
                        break;
                    case 2:
                        UpdatePoint(carrierName);
                        break;
                    case 3:
                        DeactivatePoint(carrierName);
                        break;
                }
            }
        }

        private int GetNothingToChangeNumberOfPoints(string carrierName)
        {
            switch (carrierName)
            {
                case "Poczta Polska":
                    return 300;
                case "DPD":
                    return 400;
                case "Inpost":
                    return 500;
                case "UPS":
                    return 600;
            }
            return 0;
            
        }

        private void Example()
        {
            var s1 = "test";
            var s2 = "test";
            _logger.LogInformation("przykład porównania stringów: {1}", s1 == s2);
            
            var o1 = (object)1;
            var o2 = (object)1;
            _logger.LogInformation("przykład porównania obiektów: {0}", o1 == o2);
            
            var example = new { Amount = 108, Message = "Hello" };
            //
            _logger.LogInformation("example => " + example.Amount + " " + example.Message);
            _logger.LogInformation($"example => {example.Amount}, {example.Message}");
            _logger.LogInformation(string.Format("example => {0}, {1}", example.Amount, example.Message));
            _logger.LogInformation("example => {example}", example);
        }

        private int GetRandomId()
        {
            return _random.Next(1000, 10000);
        }

        
        // Przerób jednak na na SG
        private void NewPoint(string carrierName)
        {
            _logger.LogInformation("{CarrierName}. {PointAction} punktu o id {PointId}", carrierName, _pointAction[0], GetRandomId());
        }
        
        private void UpdatePoint(string carrierName)
        {
            _logger.LogInformation("{CarrierName}. {PointAction} punktu o id {PointId}", carrierName, _pointAction[1], GetRandomId());
        }
        
        private void DeactivatePoint(string carrierName)
        {
            _logger.LogInformation("{CarrierName}. {PointAction} punktu o id {PointId}", carrierName, _pointAction[2], GetRandomId());
        }
        
        private void NothingToChangePoint(string carrierName)
        {
            _logger.LogInformation("{CarrierName}. {PointAction} punktu o id {PointId}", carrierName, _pointAction[3], GetRandomId());
        }
}