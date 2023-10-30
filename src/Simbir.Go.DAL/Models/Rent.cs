namespace Simbir.Go.DAL.Models
{
    public class Rent
    {
        /// <summary>
        /// Возвращает и задает id аренды.
        /// </summary>
        public long RentId { get; set; }

        /// <summary>
        /// Возвращает и задает id транспортного средства, которое взяли в аренду.
        /// </summary>
        public long TransportId { get; set; }

        /// <summary>
        /// Возвращает и задает id пользователя который будет владеть транспортом на время аренды.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Возвращает и задает дату и время начала аренды.
        /// </summary>
        public string TimeStart { get; set; } = null!;

        /// <summary>
        /// Возвращает и задает дату и время окончания аренды.
        /// </summary>
        public string? TimeEnd { get; set; }

        /// <summary>
        /// Возвращает и задает цену еденицы времени аренды.
        /// </summary>
        public double? PriceOfUnit { get; set; }

        /// <summary>
        /// Возвращает и задает тип оплаты аренды.
        /// </summary>
        public string RentType { get; set; }

        /// <summary>
        /// Возвращает и задает финальную стоимость аренды.
        /// </summary>
        public double? FinalPrice { get; set; }
    }
}