using System;

namespace TMS.App.Helpers
{
    class RandomDateTime
    {
        private readonly DateTime start;
        private readonly Random gen;
        private readonly int initialRange;
        private readonly int futureRange;

        public RandomDateTime()
        {
            start = new DateTime(2023, 1, 1);
            gen = new Random();
            initialRange = (DateTime.Today - start).Days;
            futureRange = 60;
        }

        public DateTime GetInitial()
        {
            return start.AddDays(gen.Next(initialRange)).AddHours(gen.Next(0, 24));
        }

        public DateTime GetBiggerThen(DateTime initial)
        {
            return initial.AddDays(gen.Next(futureRange));
        }
    }
}
