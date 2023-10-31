namespace Simbir.Go.DAL.Models
{
    public class Account
    {
        /// <summary>
        /// Возвращает и задает id аккаунта.
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Возвращает и задает имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Возвращает и задает пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Возвращает и задает статус, является ли пользователь админом.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Возвращает и задает баланс.
        /// </summary>
        public double Balance { get; set; }
    }
}