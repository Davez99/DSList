using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    public class Belonging
    {
        // Propriedade composta como chave primária
        [Key]
        public BelongingPK Id { get; set; } = new BelongingPK();

        public int Position { get; set; }

        // Acesso direto aos objetos relacionados (facilitadores)
        public Game Game
        {
            get => Id.Game;
            set => Id.Game = value;
        }

        public GameList GameList
        {
            get => Id.GameList;
            set => Id.GameList = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Belonging other &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}