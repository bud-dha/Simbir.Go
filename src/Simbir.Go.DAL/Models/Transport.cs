namespace Simbir.Go.DAL.Models
{
    public class Transport
    {
        /// <summary>
        /// Возвращает и задает id транспорта.
        /// </summary>
        public long TransportId { get; set; }

        /// <summary>
        /// Возвращает и задает id пользователя, владеющего ТС.
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        /// Возвращает и задает статус, можно ли арендавать.
        /// </summary>
        public bool CanBeRented { get; set; }

        /// <summary>
        /// Возвращает и задает тип.
        /// </summary>
        public string TransportType { get; set; }

        /// <summary>
        /// Возвращает и задает модель.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Возвращает и задает цвет.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Возвращает и задает номерной знак.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Возвращает и задает описание.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Возвращает и задает географическую широту местонахождения.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Возвращает и задает географическую долготу местонахождения.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Возвращает и задает цену аренды за минуту.
        /// </summary>
        public double? MinutePrice { get; set; }

        /// <summary>
        /// Возвращает и задает цену аренды за сутки.
        /// </summary>
        public double? DayPrice { get; set; }
    }
}