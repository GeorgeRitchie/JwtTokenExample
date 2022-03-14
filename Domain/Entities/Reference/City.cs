using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
    /// <summary>
    /// Объект указывающий на город филиала
    /// </summary>
    public class City : IBaseEntity
    {
        ///	<inheritdoc cref="IBaseEntity.Id"/>
        public Guid Id { get; set; }

        ///	<inheritdoc cref="IBaseEntity.IsDeleted"/>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// <value>Название города</value>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <value>Список филиалов находящиеся в этом городе</value>
        /// </summary>
        public List<Branch> Branches { get; set; } = new();
    }
}

