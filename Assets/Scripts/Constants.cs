public static class Constants
{
    public static class Simulations
    {
        public const int TICK_RATE = 30;
    }

    public static class World
    {
        public const int UNITS_PER_MM = 100;
        public const int UNITS_PER_CM = 1000;
        public const int UNITS_PER_METER = 100_000;

        public const int WORLD_SIZE_METERS = 10_000;
        public const int WORLD_SIZE_UNITS = WORLD_SIZE_METERS * UNITS_PER_METER;
    }
}