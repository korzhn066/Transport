namespace Transport.Models.Transport.Base
{
    internal abstract class TransportBase
    {
        protected double GetX(double screenWidth, double acceloration, double startSpeed, double maxSpeed , int time)
        {
            double dtime = (double)time / 60;

            return (startSpeed * dtime + GetSpeed(acceloration, startSpeed, maxSpeed ,time) * dtime / 2) / 500 * screenWidth;
        }

        protected double GetSpeed(double acceloration, double startSpeed, double maxSpeed ,int time)
        {
            double dtime = (double)time / 60;

            var v = startSpeed + acceloration * dtime;

            if (v > maxSpeed)
                return maxSpeed;

            return v;
        }

        protected bool IsWorking { get; set; } = true;
        protected double? FuelConsumption { get; set; }
        protected double? FuelCount { get; set; }
        protected double Acceleration { get; set; }
        protected double MaxSpeed { get; set; }
        protected double StartSpeed { get; set; }
    }
}
