namespace Code.Model.Spawn
{
    public struct SidePoints
    {
        public float FirstPoint { get; private set; }
        public float SecondPoint { get; private set; }
        public float StaticPoint { get; private set; }

        public SidePoints(float firstPoint, float secondPoint, float staticPoint)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            StaticPoint = staticPoint;
        }
    }
}