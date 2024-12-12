using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IF1DriverDao
    {
        public IList<F1Driver> GetF1Drivers();
        public F1Driver GetF1DriverById(int driverId);
        public F1Driver AddF1Driver(F1Driver f1Driver);
        public IList<F1Driver> GetF1DriverByLikeName(string name);
        public F1Driver AddOneToDriverPopularity(int driverId);
        public F1Driver UndoAddOneToDriverPopularity(int driverId);

    }
}
