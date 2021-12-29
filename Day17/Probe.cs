using System.Collections.Generic;

namespace Day17
{
    public class Probe
    {
        public (int X, int Y) StartVelocity;
        public (int X, int Y) Velocity;
        public (int X, int Y) Position;
        public (int minX, int maxX, int minY, int maxY) Target;
        public List<(int X, int Y)> Steps = new List<(int X, int Y)>();
        public bool Success { get; private set; }

        public Probe((int, int) startVelocity, (int,int,int,int) target)
        {
            StartVelocity = startVelocity;
            Velocity = startVelocity;
            Target = target;
            Position = (0, 0);
            Success = SimulateSteps();
        }

        public bool IsInTarget => Position.X.IsBetween(Target.minX, Target.maxX) && Position.Y.IsBetween(Target.minY, Target.maxY);
        public bool HasOvershotTarget => Position.X > Target.maxX || Position.Y < Target.minY;

        private bool SimulateSteps()
        {
            while (!HasOvershotTarget)
            {
                Step();
                Steps.Add(Position);
                if (IsInTarget) return true;
            }

            return false;
        }

        private void Step()
        {
            Position.X += Velocity.X;  // Increase X position by Velocity X
            Position.Y += Velocity.Y;  // Increase Y position by Velocity Y

            // Adjust Velocity X towards 0 by 1
            if (Velocity.X > 0) Velocity.X--;
            else if (Velocity.X < 0) Velocity.X++;

            Velocity.Y--;   // Decrease Velocity Y by 1
        }
    }
}
