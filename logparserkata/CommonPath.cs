namespace logparserkata
{
    public class CommonPath
    {
        public int UserId { get; }
        public int LoadTimeSum { get; }
        public IEnumerable<string> Paths { get; } = new List<string>();

        public CommonPath(int loadTimeSum, int userId, IEnumerable<string> paths)
        {
            this.LoadTimeSum = loadTimeSum;
            this.UserId = userId;
            this.Paths = paths;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CommonPath)obj;

            return UserId == other.UserId &&
                   LoadTimeSum == other.LoadTimeSum &&
                   Paths.SequenceEqual(other.Paths);
        }

        //https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + UserId.GetHashCode();
                hash = hash * 23 + LoadTimeSum.GetHashCode();
                hash = hash * 23 + Paths.Aggregate(0, (current, path) => current ^ path.GetHashCode());
                return hash;
            }
        }
    }
}