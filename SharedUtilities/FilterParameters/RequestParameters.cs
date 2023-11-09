namespace SharedUtilities.FilterParameters
{
    public abstract class RequestParameters
    {
        private DateTime _minDate = DateTime.MinValue.ToUniversalTime();
        private DateTime _maxDate = DateTime.MaxValue.ToUniversalTime();

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public DateTime MinDate
        {
            get => _minDate;
            set => _minDate = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set => _maxDate = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
        }
    }
}
