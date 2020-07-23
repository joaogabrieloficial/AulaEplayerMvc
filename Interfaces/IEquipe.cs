using System.Collections.Generic;

namespace AulaE_playerModel.Models.Interfaces
{
    public interface IEquipe
    {
         void Create(Equipe e);

         List<Equipe> ReadAll();

         void Update(Equipe e);

         void Delete(int idEquipe);
    }
}