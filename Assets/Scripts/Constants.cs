public static class Constants
{
    public static class Simulation
    {
        public const int TickRate = 30;
        public const float TickDelta = 1f / TickRate;
    }

    public static class World
    {
        // 1 unit = 0.01 mm  →  100 units = 1 mm
        public const int UnitsPerMillimeter = 100;

        // 1000 units = 1 cm
        public const int UnitsPerCentimeter = 1000;

        // 100_000 units = 1 meter
        public const int UnitsPerMeter = 100_000;

        public const int SizeInMeters = 10_000;
        public const int SizeInUnits = SizeInMeters * UnitsPerMeter;
    }
}